using System.Diagnostics;

namespace Subby.Core.Model
{
    [DebuggerDisplay("Type = {Type.DisplayName}, Url = {HttpResource.Url}")]
    public class HttpSourceContext : SourceContext
    {
        public HttpResource HttpResource { get; set; }
    }
}