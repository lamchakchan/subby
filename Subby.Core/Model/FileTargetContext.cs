using System.Diagnostics;

namespace Subby.Core.Model
{
    [DebuggerDisplay("FilePath = {FilePath}")]
    public class FileTargetContext : TargetContext
    {
        public string FilePath { get; set; }
    }
}