using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityContext.Fms.Wrapper;
using Extensions.Extension;

namespace blazor_wasm.Client.Pages.TemplatePages.TemplateItems
{
    public class TemplateItemTableDTO
    {
        private const string KEY_GROUPBY_BIND_DATA = "EntityItemCd";
        private const string VAL_GROUPBY_BIND_DATA = "EntityItemVal";

        public List<IGrouping<string, Dictionary<string, string>>>? BindDatas;
        private Dictionary<string, List<string>>? BindDataGrouping;
        public List<Dictionary<string, string>>? Datas;
        public HashSet<Dictionary<string, string>> SelectedItems = new();

        public TemplateItemTableDTO(bool? showTalbe, bool? denseTable, bool? hoverTable, bool? borderTable, bool? stripeTable, string? headerTable, string? cTableUrl, string? rTableUrl, string? uTableUrl, string? dTableUrl, Type? tableEntityType)
        {
            ShowTalbe = showTalbe;
            DenseTable = denseTable;
            HoverTable = hoverTable;
            BorderTable = borderTable;
            StripeTable = stripeTable;
            HeaderTable = headerTable;
            CTableUrl = cTableUrl;
            RTableUrl = rTableUrl;
            UTableUrl = uTableUrl;
            DTableUrl = dTableUrl;
            TableEntityType = tableEntityType;
        }

        public bool? ShowTalbe { get; set; } = false;
        public bool? DenseTable { get; set; } = false;
        public bool? HoverTable { get; set; } = false;
        public bool? BorderTable { get; set; } = false;
        public bool? StripeTable { get; set; } = false;
        public string? HeaderTable { get; set; } = "";
        public string? CTableUrl { get; set; } = "";
        public string? RTableUrl { get; set; } = "";
        public string? UTableUrl { get; set; } = "";
        public string? DTableUrl { get; set; } = "";

        public Type? TableEntityType { get; set; }

        public Dictionary<string, Type>? TableEntityNameTypes;

        public Dictionary<string, Type>? GetNameTypeDictionaryTableEntity(bool init = false)
        {
            if (TableEntityType is null) return null;
            if (TableEntityNameTypes == null || init)
            {
                TableEntityNameTypes = new();
            }
            TableEntityNameTypes = EntityHelper.EntityConverter.GetNameTypeDictionaryTableEntity(TableEntityType);
            return TableEntityNameTypes;
        }

        public List<string>? GetDataColumnNames()
        {
            if (TableEntityType is not null)
            {
                var props = EntityHelper.EntityConverter.GetPropNames(TableEntityType);
                if (props is not null)
                {
                    return props.ToList();
                }
            }
            return null;
        }

        public void SetData(List<Dictionary<string, string>>? list, bool clearSelected)
        {
            if (clearSelected)
            {
                SelectedItems.Clear();
            }
            Datas = list;
        }


        /// <summary>
        /// fmsWrapper.cs를 래핑한 클래스만 가능합니다.
        /// </summary>
        /// <param name="list"></param>
        public void SetBindData(List<Dictionary<string, string>>? list)
        {
            try
            {
                if (BindDatas is null) BindDatas = new();
                if (list is null) return;
                BindDatas = list.GroupBy(x => x[KEY_GROUPBY_BIND_DATA]).ToList();

                if (BindDatas is not null && BindDatas.Count > 0)
                {
                    if (TableEntityType is not null)
                    {
                        var newModel = Extensions.Helper.GenericHelper.Instance(TableEntityType);
                        if (newModel is not null)
                        {
                            var typeModel = newModel as FmsWrapper;
                            if (typeModel is not null)
                            {
                                var bindColumns = typeModel.GetBindColumns();
                                if (bindColumns is not null && bindColumns.Count > 0)
                                {
                                    BindDataGrouping = BindDatas.Where(g => bindColumns.Contains(g.Key))
                                                                .ToDictionary(k => k.Key, v => v.Select(x => x[VAL_GROUPBY_BIND_DATA]).ToList());
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public List<string>? GetBindValue(string key, bool isSnakeCase = false)
        {
            string find = key;
            if(isSnakeCase) find = key.ToSnakeCase();
            if (BindDatas is null) return null;
            if(BindDataGrouping is null) return null;
            if(BindDataGrouping.ContainsKey(find) == false) return null;
            return BindDataGrouping[find];
            //return BindDatas.Where(g => g.Key == key).Select(g => g.Select(x => x[VAL_GROUPBY_BIND_DATA])).FirstOrDefault()?.ToList();
        }

        public Dictionary<string, string>? AddData(Dictionary<string, string>? newModel, bool isFirst = false)
        {
            if (isFirst.Equals(true))
            {
                return AddFirstData(newModel);
            }
            else
            {
                if (newModel is not null)
                {
                    if (Datas is null) Datas = new();
                    Datas.Add(newModel);
                    return newModel;
                }
            }
            return null;
        }
        public Dictionary<string, string>? AddFirstData(Dictionary<string, string>? newModel)
        {
            if (newModel is not null)
            {
                if (Datas is null) Datas = new();
                Datas.Add(newModel);
                return newModel;
            }
            return null;
        }

        public bool RemoveData(Dictionary<string, string>? removeModel)
        {
            if (removeModel is not null)
            {
                if (Datas is not null)
                {
                    return Datas.Remove(removeModel);
                }
            }
            return false;
        }
    }
}