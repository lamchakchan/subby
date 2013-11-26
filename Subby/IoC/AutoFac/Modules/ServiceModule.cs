using Autofac;
using Subby.Core.Parser;
using Subby.Core.Parser.Impl;
using Subby.Core.Service;
using Subby.Core.Service.Impl;

namespace Subby.IoC.AutoFac.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DefaultContentTypeService>().As<IContentTypeService>().SingleInstance();
            builder.RegisterType<DefaultFileProcessService>().As<IFileResourceProcessService>().SingleInstance();
            builder.RegisterType<DefaultFilePersistenceService>().As<IFileResultPersistenceService>().SingleInstance();
            builder.RegisterType<DefaultSubstitutionService>().As<ISubstitutionService>().SingleInstance();

            builder.RegisterType<JsonParser>().As<IJsonVariableParser>().SingleInstance();
            builder.RegisterType<XmlParser>().As<IXmlVariableParser>().SingleInstance();
        }
    }
}