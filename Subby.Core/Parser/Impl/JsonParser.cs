using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Subby.Core.Parser.Impl
{
    public class JsonParser : IJsonVariableParser
    {
        public IDictionary<string, string> Parse(string json)
        {
            try
            {
                var jsonObject = (JObject) JsonConvert.DeserializeObject(json);
                return jsonObject.ToObject<Dictionary<string, string>>();
            }
            catch (JsonException ex)
            {
                throw new Exception("Json content not formatted correctly.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("JsonParser failed", ex);
            }
        }
    }
}
