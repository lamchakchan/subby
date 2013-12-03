using System;
using Subby.Core.Factory;
using Subby.Core.Factory.Impl;
using Subby.Core.Parser;
using Subby.Core.Parser.Impl;
using Subby.Core.Repl;
using Subby.Core.Repl.Impl.CLP;
using Subby.Core.Service;
using Subby.Core.Service.Impl;

namespace Subby.Core.IoC.Impl
{
    public static class IoCHelper
    {
        public static void RegisterTypes(Action<Type, Type> action)
        {
            if (action != null)
            {
                //Factories
                action(typeof (ISubstitutionContextFactory), typeof (DefaultSubstitutionContextFactory));
                action(typeof(IFileVariableContextFactory), typeof(FileVariableContextFactory));
                action(typeof(IHttpVariableContextFactory), typeof(HttpVariableContextFactory));
                action(typeof(ICompositeVariablesContextFactory), typeof(DefaultVariableContextFactory));
                action(typeof(IFileTargetContextFactory), typeof(FileTargetContextFactory));
                action(typeof(IHttpTargetContextFactory), typeof(HttpTargetContextFactory));
                action(typeof(ICompositeTargetContextFactory), typeof(DefaultTargetContextFactory));
                action(typeof(IFileDestinationContextFactory), typeof(FileDestinationContextFactory));

                //Services
                action(typeof(IContentTypeService), typeof(DefaultContentTypeService));
                action(typeof(IFileResourceProcessService), typeof(DefaultFileResourceProcessService));
                action(typeof(IHttpResourceProcessService), typeof(DefaultHttpResourceProcessService));
                action(typeof(ICompositeResourceProcessService), typeof(DefaultResourceProcessService));
                action(typeof(IFileResultPersistenceService), typeof(DefaultFilePersistenceService));
                action(typeof(ISubstitutionService), typeof(DefaultSubstitutionService));
                
                //Parsers
                action(typeof(IJsonVariableParser), typeof(JsonParser));
                action(typeof(IXmlVariableParser), typeof(XmlParser));
                action(typeof(INewLineVariableParser), typeof(NewLineParser));

                //Repl
                action(typeof(IArgumentParser), typeof(CLPArgumentParser));
                action(typeof(IHelpPrinter), typeof(CLPHelpPrinter));
            }
        }
    }
}
