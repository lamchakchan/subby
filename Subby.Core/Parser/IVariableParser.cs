using System.Collections.Generic;

namespace Subby.Core.Parser
{
    public interface IVariableParser
    {
        IDictionary<string, string> Parse(string content);
    }
}
