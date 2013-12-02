using Subby.Core.Enum.Impl;

namespace Subby.Core.Service
{
    public interface IContentTypeService
    {
        SourceType ProcessFilePath(string filePath);
        SourceType ProcessUrlPath(string urlPath);
        SourceType ProcessCode(string code);
    }
}
