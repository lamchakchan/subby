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
        private readonly IXmlVariableParser _xmlParser;
        private readonly INewLineVariableParser _newLineParser;

        public DefaultFileProcessService(IJsonVariableParser jsonParser, IXmlVariableParser xmlParser, INewLineVariableParser newLineParser)
        {
            _jsonParser = jsonParser;
            _xmlParser = xmlParser;
            _newLineParser = newLineParser;
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
                    string content;

                    try
                    {
                        content = File.ReadAllText(source.FilePath);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("Error reading source file ({0})", source.FilePath), ex);
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
                else
                {
                    throw new Exception(string.Format("Source file does not exist for ({0})", source.FilePath));
                }
            }

            return result;
        }

        public string Read(FileTargetContext target)
        {
            if (target == null)
            {
                throw new Exception("Context is null");
            }

            if (File.Exists(target.FilePath))
            {
                string content;
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
