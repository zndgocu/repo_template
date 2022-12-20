using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shared.Global
{
    public static class GlobalJsonOption
    {
        public static JsonSerializerOptions GetJsonOptionUncheckUpperLower()
        {
            return new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }
        public static JsonSerializerOptions GetJsonOptionCamelCase()
        {
            return new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
    }
}