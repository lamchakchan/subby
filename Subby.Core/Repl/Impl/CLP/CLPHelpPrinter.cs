using Subby.Core.Repl.Impl.CLP.Model;

namespace Subby.Core.Repl.Impl.CLP
{
    public class CLPHelpPrinter : IHelpPrinter
    {
        public string PrintHelp()
        {
            var result = new CLPOptions();
            return result.GetUsage();
        }
    }
}