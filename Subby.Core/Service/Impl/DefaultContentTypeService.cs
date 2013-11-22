using System.IO;
using Subby.Core.Enum.Impl;

namespace Subby.Core.Service.Impl
{
    public class DefaultContentTypeService : IContentTypeService
    {
        public SourceType ProcessFilePath(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLower();

            switch (extension)
            {
                case ".js":
                case ".json":
                    return SourceType.Json;
                case ".xml":
                    return SourceType.Xml;
                case ".txt":
                    return SourceType.NewLineDelimited;
                default:
                    return SourceType.Unknown;
            }
        }

        public SourceType ProcessCode(string code)
        {
            return string.IsNullOrEmpty(code) ? SourceType.Unknown : SourceType.FromValue(code.ToLower());
        }
    }
}
