using Subby.Core.Enum.Impl;

namespace Subby.Core.Model
{
    public class HttpResource
    {
        public HttpResource()
        {
            Action = HttpMethod.Get;
        }

        public string Url { get; set; }
        public HttpMethod Action { get; set; }
    }
}