using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Subby.Core.Enum.Impl;
using Subby.Core.Model;
using Subby.Core.Parser;

namespace Subby.Core.Service.Impl
{
    public class DefaultHttpProcessService : IHttpResourceProcessService
    {
        private readonly IJsonVariableParser _jsonParser;
        private readonly IXmlVariableParser _xmlParser;
        private readonly INewLineVariableParser _newLineParser;

        public DefaultHttpProcessService(IJsonVariableParser jsonParser, IXmlVariableParser xmlParser, INewLineVariableParser newLineParser)
        {
            _jsonParser = jsonParser;
            _xmlParser = xmlParser;
            _newLineParser = newLineParser;
        }

        public IDictionary<string, string> Read(IList<HttpSourceContext> sources)
        {
            if (sources == null)
            {
                throw new Exception("Context is null");
            }
            if (!sources.Any())
            {
                throw new Exception("Context is empty");
            }

            var result = new Dictionary<string, string>();

            foreach (var source in sources)
            {

                string content;

                try
                {
                    using (var webClient = new WebClient())
                    {
                        content = source.HttpResource.Action.Value == HttpMethod.Get.Value
                            ? webClient.DownloadString(source.HttpResource.Url)
                            : webClient.UploadString(source.HttpResource.Url, string.Empty);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Error reading source file ({0})", source.HttpResource.Url), ex);
                }

                if (source.Type.Value == SourceType.Json.Value)
                {
                    result = result.Union(_jsonParser.Parse(content)).ToDictionary(p => p.Key, p => p.Value);
                }
                else if (source.Type.Value == SourceType.Xml.Value)
                {
                    result = result.Union(_xmlParser.Parse(content)).ToDictionary(p => p.Key, p => p.Value);
                }
                else if (source.Type.Value == SourceType.NewLineDelimited.Value)
                {
                    result = result.Union(_newLineParser.Parse(content)).ToDictionary(p => p.Key, p => p.Value);
                }
                else
                {
                    throw new Exception(string.Format("No parser implemented for this content type ({0})",
                        source.Type.DisplayName));
                }
            }

            return result;
        }

        public string Read(HttpTargetContext target)
        {
            if (target == null)
            {
                throw new Exception("Context is null");
            }

            try
            {
                using (var webClient = new WebClient())
                {
                    return target.HttpResource.Action.Value == HttpMethod.Get.Value
                        ? webClient.DownloadString(target.HttpResource.Url)
                        : webClient.UploadString(target.HttpResource.Url, string.Empty);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading target file", ex);
            }
        }
    }
}