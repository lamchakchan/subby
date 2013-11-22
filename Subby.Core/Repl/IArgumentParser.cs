using Subby.Core.Repl.Model;

namespace Subby.Core.Repl
{
    public interface IArgumentParser
    {
        Options Parse(string[] arguments);
    }
}
