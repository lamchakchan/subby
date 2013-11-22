using Subby.Core.Model;

namespace Subby.Core.Factory
{
    public interface IDestinationContextFactory<TDestination> where TDestination : DestinationContext
    {
        TDestination Build(string path);
    }
}