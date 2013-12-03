using System;
using Subby.Core.Extensions;
using Subby.Core.Model;

namespace Subby.Core.Factory.Impl
{
    public class HttpTargetContextFactory : IHttpTargetContextFactory
    {
        public HttpTargetContext Build(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path can not be empty");
            }

            if(!path.IsUrl())
            {
                throw new ArgumentException("Path is not a url");
            }

            return new HttpTargetContext
                   {
                       HttpResource = new HttpResource
                                      {
                                          Url = path
                                      }
                   };
        }
    }
}