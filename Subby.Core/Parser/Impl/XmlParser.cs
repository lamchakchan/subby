using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Subby.Core.Parser.Impl
{
    public class XmlParser : IXmlVariableParser
    {
        public IDictionary<string, string> Parse(string content)
        {
            try
            {
                var rootElement = XElement.Parse(content);
                return rootElement.Descendants("item")
                    .ToDictionary(x => (string)x.Attribute("name"), p => (string)p.Attribute("value"));
            }
            catch (XmlException ex)
            {
                throw new Exception("Xml content not formatted correctly.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("XmlParser failed", ex);
            }
        }
    }
}