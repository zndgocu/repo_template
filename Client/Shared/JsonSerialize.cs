using System.Text.Json;
using Shared.Global;

namespace blazor_wasm.Client.Shared
{
    public static class JsonSerialize
    {
        public static string SerializeDefault<T>(T item)
        {
            try
            {
                return JsonSerializer.Serialize(item);
            }
            catch (System.Exception exception)
            {
                throw new Exception(exception.Message); ;
            }
        }

        public static T? DeSerializeDefault<T>(string content)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(content, GlobalJsonOption.GetJsonOptionUncheckUpperLower());
            }
            catch (System.Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static object? DeSerializeDefault(string content, Type type)
        {
            try
            {
                return JsonSerializer.Deserialize(content, type, GlobalJsonOption.GetJsonOptionUncheckUpperLower());
            }
            catch (System.Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}