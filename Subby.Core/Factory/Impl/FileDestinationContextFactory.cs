using System;
using Subby.Core.Model;

namespace Subby.Core.Factory.Impl
{
    public class FileDestinationContextFactory : IFileDestinationContextFactory
    {
        public FileDestinationContext Build(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path can not be empty");
            }

            return new FileDestinationContext
            {
                FilePath = path
            };
        }
    }
}