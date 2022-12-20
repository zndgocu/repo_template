using System.Data;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Extensions.Extension;

namespace EntityHelper;
public class EntityConverter
{
    public static List<T> GetEntites<T>(DataTable dt, bool isSnakeCase) where T : class, new()
    {
        List<T> entities = new List<T>();
        foreach (DataRow row in dt.Rows)
        {
            T entity = GetEntity<T>(row, isSnakeCase);
            if (entity != null)
            {
                entities.Add(entity);
            }
        }
        return entities;
    }

    public static T GetEntity<T>(DataRow dr, bool isSnakeCase = false, bool caseInsensitive = true) where T : class, new()
    {
        try
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            var props = temp.GetProperties()?.Where(prop => Attribute.IsDefined(prop, typeof(JsonIgnoreAttribute)) == false).ToList();
            if (props is not null && props.Count > 0)
            {
                for (var ite = 0; ite < dr.Table.Columns.Count; ite++)
                {
                    var columnName = (caseInsensitive) ? dr.Table.Columns[ite].ColumnName.ToUpper() : dr.Table.Columns[ite].ColumnName;
                    PropertyInfo? prop;
                    if (isSnakeCase)
                    {
                        if (caseInsensitive)
                        {
                            prop = props.Where(x => x.Name.ToSnakeCase().ToUpper() == columnName).FirstOrDefault();
                        }
                        else
                        {
                            prop = props.Where(x => x.Name.ToSnakeCase() == columnName).FirstOrDefault();
                        }
                    }
                    else
                    {
                        if (caseInsensitive)
                        {
                            prop = props.Where(x => x.Name.ToUpper() == columnName).FirstOrDefault();
                        }
                        else
                        {
                            prop = props.Where(x => x.Name == columnName).FirstOrDefault();
                        }
                    }

                    if (prop is null) continue;

                    var propName = (caseInsensitive) ? (isSnakeCase) ? prop.Name.ToSnakeCase().ToUpper() : prop.Name.ToUpper()
                                                     : (isSnakeCase) ? prop.Name.ToSnakeCase() : prop.Name;
                    if (propName is null) continue;

                    if (dr[dr.Table.Columns[ite].ColumnName].GetType() != typeof(DBNull))
                    {
                        prop.SetValue(obj, dr[dr.Table.Columns[ite].ColumnName], null);
                    }
                }
            }
            return obj;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            return new();
        }
    }
    public static string? GetJsonPropertyName(Type type, string propertyName)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type));

        return type.GetProperty(propertyName)?.GetCustomAttribute(typeof(JsonPropertyNameAttribute))?.ToString();
    }

    public static T GetDictionaryToData<T>(Dictionary<string, string> dict) where T : notnull, new()
    {
        try
        {
            if (dict == null) return new();
            var type = typeof(T);
            var typeObject = Activator.CreateInstance(type);
            if (typeObject is null) throw new Exception("typeobject null");
            foreach (var pair in dict)
            {
                PropertyInfo? prop = type.GetProperty(pair.Key, BindingFlags.Public | BindingFlags.Instance);
                if (prop == null) continue;
                var propTypVal = Convert.ChangeType(pair.Value, prop.PropertyType);
                prop.SetValue(typeObject, propTypVal, null);
            }
            return (T)typeObject;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            return new();
        }
    }

    public static List<Dictionary<string, string>>? GetDataDictionarys<T>(List<T> items)
    {
        try
        {
            if (items == null) throw new Exception();
            if (items.Count < 1) throw new Exception();
            List<Dictionary<string, string>> datas = new List<Dictionary<string, string>>();
            foreach (var item in items)
            {
                if (item is not null)
                {
                    Dictionary<string, string> data = new Dictionary<string, string>();

                    var Props = typeof(T).GetProperties()?.Where(prop => Attribute.IsDefined(prop, typeof(JsonIgnoreAttribute)) == false).ToList();
                    if (Props is null) throw new Exception();
                    foreach (var prop in Props)
                    {
                        var propValue = prop.GetValue(item)?.ToString();
                        if (propValue is not null)
                        {
                            data.Add(prop.Name, propValue);
                        }
                        else
                        {
                            data.Add(prop.Name, "");
                        }
                    }
                    datas.Add(data);
                }
            }
            return datas;
        }
        catch (System.Exception)
        {
            return new();
        }
    }

    public static Dictionary<string, string> GetDataDictionary<T>(T item)
    {
        try
        {
            if (item == null) throw new Exception();
            Dictionary<string, string> data = new Dictionary<string, string>();

            var Props = item.GetType().GetProperties()?.Where(prop => Attribute.IsDefined(prop, typeof(JsonIgnoreAttribute)) == false).ToList();
            if (Props is null) throw new Exception();
            foreach (var prop in Props)
            {
                var propValue = prop.GetValue(item)?.ToString();
                if (propValue is not null)
                {
                    data.Add(prop.Name, propValue);
                }
                else
                {
                    data.Add(prop.Name, "");
                }
            }
            return data;
        }
        catch (System.Exception)
        {
            return new();
        }
    }

    public static string[]? GetProps<T>()
    {
        var Props = typeof(T).GetProperties()?.Where(prop => Attribute.IsDefined(prop, typeof(JsonIgnoreAttribute)) == false).ToList();
        if (Props is null) return null;
        return Props.Select(x => x.Name).ToArray();
    }

    public static string[]? GetPropNames(Type type)
    {
        var props = type.GetProperties()?.Where(prop => Attribute.IsDefined(prop, typeof(JsonIgnoreAttribute)) == false).ToList();
        if (props is null) return null;
        return props.Select(x => x.Name).ToArray();
    }


    public static Dictionary<string, Type>? GetNameTypeDictionaryTableEntity(Type type)
    {
        var props = type.GetProperties()?.Where(prop => Attribute.IsDefined(prop, typeof(JsonIgnoreAttribute)) == false).ToList();
        if (props is not null)
        {
            Dictionary<string, Type> result = new();
            foreach (var prop in props)
            {
                result.Add(prop.Name, prop.PropertyType);
            }
            return result;
        }
        return null;
    }

    public static bool SetEntity(ref object entity, Type entityType, Dictionary<string, string> create)
    {
        try
        {
            var castedEntity = Convert.ChangeType(entity, entityType);
            var castedEntityProps = castedEntity.GetType().GetProperties()?.Where(prop => Attribute.IsDefined(prop, typeof(JsonIgnoreAttribute)) == false).ToList();
            if (castedEntityProps is null) return false;
            foreach (var castedEntityProp in castedEntityProps)
            {
                string? toSetValue;
                if (create.TryGetValue(castedEntityProp.Name, out toSetValue) == false) throw new Exception();
                var castedEntityPropNullableUnderLyingType = Nullable.GetUnderlyingType(castedEntityProp.PropertyType);
                if ((castedEntityProp.PropertyType == typeof(string)) || (castedEntityPropNullableUnderLyingType == typeof(string)))
                {
                    if (castedEntityPropNullableUnderLyingType is not null)
                    {
                        castedEntityProp.SetValue(castedEntity, toSetValue, null);
                    }
                    else
                    {
                        castedEntityProp.SetValue(castedEntity, toSetValue, null);
                    }
                }
                else if ((castedEntityProp.PropertyType == typeof(Decimal)) || (castedEntityPropNullableUnderLyingType == typeof(Decimal)))
                {
                    Decimal decimalValue;
                    if (Decimal.TryParse(toSetValue, out decimalValue) == true)
                    {
                        castedEntityProp.SetValue(castedEntity, decimalValue, null);
                    }
                }
                else if ((castedEntityProp.PropertyType == typeof(int)) || (castedEntityPropNullableUnderLyingType == typeof(int)))
                {
                    int intValue;
                    if (int.TryParse(toSetValue, out intValue) == true)
                    {
                        castedEntityProp.SetValue(castedEntity, intValue, null);
                    }
                }
                else if ((castedEntityProp.PropertyType == typeof(bool)) || (castedEntityPropNullableUnderLyingType == typeof(bool)))
                {
                    bool boolValue;
                    if (bool.TryParse(toSetValue, out boolValue) == true)
                    {
                        castedEntityProp.SetValue(castedEntity, boolValue, null);
                    }
                }
                else
                {
                    if (castedEntityPropNullableUnderLyingType is not null)
                    {
                        castedEntityProp.SetValue(castedEntity, Convert.ChangeType(toSetValue, castedEntityPropNullableUnderLyingType), null);
                    }
                    else
                    {
                        castedEntityProp.SetValue(castedEntity, Convert.ChangeType(toSetValue, castedEntityProp.PropertyType), null);
                    }
                }
            }
            return true;
        }
        catch (System.Exception exception)
        {
            Console.WriteLine(exception.Message);
            return false;
        }
    }

}
