using System;
using System.Collections.Generic;
using System.Linq;
using Subby.Core.Enum.Impl;
using Subby.Core.Model;

namespace Subby.Core.Factory.Impl
{
    public class DefaultVariableContextFactory : IVariablesContextFactory<SourceContext>
    {
        private readonly IFileVariableContextFactory _fileVariableContextFactory;
        private readonly IHttpVariableContextFactory _httpVariableContextFactory;

        public DefaultVariableContextFactory(IFileVariableContextFactory fileVariableContextFactory, IHttpVariableContextFactory httpVariableContextFactory)
        {
            _fileVariableContextFactory = fileVariableContextFactory;
            _httpVariableContextFactory = httpVariableContextFactory;
        }

        public IList<SourceContext> Build(SourceType type, IList<string> paths)
        {
            if (paths == null || !paths.Any())
            {
                throw new ArgumentException("Source file paths must be provided");
            }

            var sourceContexts = new List<SourceContext>();

            foreach (var path in paths)
            {
                if (path.ToLower().StartsWith("http://") || path.ToLower().StartsWith("https://"))
                {
                    sourceContexts.AddRange(_httpVariableContextFactory.Build(type, new[] { path }));
                }
                else
                {
                    sourceContexts.AddRange(_fileVariableContextFactory.Build(type, new[] { path }));
                }
            }

            return sourceContexts;
        }
    }
}