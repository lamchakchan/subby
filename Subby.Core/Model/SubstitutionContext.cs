using System.Collections.Generic;
using System.Diagnostics;

namespace Subby.Core.Model
{
    [DebuggerDisplay("VariablesCount = {Variables.Count}")]
    public class SubstitutionContext
    {
        public IDictionary<string, string> Variables { get; set; }
        public string Target { get; set; }
        public string Result { get; set; }
    }
}
