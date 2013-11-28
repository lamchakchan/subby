using System;
using System.Collections.Generic;

namespace Subby.Core.Parser.Impl
{
    public class NewLineParser : INewLineVariableParser
    {
        public IDictionary<string, string> Parse(string content)
        {
            try
            {
                var lines = content.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
                var result = new Dictionary<string, string>();

                for (var i = 0; (i + 1) < lines.Length; i = i + 2)
                {
                    result.Add(lines[i], lines[i + 1]);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("NewLineParser failed", ex);
            }
        }
    }
}