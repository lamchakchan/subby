namespace Subby.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsUrl(this string source)
        {
            return source.ToLower().StartsWith("http://") || source.ToLower().StartsWith("https://");
        }
    }
}