using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Subby.Core.Enum.Impl;
using Subby.Core.Model;
using Subby.Core.Service;

namespace Subby.Core.Factory.Impl
{
    public class FileVariableContextFactory : IFileVariableContextFactory
    {
        private readonly IContentTypeService _contentTypeService;

        public FileVariableContextFactory(IContentTypeService contentTypeService)
        {
            _contentTypeService = contentTypeService;
        }

        public IList<FileSourceContext> Build(SourceType type, IList<string> paths)
        {
            if (paths == null || !paths.Any())
            {
                throw new ArgumentException("Source file paths must be provided");
            }

            List<FileSourceContext> result = null;

            if (type == null || type.Value == SourceType.Unknown.Value)
            {
                result = paths.Select(p =>
                    new FileSourceContext
                    {
                        Type = _contentTypeService.ProcessFilePath(p),
                        FilePath = p
                    }
                ).ToList();

            }
            else
            {
                result = paths.Select(p => new FileSourceContext
                {
                    Type = type,
                    FilePath = p
                }).ToList();
            }

            return result;
        }
    }
}
