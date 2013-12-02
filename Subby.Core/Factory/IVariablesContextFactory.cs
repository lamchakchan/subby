using System.Collections.Generic;
using Subby.Core.Enum.Impl;
using Subby.Core.Model;

namespace Subby.Core.Factory
{
    public interface IVariablesContextFactory<TSource> where TSource : SourceContext
    {
        IList<TSource> Build(SourceType type, IList<string> paths);
    }
}