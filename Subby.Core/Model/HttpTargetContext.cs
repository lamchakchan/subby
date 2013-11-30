using System.Diagnostics;

namespace Subby.Core.Model
{
    [DebuggerDisplay("Url = {HttpResource.Url}")]
    public class HttpTargetContext : TargetContext
    {
        public HttpResource HttpResource { get; set; }
    }
}