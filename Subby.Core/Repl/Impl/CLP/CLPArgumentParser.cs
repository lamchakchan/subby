using System;
using Subby.Core.Repl.Impl.CLP.Model;
using Subby.Core.Repl.Model;

namespace Subby.Core.Repl.Impl.CLP
{
    public class CLPArgumentParser : IArgumentParser
    {
        public Options Parse(string[] arguments)
        {
            var result = new CLPOptions();
            var commandLineParser = new CommandLine.Parser();
            commandLineParser.ParseArguments(arguments, result);
            return result;
        }
    }
}
