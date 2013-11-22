using System;
using Subby.Core.Model;

namespace Subby.Core.Factory.Impl
{
    public class FileTargetContextFactory : IFileTargetContextFactory
    {
        public FileTargetContext Build(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path can not be empty");
            }

            return new FileTargetContext
            {
                FilePath = path
            };
        }
    }
}
