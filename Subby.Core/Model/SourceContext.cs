using System.Diagnostics;
using Subby.Core.Enum.Impl;

namespace Subby.Core.Model
{
    [DebuggerDisplay("Type = {Type.DisplayName}")]
    public class SourceContext
    {
        public SourceType Type { get; set; }
    }
}