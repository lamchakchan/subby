using System.Collections.Generic;
using Subby.Core.Model;

namespace Subby.Core.Service
{
    public interface IResourceProcessService<TSource, TTarget> where TSource : SourceContext where TTarget : TargetContext
    {
        IDictionary<string, string> Read(IList<TSource> sources);
        string Read(TTarget target);
    }
}