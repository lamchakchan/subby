namespace Subby.Core.Enum.Impl
{
    public class HttpMethod : Enumeration<HttpMethod, string>
    {
        public static HttpMethod Get = new HttpMethod("get", "GET");
        public static HttpMethod Post = new HttpMethod("post", "POST");

        public HttpMethod(string value, string displayName) : base(value, displayName)
        {
        }
    }
}