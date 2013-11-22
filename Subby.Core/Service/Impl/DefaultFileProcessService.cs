using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Subby.Core.Enum.Impl;
using Subby.Core.Model;
using Subby.Core.Parser;

namespace Subby.Core.Service.Impl
{
    public class DefaultFileProcessService : IFileResourceProcessService
    {
        private readonly IJsonVariableParser _jsonParser;

        public DefaultFileProcessService(IJsonVariableParser jsonParser)
        {
            _jsonParser = jsonParser;
        }

        public IDictionary<string, string> Read(IList<FileSourceContext> sources)
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
                if (File.Exists(source.FilePath))
                {
                    var content = string.Empty;

                    try
                    {
                        content = File.ReadAllText(source.FilePath);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error reading source file", ex);
                    }

                    if (source.Type == SourceType.Json)
                    {
                        result = result.Union(_jsonParser.Parse(content)).ToDictionary(p => p.Key, p => p.Value);
                    }
                    else if (source.Type == SourceType.Xml)
                    {
                        throw new NotImplementedException("Xml parser not implemented");
                    }
                    else if (source.Type == SourceType.NewLineDelimited)
                    {
                        throw new NotImplementedException("New line delimited parser not implemented");
                    }
                    else
                    {
                        throw new Exception(string.Format("No parser implemented for this content type ({0})",
                            source.Type.DisplayName));
                    }
                }
                else
                {
                    throw new Exception(string.Format("Source file does not exist for ({0})", source.FilePath));
                }
            }

            return result;
        }

        public string Read(FileTargetContext target)
        {
            if (File.Exists(target.FilePath))
            {
                var content = string.Empty;
                try
                {
                    content = File.ReadAllText(target.FilePath);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error reading target file", ex);
                }

                return content;
            }

            throw new Exception("Target file does not exist");
        }
    }
}
