using Microsoft.AspNetCore.Components;
using blazor_wasm.Client.Service;
using Shared.ApiResult;
using MudBlazor;
using blazor_wasm.Client.Shared;
using blazor_wasm.Client.Pages.TemplatePages.TemplateItems;
using EntityContext.Fms;
using blazor_wasm.Client.Pages.TemplatePages.TemplateItems.Inherits;
using Extensions.Extension;

namespace blazor_wasm.Client.Pages.TemplatePages.TemplateItems
{
    public partial class TemplateItem : TemplateItemRenderTree
    {
        [CascadingParameter]
        public string? TemplateId { get; set; }

        [Parameter]
        public HttpService? HttpService { get; set; }

        [Parameter]
        public ISnackbar? SnackBar { get; set; }

        public TemplateItemDTO? TemplateDTO { get; set; }

        private TemplateItemTable? Table { get; set; }

        private TemplateItemChart? Chart { get; set; }


        protected override async Task SetRenderData()
        {
            TemplateDTO = null;
            Table = null;
            try
            {
                if (HttpService is not null)
                {
                    var mineHttpResult = await HttpService.GetAsync("", $"/template-page-layout/get/{TemplateId}");
                    if (mineHttpResult is not null)
                    {
                        if (mineHttpResult.IsSuccessStatusCode)
                        {
                            var result = JsonSerialize.DeSerializeDefault<HttpResult<List<TemplatePageLayout>>>(await mineHttpResult.Content.ReadAsStringAsync());
                            if (result is not null)
                            {
                                if (result.Success)
                                {
                                    if (result.Result is not null)
                                    {
                                        var lists = result.Result;
                                        if (lists is not null)
                                        {
                                            var wireFrame = lists.Where(x => x.Id == TemplateId).FirstOrDefault();
                                            if (wireFrame is not null)
                                            {
                                                TemplateDTO = new TemplateItemDTO(wireFrame.Id, wireFrame.IsWireFrame, wireFrame.IsFlexItem, wireFrame.IsFlexRow, wireFrame.IsFlexGrow
                                                                                , wireFrame.IsFlexBasis, wireFrame.IsShadowBox, wireFrame.ShowHeader, wireFrame.Header, wireFrame.HeaderStyle
                                                                                , wireFrame.ShowTable, wireFrame.DenseTable, wireFrame.HoverTable, wireFrame.BorderTable, wireFrame.StripeTable
                                                                                , wireFrame.HeaderTable, wireFrame.CTableUrl, wireFrame.RTableUrl, wireFrame.UTableUrl, wireFrame.DTableUrl
                                                                                , wireFrame.TableEntityType?.ToPascalCase()
                                                                                , wireFrame.TemplateItem);
                                            }
                                            else
                                            {
                                                throw new Exception($"are missmatch template id and api result {TemplateId}");
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception($"a template not found from api result {TemplateId}");
                                        }
                                    }
                                    else
                                    {
                                        if (result.Message is not null)
                                        {
                                            throw new Exception($"a httpResult not found from api, message {result.Message}");
                                        }
                                        throw new Exception("a httpResult not found from api");
                                    }
                                }
                                else
                                {
                                    throw new Exception($"failed to call api /template-page-layout/get/{TemplateId}");
                                }
                            }
                            else
                            {
                                throw new Exception($"received null from API /template-page-layout/get/{TemplateId}");
                            }
                        }
                        else
                        {
                            throw new Exception($"failed to httpRequest {mineHttpResult.StatusCode.ToString()}");
                        }

                        //child
                        var childHttpResult = await HttpService.GetAsync("", $"/template-page-layout/get/parent-id/{TemplateId}");
                        if (childHttpResult is not null)
                        {
                            if (childHttpResult.IsSuccessStatusCode)
                            {
                                var result = JsonSerialize.DeSerializeDefault<HttpResult<List<TemplatePageLayout>>>(await childHttpResult.Content.ReadAsStringAsync());
                                if (result is not null)
                                {
                                    if (result.Result is not null)
                                    {
                                        var lists = result.Result;
                                        if (lists is not null)
                                        {
                                            var childsIds = lists.Select(x => x.Id).ToList();
                                            if (childsIds is not null)
                                            {
                                                TemplateDTO.Childs = new();
                                                foreach (var id in childsIds)
                                                {
                                                    TemplateDTO.Childs.Add(new TemplateItemComponent(id));
                                                }
                                            }
                                            else
                                            {
                                                throw new Exception($"are missmatch template id and api result {TemplateId}");
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception($"a template not found from api result {TemplateId}");
                                        }
                                    }
                                    else
                                    {
                                        if (result.Message is not null)
                                        {
                                            throw new Exception($"result not found message {result.Message}");
                                        }
                                        throw new Exception("result not found");
                                    }
                                }
                                else
                                {
                                    throw new Exception($"failed to call api /template-page-layout/get/parent-id/{TemplateId}");
                                }
                            }
                            else
                            {
                                throw new Exception($"failed to httpRequest {mineHttpResult.StatusCode.ToString()}");
                            }
                        }
                        else
                        {
                            throw new Exception($"a child template not found tempalte parent_template_id {TemplateId}");
                        }
                    }
                    else
                    {
                        throw new Exception($"template not found tempalte id {TemplateId}");
                    }
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
            await base.SetRenderData();
        }
    }
}