using Microsoft.AspNetCore.Components;
using blazor_wasm.Client.Service;
using System.Text.Json;
using MudBlazor;
using Shared.ApiResult;
using blazor_wasm.Client.Pages.DialogPages.DialogItems;
using System.Text;
using EntityContext.Fms.Wrapper;
using Extensions.Helper;
using blazor_wasm.Client.Shared;
using blazor_wasm.Client.Pages.TemplatePages.TemplateItems.Inherits;
using Extensions.Extension;

namespace blazor_wasm.Client.Pages.TemplatePages.TemplateItems
{
    public partial class TemplateItemTable : TemplateItemRenderTree
    {
        [CascadingParameter]
        public TemplateItemDTO? TemplateItemDTO { get; set; }

        [Parameter]
        public HttpService? HttpService { get; set; }

        [Parameter]
        public ISnackbar? SnackBar { get; set; }

        private ProgressDialog? _progressDialog { get; set; }
        private ProgressDialogDTO _progressDto = new ProgressDialogDTO();

        private MudTable<Dictionary<string, string>>? _table { get; set; }
        private List<Dictionary<string, string>>? TempModelCreate { get; set; }
        private List<Dictionary<string, string>>? TempModelUpdate { get; set; }
        private List<Dictionary<string, string>>? TempModelDelete { get; set; }

        private Dictionary<string, string>? _beforeEditItem;


        private bool _loading = false;

        private TemplateItemTableDTO? TableDto { get; set; }

        private string _searchString { get; set; } = "";

        private void OnChangeCheckBox(Dictionary<string, string> context, string key, string boolValue)
        {
            if (context.ContainsKey(key) == true)
            {
                if (boolValue == "on")
                {
                    context[key] = "True";
                }
                else
                {
                    context[key] = "False";
                }
            }
        }


        private bool _tableReload = false;

