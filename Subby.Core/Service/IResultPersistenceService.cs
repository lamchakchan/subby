using Subby.Core.Model;

namespace Subby.Core.Service
{
    public interface IResultPersistenceService<TDestination> where TDestination : DestinationContext
    {
        void Write(TDestination destination, string content);
    }
}