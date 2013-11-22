using System.Diagnostics;

namespace Subby.Core.Model
{
    [DebuggerDisplay("FilePath = {FilePath}")]
    public class FileDestinationContext : DestinationContext
    {
        public string FilePath { get; set; }
    }
}