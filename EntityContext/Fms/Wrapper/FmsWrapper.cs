using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Extensions.Extension;

namespace EntityContext.Fms.Wrapper
{
    public class FmsWrapper : IQueryBase
    {
        public FmsWrapper(Type childType)
        {
            ChildType = childType;
        }

        [JsonIgnore]
        protected Type ChildType { get; set; }

        [JsonIgnore]
        protected virtual List<string>? KeyColumns { get; }
        [JsonIgnore]
        protected virtual List<string>? BindColumns { get; }

        public List<string>? GetBindColumns(){
            return BindColumns;
        }
        protected bool IsBindColumn(string columnName){
            if(BindColumns is null) return false;
            return BindColumns.Contains(columnName);
        }
        
        public string? GetCreateQuery()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                object child = Convert.ChangeType(this, ChildType);
                var props = GetChildProps();

                sb.Append(" insert into fms.");
                sb.Append(ChildType.Name.ToSnakeCase());
                sb.Append(" (");
                foreach (var propName in props.Select(x => x.Name.ToSnakeCase()).ToList())
                {
                    sb.Append(propName);
                    sb.Append(",");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append(") ");
                sb.Append(" values");
                sb.Append(" (");
                foreach (var prop in props)
                {
                    var value = prop.GetValue(child, null);
                    if (prop.PropertyType == typeof(string))
                    {
                        if (value is not null)
                        {
                            sb.Append($"{value.ToString()?.Quot()}");
                        }
                        else
                        {
                            sb.Append($"{"".Quot()}");
                        }
                    }
                    else
                    {
                        if (value is not null)
                        {
                            sb.Append($"{value}");
                        }
                        else
                        {
                            sb.Append($"null");
                        }
                    }
                    sb.Append(",");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append(") ");
            }
            catch (System.Exception exception)
            {
                return exception.Message;
            }

            return sb.ToString();
        }

        public string GetDeleteFromQuery()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                object child = Convert.ChangeType(this, ChildType);
                var props = GetChildProps();
                if (KeyColumns is not null)
                {
                    for (int keyIte = 0; keyIte < KeyColumns.Count(); keyIte++)
                    {
                        PropertyInfo? prop = props.Where(x => x.Name.ToSnakeCase() == KeyColumns[keyIte]).FirstOrDefault();
                        if (prop is null) throw new Exception("failed, reason prop not found");
                        sb.Append($"{KeyColumns[keyIte].ToCamelCase()}=");
                        var value = prop.GetValue(child, null);
                        if (prop.PropertyType == typeof(string))
                        {
                            if (value is not null)
                            {
                                sb.Append($"{value.ToString()}");
                            }
                            else
                            {
                                sb.Append($"{""}");
                            }
                        }
                        else
                        {
                            if (value is not null)
                            {
                                sb.Append($"{value}");
                            }
                            else
                            {
                                sb.Append($"{""}");
                            }
                        }
                        sb.Append("&");
                    }
                    sb.Remove(sb.Length - 1, 1);
                }
                else
                {
                    throw new Exception("keycolumns are not binded");
                }
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
            return sb.ToString();
        }

        public string? GetDeleteQuery()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                object child = Convert.ChangeType(this, ChildType);
                var props = GetChildProps();

                sb.Append(" delete from fms. ");
                sb.Append(ChildType.Name.ToSnakeCase());
                if (KeyColumns is not null)
                {
                    for (int keyIte = 0; keyIte < KeyColumns.Count(); keyIte++)
                    {
                        if (keyIte < 1)
                        {
                            sb.Append(" where ");
                        }
                        PropertyInfo? prop = props.Where(x => x.Name.ToSnakeCase() == KeyColumns[keyIte]).FirstOrDefault();
                        if (prop is null) throw new Exception("failed, reason prop not found");
                        sb.Append($"{KeyColumns[keyIte]} = ");

                        var value = prop.GetValue(child, null);
                        if (prop.PropertyType == typeof(string))
                        {
                            if (value is not null)
                            {
                                sb.Append($"{value.ToString()?.Quot()}");
                            }
                            else
                            {
                                sb.Append($"{"".Quot()}");
                            }
                        }
                        else
                        {
                            if (value is not null)
                            {
                                sb.Append($"{value}");
                            }
                            else
                            {
                                sb.Append($"null");
                            }
                        }
                        sb.Append(",");
                    }
                    sb.Remove(sb.Length - 1, 1);
                }
            }
            catch (System.Exception exception)
            {
                return exception.Message;
            }
            return sb.ToString();
        }

        public string? GetReadQuery()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                var props = GetChildProps();
                sb.Append(" select ");
                foreach (var propName in props.Select(x => x.Name.ToSnakeCase()).ToList())
                {
                    sb.Append(propName);
                    sb.Append(",");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append(" from fms.");
                sb.Append(ChildType.Name.ToSnakeCase());
                sb.Append(" ");
            }
            catch (System.Exception exception)
            {
                return exception.Message;
            }

            return sb.ToString();
        }

        public string? GetUpdateQuery()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                object child = Convert.ChangeType(this, ChildType);
                var props = GetChildProps();

                sb.Append(" update fms.");
                sb.Append(ChildType.Name.ToSnakeCase());
                sb.Append(" set ");

                string? tempNameSnakeCase;
                foreach (var prop in props)
                {
                    tempNameSnakeCase = prop.Name.ToSnakeCase();
                    if (tempNameSnakeCase is null) continue;
                    if (KeyColumns is not null)
                    {
                        if (KeyColumns.Contains(tempNameSnakeCase)) continue;
                    }

                    sb.Append(tempNameSnakeCase);
                    sb.Append("=");
                    var value = prop.GetValue(child, null);
                    if (prop.PropertyType == typeof(string))
                    {
                        if (value is not null)
                        {
                            sb.Append($"{value.ToString()?.Quot()}");
                        }
                        else
                        {
                            sb.Append($"{"".Quot()}");
                        }
                    }
                    else
                    {
                        if (value is not null)
                        {
                            sb.Append($"{value}");
                        }
                        else
                        {
                            sb.Append($"null");
                        }
                    }
                    sb.Append(",");
                }
                sb.Remove(sb.Length - 1, 1);

                if (KeyColumns is not null)
                {
                    for (int keyIte = 0; keyIte < KeyColumns.Count(); keyIte++)
                    {
                        if (keyIte < 1)
                        {
                            sb.Append(" where ");
                        }
                        PropertyInfo? prop = props.Where(x => x.Name.ToSnakeCase() == KeyColumns[keyIte]).FirstOrDefault();
                        if (prop is null) throw new Exception("failed, because prop not found");
                        sb.Append($"{KeyColumns[keyIte]} = ");

                        var value = prop.GetValue(child, null);
                        if (prop.PropertyType == typeof(string))
                        {
                            if (value is not null)
                            {
                                sb.Append($"{value.ToString()?.Quot()}");
                            }
                            else
                            {
                                sb.Append($"{"".Quot()}");
                            }
                        }
                        else
                        {
                            if (value is not null)
                            {
                                sb.Append($"{value}");
                            }
                            else
                            {
                                sb.Append($"null");
                            }
                        }
                        sb.Append(",");
                    }
                    sb.Remove(sb.Length - 1, 1);
                }
            }
            catch (System.Exception exception)
            {
                return exception.Message;
            }

            return sb.ToString();
        }

        private List<PropertyInfo> GetChildProps()
        {
            return ChildType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(JsonIgnoreAttribute)) == false).ToList();
        }
    }
}