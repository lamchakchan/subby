using System.Diagnostics;

namespace Subby.Core.Model
{
    [DebuggerDisplay("Type = {Type.DisplayName}, FilePath = {FilePath}")]
    public class FileSourceContext : SourceContext
    {
        public string FilePath { get; set; }
    }
}