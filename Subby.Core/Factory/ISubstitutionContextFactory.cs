using System.Collections.Generic;
using Subby.Core.Model;

namespace Subby.Core.Factory
{
    public interface ISubstitutionContextFactory
    {
        SubstitutionContext Build(IList<FileSourceContext> sources, FileTargetContext target);
    }
}
