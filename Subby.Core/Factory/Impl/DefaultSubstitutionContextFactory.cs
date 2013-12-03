using System.Collections.Generic;
using Subby.Core.Model;
using Subby.Core.Service;

namespace Subby.Core.Factory.Impl
{
    public class DefaultSubstitutionContextFactory : ISubstitutionContextFactory
    {
        private readonly ICompositeResourceProcessService _resourceProcessService;
        
        public DefaultSubstitutionContextFactory(ICompositeResourceProcessService resourceProcessService)
        {
            _resourceProcessService = resourceProcessService;
        }

        public SubstitutionContext Build(IList<SourceContext> sources, TargetContext target)
        {
            return new SubstitutionContext
                   {
                       Variables = _resourceProcessService.Read(sources),
                       Target = _resourceProcessService.Read(target)
                   };
        }
    }
}
