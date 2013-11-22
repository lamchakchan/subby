namespace Subby.Core.Enum.Impl
{
    public class SourceType : Enumeration<SourceType, string>
    {
        public static SourceType Json = new SourceType("json", "Json");
        public static SourceType NewLineDelimited = new SourceType("nl", "New Line Delimited");
        public static SourceType Xml = new SourceType("xml", "Xml");
        public static SourceType Unknown = new SourceType("unknown", "Unknown");

        public SourceType(string value, string displayName) : base(value, displayName)
        {
        }
    }
}
