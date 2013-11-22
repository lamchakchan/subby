using Subby.Core.Enum.Impl;

namespace Subby.Core.Service
{
    public interface IContentTypeService
    {
        SourceType ProcessFilePath(string filePath);
        SourceType ProcessCode(string code);
    }
}
