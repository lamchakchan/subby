using System.Collections.Generic;
using Subby.Core.Model;
using Subby.Core.Service;

namespace Subby.Core.Factory.Impl
{
    public class DefaultSubstitutionContextFactory : ISubstitutionContextFactory
    {
        private readonly IFileResourceProcessService _fileResourceProcessService;
        
        public DefaultSubstitutionContextFactory(IFileResourceProcessService fileResourceProcessService)
        {
            _fileResourceProcessService = fileResourceProcessService;
        }

        public SubstitutionContext Build(IList<FileSourceContext> sources, FileTargetContext target)
        {
            return new SubstitutionContext
            {
                Variables = _fileResourceProcessService.Read(sources),
                Target = _fileResourceProcessService.Read(target),
            };
        }
    }
}
