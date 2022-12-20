using System.Text.Json.Serialization;

namespace Shared.ApiResult
{
    public class HttpResult<T>
    {
        [JsonInclude]
        public bool Success { get; set; } = false;
        [JsonInclude]
        public string? Message { get; set; } = "";
        [JsonInclude]
        public T? Result { get; set; }
    }
}