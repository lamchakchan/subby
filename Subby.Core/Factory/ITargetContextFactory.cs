using Subby.Core.Model;

namespace Subby.Core.Factory
{
    public interface ITargetContextFactory<TTarget> where TTarget : TargetContext
    {
        TTarget Build(string path);
    }
}
