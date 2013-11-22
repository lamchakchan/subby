using Autofac;
using Subby.Core.Factory;
using Subby.Core.Factory.Impl;

namespace Subby.IoC.AutoFac.Modules
{
    public class FactoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DefaultSubstitutionContextFactory>().As<ISubstitutionContextFactory>().SingleInstance();
            builder.RegisterType<FileVariableContextFactory>().As<IFileVariableContextFactory>().SingleInstance();
            builder.RegisterType<FileTargetContextFactory>().As<IFileTargetContextFactory>().SingleInstance();
            builder.RegisterType<FileDestinationContextFactory>().As<IFileDestinationContextFactory>().SingleInstance();
        }
    }
}
