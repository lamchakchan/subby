using System;
using System.Collections.Generic;
using System.Linq;
using Subby.Core.Enum.Impl;
using Subby.Core.Model;
using Subby.Core.Service;

namespace Subby.Core.Factory.Impl
{
    public class HttpVariableContextFactory : IHttpVariableContextFactory
    {
        private readonly IContentTypeService _contentTypeService;

        public HttpVariableContextFactory(IContentTypeService contentTypeService)
        {
            _contentTypeService = contentTypeService;
        }

        public IList<HttpSourceContext> Build(SourceType type, IList<string> paths)
        {
            if (paths == null || !paths.Any())
            {
                throw new ArgumentException("Source file paths must be provided");
            }

            List<HttpSourceContext> result = null;

            if (type == null || type.Value == SourceType.Unknown.Value)
            {
                result = paths.Select(p =>
                    new HttpSourceContext
                    {
                        Type = _contentTypeService.ProcessUrlPath(p),
                        HttpResource = new HttpResource{Url = p}
                    }
                    ).ToList();

            }
            else
            {
                result = paths.Select(p => new HttpSourceContext
                                           {
                                               Type = type,
                                               HttpResource = new HttpResource { Url = p }
                                           }).ToList();
            }

            return result;
        }
    }
}