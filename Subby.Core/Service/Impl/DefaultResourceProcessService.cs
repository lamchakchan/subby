using System.Collections.Generic;
using System.Linq;
using Subby.Core.Extensions;
using Subby.Core.Model;

namespace Subby.Core.Service.Impl
{
    public class DefaultResourceProcessService : ICompositeResourceProcessService
    {
        private readonly IFileResourceProcessService _fileResourceProcessService;
        private readonly IHttpResourceProcessService _httpResourceProcessService;

        public DefaultResourceProcessService(IFileResourceProcessService fileResourceProcessService, IHttpResourceProcessService httpResourceProcessService)
        {
            _fileResourceProcessService = fileResourceProcessService;
            _httpResourceProcessService = httpResourceProcessService;
        }

        public IDictionary<string, string> Read(IList<SourceContext> sources)
        {
            var result = new Dictionary<string, string>();

            var fileContextSources = sources.Where(p => p is FileSourceContext).Cast<FileSourceContext>().ToList();
            var httpContextSources = sources.Where(p => p is HttpSourceContext).Cast<HttpSourceContext>().ToList();

            if (fileContextSources.Any())
            {
                result = result.MergeLeft(_fileResourceProcessService.Read(fileContextSources));
            }
            if (httpContextSources.Any())
            {
                result = result.MergeLeft(_httpResourceProcessService.Read(httpContextSources));
            }

            return result;
        }

        public string Read(TargetContext target)
        {
            string result = null;

            if (target is FileTargetContext)
            {
                result = _fileResourceProcessService.Read(target as FileTargetContext);
            }
            if (target is HttpTargetContext)
            {
                result = _httpResourceProcessService.Read(target as HttpTargetContext);
            }

            return result;
        }
    }
}