        private async Task TableReload()
        {
            try
            {
                ShowLoading();
                if (_table is not null)
                {
                    await _table.ReloadServerData();
                }
            }
            finally
            {
                UnShowLoading();
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (_tableReload)
            {
                _tableReload = false;
                await TableReload();
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        protected override async Task SetRenderData()
        {
            _table = null;
            TempModelCreate = null;
            TempModelUpdate = null;
            TempModelDelete = null;
            _beforeEditItem = null;
            TableDto = null;

            try
            {
                if (TemplateItemDTO is not null)
                {
                    if (TemplateItemDTO.ShowTable ?? false)
                    {
                        if (TemplateItemDTO.TableEntityType is not null && string.IsNullOrEmpty(TemplateItemDTO.TableEntityType).Equals(false))
                        {
                            var entityType = EntityContext.Helper.EntityFinder.FindType(TemplateItemDTO.TableEntityType);
                            if (entityType is not null)
                            {
                                TableDto = new TemplateItemTableDTO(TemplateItemDTO.ShowTable, TemplateItemDTO.DenseTable, TemplateItemDTO.HoverTable, TemplateItemDTO.BorderTable, TemplateItemDTO.StripeTable
                                                                             , TemplateItemDTO.HeaderTable, TemplateItemDTO.CTableUrl, TemplateItemDTO.RTableUrl, TemplateItemDTO.UTableUrl, TemplateItemDTO.DTableUrl
                                                                             , entityType);
                            }
                            else
                            {
                                throw new Exception($"the entityType not found {TemplateItemDTO.TableEntityType}");
                            }
                        }
                        else
                        {
                            throw new Exception($"the entityType is null");
                        }
                    }
                    else
                    {
                        throw new Exception($"a showTable property is false from got database ");
                    }
                }
                else
                {
                    throw new Exception($"error dto is null");
                }

                if (TableDto is not null)
                {
                    TableDto.SetData(await ReadTableData(), true);
                    TableDto.SetBindData(await ReadBindData());
                    if (_table is not null)
                    {
                        ShowLoading();
                        await _table.ReloadServerData();
                    }
                    else
                    {
                        _tableReload = true;
                        StateHasChanged();
                    }
                }
                else
                {
                    throw new Exception($"error dto is null");
                }
            }
            catch (Exception exception)
            {
                if (SnackBar is not null)
                {
                    SnackBar.Add(exception.Message);
                }
                else
                {
                    Console.Write(exception.Message);
                }
            }
            finally
            {
                UnShowLoading();
            }
            await base.SetRenderData();
        }


        private async Task<List<Dictionary<string, string>>?> ReadBindData()
        {
            try
            {
                if (HttpService is null) throw new Exception("a httpService is not found");
                if (TableDto is null) throw new Exception("a tableDto is not found");
                if (TableDto.ShowTalbe is null || TableDto.ShowTalbe.Equals(false)) return null;
                if (TableDto.RTableUrl is null || string.IsNullOrEmpty(TableDto.RTableUrl)) return null;

                if (TableDto.TableEntityType is null) throw new Exception($"a TableEntityType is null");
                var entitySpecType = TemplatePagesConst.GetEntitySpecType();
                if (entitySpecType is null) throw new Exception("a entitySpecType is null");

                var requestUrl = $"{TemplatePagesConst.GetUrlEntitySpec(TableDto.TableEntityType.Name.ToSnakeCase())}";
                var readHttpResult = await HttpService.GetAsync("", requestUrl);
                if (readHttpResult is null) throw new Exception("a readHttpResult is not found");
                if (readHttpResult.IsSuccessStatusCode == false) throw new Exception($"failed to httpRequest {readHttpResult.StatusCode.ToString()}");

                var genericListType = Extensions.Helper.GenericHelper.MakeGenericList(entitySpecType);
                if (genericListType is null) throw new Exception($"entitySpecType error {TableDto.TableEntityType.Name.ToString()}");
                var genericResultType = typeof(HttpResult<>).MakeGenericType(genericListType);

                var result = JsonSerialize.DeSerializeDefault(await readHttpResult.Content.ReadAsStringAsync(), genericResultType);
                if (result is null) throw new Exception("result not found");
                var resultResultList = GenericHelper.GetValueObject(result, "Result");
                if (resultResultList is null) throw new Exception($"a template not found from api result {requestUrl}");

                var functionResult = Extensions
                                    .Helper
                                    .GenericHelper
                                    .CallGenericStaticMethod<List<Dictionary<string, string>>?>
                                    (
                                        entitySpecType
                                        , typeof(EntityHelper.EntityConverter)
                                        , false
                                        , nameof(EntityHelper.EntityConverter.GetDataDictionarys)
                                        , new object[] { resultResultList }
                                    );

                if (functionResult is null) throw new Exception(@"Failed To Call Function GetDataDictionarys");
                return functionResult;
            }
            catch (System.Exception exception)
            {
                if (SnackBar is not null)
                {
                    SnackBar.Add(exception.Message);
                }
                else
                {
                    Console.WriteLine(exception.Message);
                }
            }
            return null;
        }

        private async Task<List<Dictionary<string, string>>?> ReadTableData()
        {
            try
            {
                if (HttpService is null) throw new Exception("a httpService is not found");
                if (TableDto is null) throw new Exception("a tableDto is not found");
                if (TableDto.ShowTalbe is null || TableDto.ShowTalbe.Equals(false)) throw new Exception("a showTable is false from TableDto");
                if (TableDto.RTableUrl is null || string.IsNullOrEmpty(TableDto.RTableUrl)) throw new Exception("RTableUrl is null");

                var readHttpResult = await HttpService.GetAsync("", $"/{TableDto.RTableUrl}");
                if (readHttpResult is null) throw new Exception($"RTableUrl Error {TableDto.RTableUrl}");
                if (readHttpResult.IsSuccessStatusCode == false) throw new Exception($"failed to httpRequest {readHttpResult.StatusCode.ToString()}");
                if (TableDto.TableEntityType is null) throw new Exception($"a TableEntityType is null");

                var genericListType = Extensions.Helper.GenericHelper.MakeGenericList(TableDto.TableEntityType);
                if (genericListType is null) throw new Exception($"TableDto.TableEntityType error {TableDto.TableEntityType.Name.ToString()}");
                var genericResultType = typeof(HttpResult<>).MakeGenericType(genericListType);

                var result = JsonSerialize.DeSerializeDefault(await readHttpResult.Content.ReadAsStringAsync(), genericResultType);
                if (result is null) throw new Exception("result not found");
                var resultResultList = GenericHelper.GetValueObject(result, "Result");
                if (resultResultList is null) throw new Exception($"a template not found from api result {TableDto.RTableUrl}");

                var functionResult = Extensions
                                    .Helper
                                    .GenericHelper
                                    .CallGenericStaticMethod<List<Dictionary<string, string>>?>
                                    (
                                        TableDto.TableEntityType
                                        , typeof(EntityHelper.EntityConverter)
                                        , false
                                        , nameof(EntityHelper.EntityConverter.GetDataDictionarys)
                                        , new object[] { resultResultList }
                                    );

                if (functionResult is null) throw new Exception(@"Failed To Call Function GetDataDictionarys");
                return functionResult;
            }
            catch (System.Exception exception)
            {
                if (SnackBar is not null)
                {
                    SnackBar.Add(exception.Message);
                }
                else
                {
                    Console.WriteLine(exception.Message);
                }
            }
            return null;
        }


        #region event
        private void ShowLoading()
        {
            _loading = true;
        }
        private void UnShowLoading()
        {
            _loading = false;
        }
        private bool FilterFunc1(Dictionary<string, string> element) => FilterFunc(element, _searchString);

        private bool FilterFunc(Dictionary<string, string> element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;

            foreach (var value in element.Values)
            {
                if (value is null) return true;
                if (value.Contains(searchString, StringComparison.OrdinalIgnoreCase)) return true;
            }
            return false;
        }
        private string RowStyleFunc(Dictionary<string, string> item, int index)
        {
            if (TempModelCreate is not null && TempModelCreate.Count > 0)
            {
                if (TempModelCreate.Where(x => x == item).FirstOrDefault() is not null)
                {
                    return "background-color:red";
                }
            }
            if (TempModelUpdate is not null && TempModelUpdate.Count > 0)
            {
                if (TempModelUpdate.Where(x => x == item).FirstOrDefault() is not null)
                {
                    return "background-color:red";
                }
            }
            if (TempModelDelete is not null && TempModelDelete.Count > 0)
            {
                if (TempModelDelete.Where(x => x == item).FirstOrDefault() is not null)
                {
                    return "background-color:red";
                }
            }

            return "";
        }

        private void BackupItem(object beforeItem)
        {
            try
            {
                ShowLoading();
                var castBeforeItem = beforeItem as Dictionary<string, string>;
                if (castBeforeItem is not null)
                {
                    if (_beforeEditItem is not null)
                    {
                        _beforeEditItem.Clear();
                        _beforeEditItem = null;
                    }
                    _beforeEditItem = new();

                    foreach (var dictPair in castBeforeItem)
                    {
                        _beforeEditItem.Add(dictPair.Key, dictPair.Value);
                    }
                }
                else
                {
                    throw new Exception($"selectedItem is null, you must be do refresh");
                }
            }
            catch (System.Exception exception)
            {
                if (SnackBar is not null)
                {
                    SnackBar.Add(exception.Message);
                }
                else
                {
                    Console.Write(exception.Message);
                }
            }
            finally
            {
                UnShowLoading();
            }
        }
        private void CommitItem(object originItem)
        {
            try
            {
                ShowLoading();
                var castOriginItem = originItem as Dictionary<string, string>;
                if (castOriginItem is not null)
                {
                    bool isCreate = false;
                    if (TempModelCreate is not null)
                    {
                        if (TempModelCreate.Where(x => x == castOriginItem).FirstOrDefault() is not null)
                        {
                            isCreate = true;
                        }
                    }

                    if (isCreate.Equals(false))
                    {
                        if (TempModelUpdate is null) TempModelUpdate = new();
                        if (TempModelUpdate.Where(x => x == castOriginItem).FirstOrDefault() is null)
                        {
                            TempModelUpdate.Add(castOriginItem);
                        }
                    }

                    if (_beforeEditItem is not null)
                    {
                        _beforeEditItem.Clear();
                        _beforeEditItem = null;
                    }
                }
                else
                {
                    throw new Exception($"selectedItem is null, trying refresh");
                }
            }
            catch (System.Exception exception)
            {
                if (SnackBar is not null)
                {
                    SnackBar.Add(exception.Message);
                }
                else
                {
                    Console.Write(exception.Message);
                }
            }
            finally
            {
                UnShowLoading();
            }
        }

        private void ResetItem(object originItem)
        {
            try
            {
                ShowLoading();
                var castOriginItem = originItem as Dictionary<string, string>;
                if (castOriginItem is not null)
                {
                    if (_beforeEditItem is not null)
                    {
                        foreach (var dictPairOrigin in castOriginItem)
                        {
                            string? backupValue;
                            var existBackUpValue = _beforeEditItem.TryGetValue(dictPairOrigin.Key, out backupValue);
                            if (backupValue is not null)
                            {
                                castOriginItem[dictPairOrigin.Key] = backupValue;
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("BackupedItem is null, trying refresh");
                    }
                }
                else
                {
                    throw new Exception($"selectedItem is null, trying refresh");
                }
            }
            catch (System.Exception exception)
            {
                if (SnackBar is not null)
                {
                    SnackBar.Add(exception.Message);
                }
                else
                {
                    Console.Write(exception.Message);
                }
            }
            finally
            {
                UnShowLoading();
            }
        }

        private async Task OnClickR()
        {
            try
            {
                ShowLoading();
                if (TableDto is not null)
                {
                    if (TableDto.RTableUrl is not null && string.IsNullOrEmpty(TableDto.RTableUrl).Equals(false))
                    {
                        TableDto.SetData(await ReadTableData(), true);
                        if (_table is not null)
                        {
                            await _table.ReloadServerData();
                        }
                        else
                        {
                            throw new Exception("a render fragment MudTable is null");
                        }
                    }
                    else
                    {
                        throw new Exception("is url null from RTableUrl in the TableDto");
                    }
                }
                else
                {
                    throw new Exception("a TableDto is null");
                }
            }
            catch (Exception exception)
            {
                if (SnackBar is not null)
                {
                    SnackBar.Add(exception.Message);
                }
                else
                {
                    Console.WriteLine(exception.Message);
                }
            }
            finally
            {
                UnShowLoading();
            }
        }

        private Dictionary<string, string>? CreateEntityTypeInstance(Type toCreateType)
        {
            try
            {
                var newModel = Extensions.Helper.GenericHelper.Instance(toCreateType);
                if (newModel is not null)
                {
                    var castModel = Convert.ChangeType(newModel, toCreateType);
                    return EntityHelper.EntityConverter.GetDataDictionary(castModel);
                }
            }
            catch (System.Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return null;
        }

        private async Task OnClickC()
        {
            try
            {
                ShowLoading();
                if (TemplateItemDTO is not null && TableDto is not null)
                {
                    if (TemplateItemDTO.TableEntityType is not null)
                    {
                        if (TempModelCreate is null) TempModelCreate = new();
                        var entityType = EntityContext.Helper.EntityFinder.FindType(TemplateItemDTO.TableEntityType);
                        if (entityType is not null)
                        {
                            Dictionary<string, string>? newModel = CreateEntityTypeInstance(entityType);
                            if (newModel is not null)
                            {
                                var newModelRef = TableDto.AddFirstData(newModel);
                                if (newModelRef is not null)
                                {
                                    TempModelCreate.Add(newModelRef);
                                    if (_table is not null)
                                    {
                                        await _table.ReloadServerData();
                                    }
                                    else
                                    {
                                        throw new Exception("a render fragment MudTable is null");
                                    }
                                }
                                else
                                {
                                    throw new Exception("failed to add to origin data");
                                }
                            }
                            else
                            {
                                throw new Exception($"fail the CreateEntityTypeInstance");
                            }
                        }
                        else
                        {
                            throw new Exception($"fail the find EntityType, because Cast Fail");
                        }
                    }
                    else
                    {
                        throw new Exception($"a TableEntityType is null");
                    }
                }
                else
                {
                    throw new Exception($"a TemplateItemDTO is null");
                }
            }
            catch (Exception exception)
            {
                if (SnackBar is not null)
                {
                    SnackBar.Add(exception.Message, Severity.Warning);
                }
                else
                {
                    Console.WriteLine(exception.Message);
                }
            }
            finally
            {
                UnShowLoading();
            }
        }
        private async Task OnClickD()
        {
            await Task.Delay(0);
            try
            {
                ShowLoading();
                if (TemplateItemDTO is not null && TableDto is not null)
                {
                    if (TemplateItemDTO.TableEntityType is not null)
                    {
                        if (TableDto.SelectedItems is not null && TableDto.SelectedItems.Count > 0)
                        {
                            foreach (var selectedItem in TableDto.SelectedItems)
                            {
                                bool hasCModel = false, hasUModel = false;
                                if (TempModelCreate is not null && TempModelCreate.Count > 0)
                                {
                                    var cModel = TempModelCreate.Where(x => x == selectedItem).FirstOrDefault();
                                    if (cModel is not null)
                                    {
                                        hasCModel = true;
                                        if (TempModelCreate.Remove(cModel).Equals(false))
                                        {
                                            throw new Exception("error. please retry");
                                        }

                                        if (TableDto.RemoveData(cModel).Equals(false))
                                        {
                                            throw new Exception("error. please retry, if that doesn't work do the refresh");
                                        }
                                    }
                                }

                                if (TempModelUpdate is not null && TempModelUpdate.Count > 0)
                                {
                                    var uModel = TempModelUpdate.Where(x => x == selectedItem).FirstOrDefault();
                                    if (uModel is not null)
                                    {
                                        hasUModel = true;
                                        if (TempModelUpdate.Remove(uModel).Equals(false))
                                        {
                                            throw new Exception("error. please retry");
                                        }

                                        if (TableDto.RemoveData(uModel).Equals(false))
                                        {
                                            throw new Exception("error. please retry, if that doesn't work do the refresh");
                                        }
                                    }
                                }

                                if (hasCModel.Equals(false) && hasUModel.Equals(false))
                                {
                                    if (TempModelDelete is null)
                                    {
                                        TempModelDelete = new();
                                    }
                                    TempModelDelete.Add(selectedItem);
                                    if (TableDto.RemoveData(selectedItem).Equals(false))
                                    {
                                        throw new Exception("error. please retry, if that doesn't work do the refresh");
                                    }
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("no items selected");
                        }
                    }
                    else
                    {
                        throw new Exception("failed, because no have entity type");
                    }
                }
                else
                {
                    throw new Exception("failed, because no have tableDto");
                }
            }
            catch (Exception exception)
            {
                if (SnackBar is not null)
                {
                    SnackBar.Add(exception.Message, Severity.Warning);
                }
                else
                {
                    Console.WriteLine(exception.Message);
                }
            }
            finally
            {
                UnShowLoading();
            }
        }
        private async Task OnClickSave()
        {
            await Task.Delay(0);
            try
            {
                ShowLoading();
                _progressDto.Show();
                if (HttpService is not null)
                {
                    if (TableDto is not null)
                    {
                        if (TableDto.ShowTalbe is not null && TableDto.ShowTalbe.Equals(true))
                        {
                            #region C
                            if (TableDto.CTableUrl is not null && string.IsNullOrEmpty(TableDto.CTableUrl).Equals(false))
                            {
                                if (TempModelCreate is not null && TempModelCreate.Count > 0)
                                {
                                    _progressDto.AddFirstTextContent("doing a create");
                                    _progressDto.AddMaxValue(TempModelCreate.Count);
                                    List<Dictionary<string, string>> successCreates = new();
                                    foreach (var create in TempModelCreate)
                                    {
                                        try
                                        {
                                            if (create is not null)
                                            {
                                                if (TemplateItemDTO is not null && TemplateItemDTO.TableEntityType is not null)
                                                {
                                                    var entityType = EntityContext.Helper.EntityFinder.FindType(TemplateItemDTO.TableEntityType);
                                                    if (entityType is not null)
                                                    {
                                                        var newModel = Extensions.Helper.GenericHelper.Instance(entityType);
                                                        if (newModel is not null)
                                                        {
                                                            var castModel = Convert.ChangeType(newModel, entityType);
                                                            if (castModel is not null)
                                                            {
                                                                if (EntityHelper.EntityConverter.SetEntity(ref castModel, entityType, create).Equals(false))
                                                                {
                                                                    throw new Exception($"a type are different entityType and dictionaryType");
                                                                }

                                                                var content = new StringContent(JsonSerialize.SerializeDefault(castModel), Encoding.UTF8, "application/json");
                                                                var readHttpResult = await HttpService.PostAsync("", $"/{TableDto.CTableUrl}", content);
                                                                if (readHttpResult is not null)
                                                                {
                                                                    if (readHttpResult.IsSuccessStatusCode)
                                                                    {
                                                                        var genericResultType = typeof(HttpResult<>).MakeGenericType(entityType);
                                                                        var result = JsonSerialize.DeSerializeDefault(await readHttpResult.Content.ReadAsStringAsync(), genericResultType);
                                                                        if (result is not null)
                                                                        {
                                                                            var success = GenericHelper.GetValueObject(result, "Success");
                                                                            if (success is not null && ((bool)success))
                                                                            {
                                                                                successCreates.Add(create);
                                                                                _progressDto.AddValueOne();
                                                                            }
                                                                            else
                                                                            {
                                                                                var message = Extensions.Helper.GenericHelper.GetValueObject(result, "Message");
                                                                                throw new Exception($"api error, reasons is {message}");
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            throw new Exception("api error, reasons is result not found");
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        throw new Exception($"failed to HttpCall, reason code {readHttpResult.StatusCode}");
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    throw new Exception($"httpResult not found {TableDto.CTableUrl}");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                throw new Exception($"casted fail, reason are error entity type {entityType.Name}");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            throw new Exception("failed a create instance");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        throw new Exception("failed a create instance");
                                                    }
                                                }
                                                else
                                                {
                                                    throw new Exception("a system don't have entityType");
                                                }
                                            }
                                            else
                                            {
                                                throw new Exception("a createModel not found");
                                            }
                                        }
                                        catch (System.Exception exceptionCreate)
                                        {
                                            _progressDto.AddFirstTextContent(exceptionCreate.Message);
                                        }
                                    }

                                    if (successCreates.Count > 0)
                                    {
                                        foreach (var success in successCreates)
                                        {
                                            if (TempModelCreate.Remove(success) == false)
                                            {
                                                _progressDto.AddFirstTextContent("logic has error, so that need try refresh after work process");
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region U
                            if (TableDto.UTableUrl is not null && string.IsNullOrEmpty(TableDto.UTableUrl).Equals(false))
                            {
                                if (TempModelUpdate is not null && TempModelUpdate.Count > 0)
                                {
                                    _progressDto.AddFirstTextContent("doing a update");
                                    _progressDto.AddMaxValue(TempModelUpdate.Count);
                                    List<Dictionary<string, string>> successUpdates = new();
                                    foreach (var update in TempModelUpdate)
                                    {
                                        try
                                        {
                                            if (update is not null)
                                            {
                                                if (TemplateItemDTO is not null && TemplateItemDTO.TableEntityType is not null)
                                                {
                                                    var entityType = EntityContext.Helper.EntityFinder.FindType(TemplateItemDTO.TableEntityType);
                                                    if (entityType is not null)
                                                    {
                                                        var newModel = Extensions.Helper.GenericHelper.Instance(entityType);
                                                        if (newModel is not null)
                                                        {
                                                            var castModel = Convert.ChangeType(newModel, entityType);
                                                            if (castModel is not null)
                                                            {
                                                                if (EntityHelper.EntityConverter.SetEntity(ref castModel, entityType, update).Equals(false))
                                                                {
                                                                    throw new Exception($"a type are different entityType and dictionaryType");
                                                                }
                                                                var content = new StringContent(JsonSerialize.SerializeDefault(castModel), Encoding.UTF8, "application/json");
                                                                var readHttpResult = await HttpService.PutAsync("", $"/{TableDto.UTableUrl}", content);
                                                                if (readHttpResult is not null)
                                                                {
                                                                    if (readHttpResult.IsSuccessStatusCode)
                                                                    {
                                                                        var genericResultType = typeof(HttpResult<>).MakeGenericType(typeof(int));
                                                                        var result = JsonSerialize.DeSerializeDefault(await readHttpResult.Content.ReadAsStringAsync(), genericResultType);
                                                                        if (result is not null)
                                                                        {
                                                                            var success = GenericHelper.GetValueObject(result, "Success");
                                                                            if (success is not null && ((bool)success))
                                                                            {
                                                                                successUpdates.Add(update);
                                                                                _progressDto.AddValueOne();
                                                                            }
                                                                            else
                                                                            {
                                                                                var message = Extensions.Helper.GenericHelper.GetValueObject(result, "Message");
                                                                                throw new Exception($"api error, reasons is {message}");
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            throw new Exception("api error, reasons is result not found");
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        throw new Exception($"failed to HttpCall, reason code {readHttpResult.StatusCode}");
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    throw new Exception($"httpResult not found {TableDto.CTableUrl}");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                throw new Exception($"casted fail, reason are error entity type {entityType.Name}");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            throw new Exception("failed a create instance");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        throw new Exception("failed a create instance");
                                                    }
                                                }
                                                else
                                                {
                                                    throw new Exception("a system don't have entityType");
                                                }
                                            }
                                            else
                                            {
                                                throw new Exception("a updateModel not found");
                                            }
                                        }
                                        catch (System.Exception exceptionCreate)
                                        {
                                            _progressDto.AddFirstTextContent(exceptionCreate.Message);
                                        }
                                    }

                                    if (successUpdates.Count > 0)
                                    {
                                        foreach (var update in successUpdates)
                                        {
                                            if (TempModelUpdate.Remove(update) == false)
                                            {
                                                _progressDto.AddFirstTextContent("logic has error, so that need try refresh after work process");
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region D
                            if (TableDto.DTableUrl is not null && string.IsNullOrEmpty(TableDto.DTableUrl).Equals(false))
                            {
                                if (TempModelDelete is not null && TempModelDelete.Count > 0)
                                {
                                    _progressDto.AddFirstTextContent("doing a delete");
                                    _progressDto.AddMaxValue(TempModelDelete.Count);
                                    List<Dictionary<string, string>> successDeletes = new();
                                    foreach (var delete in TempModelDelete)
                                    {
                                        try
                                        {
                                            if (delete is not null)
                                            {
                                                if (TemplateItemDTO is not null && TemplateItemDTO.TableEntityType is not null)
                                                {
                                                    var entityType = EntityContext.Helper.EntityFinder.FindType(TemplateItemDTO.TableEntityType);
                                                    if (entityType is not null)
                                                    {
                                                        var newModel = Extensions.Helper.GenericHelper.Instance(entityType);
                                                        if (newModel is not null)
                                                        {
                                                            var castModel = Convert.ChangeType(newModel, entityType);
                                                            if (castModel is not null)
                                                            {
                                                                if (EntityHelper.EntityConverter.SetEntity(ref castModel, entityType, delete).Equals(false))
                                                                {
                                                                    throw new Exception($"a type are different entityType and dictionaryType");
                                                                }
                                                                string deleteFromQuery = ((FmsWrapper)castModel).GetDeleteFromQuery();
                                                                var readHttpResult = await HttpService.DeleteAsync("", $"/{TableDto.DTableUrl}?{deleteFromQuery}");
                                                                if (readHttpResult is not null)
                                                                {
                                                                    if (readHttpResult.IsSuccessStatusCode)
                                                                    {
                                                                        var genericResultType = typeof(HttpResult<>).MakeGenericType(typeof(int));
                                                                        var result = JsonSerialize.DeSerializeDefault(await readHttpResult.Content.ReadAsStringAsync(), genericResultType);
                                                                        if (result is not null)
                                                                        {
                                                                            var success = GenericHelper.GetValueObject(result, "Success");
                                                                            var excCnt = GenericHelper.GetValueObject(result, "Result");
                                                                            if (success is not null && ((bool)success) && excCnt is not null && ((int)excCnt) > 0)
                                                                            {
                                                                                successDeletes.Add(delete);
                                                                                _progressDto.AddValueOne();
                                                                            }
                                                                            else
                                                                            {
                                                                                var message = GenericHelper.GetValueObject(result, "Message");
                                                                                throw new Exception($"api error, the reason why {message}");
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            throw new Exception("api error, reasons is result not found");
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        throw new Exception($"failed to HttpCall, reason code {readHttpResult.StatusCode}");
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    throw new Exception($"httpResult not found {TableDto.CTableUrl}");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                throw new Exception($"casted fail, reason are error entity type {entityType.Name}");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            throw new Exception("failed a create instance");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        throw new Exception("failed a create instance");
                                                    }
                                                }
                                                else
                                                {
                                                    throw new Exception("a system don't have entityType");
                                                }
                                            }
                                            else
                                            {
                                                throw new Exception("a updateModel not found");
                                            }
                                        }
                                        catch (System.Exception exceptionCreate)
                                        {
                                            _progressDto.AddFirstTextContent(exceptionCreate.Message);
                                        }
                                    }

                                    if (successDeletes.Count > 0)
                                    {
                                        foreach (var delete in successDeletes)
                                        {
                                            if (TempModelDelete.Remove(delete) == false)
                                            {
                                                _progressDto.AddFirstTextContent("logic has error, so that need try refresh after work process");
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion

                            _progressDto.AddFirstTextContent("the system have completed all processes");
                            _progressDto.Complete();

                            if (_table is not null)
                            {
                                await _table.ReloadServerData();
                            }
                        }
                    }
                }
            }
            catch (System.Exception exception)
            {
                if (SnackBar is not null)
                {
                    SnackBar.Add($"A fatal error has occurred, must be doing refresh {exception.Message}", Severity.Error);
                }
                else
                {
                    Console.WriteLine(exception.Message);
                }
            }
            finally
            {
                UnShowLoading();
            }
        }
        #endregion
    }
}