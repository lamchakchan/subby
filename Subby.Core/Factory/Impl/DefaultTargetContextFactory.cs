using Subby.Core.Extensions;
using Subby.Core.Model;

namespace Subby.Core.Factory.Impl
{
    public class DefaultTargetContextFactory : ICompositeTargetContextFactory
    {
        private readonly IFileTargetContextFactory _fileTargetContextFactory;
        private readonly IHttpTargetContextFactory _httpTargetContextFactory;

        public DefaultTargetContextFactory(IFileTargetContextFactory fileTargetContextFactory, IHttpTargetContextFactory httpTargetContextFactory)
        {
            _fileTargetContextFactory = fileTargetContextFactory;
            _httpTargetContextFactory = httpTargetContextFactory;
        }

        public TargetContext Build(string path)
        {
            TargetContext result;

            if (path.IsUrl())
            {
                result = _httpTargetContextFactory.Build(path);
            }
            else
            {
                result = _fileTargetContextFactory.Build(path);
            }

            return result;
        }
    }
}