using Autofac;
using Subby.Core.IoC.Impl;

namespace Subby.IoC.AutoFac.Modules
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            IoCHelper.RegisterTypes((interfaceType, concreteType) => builder.RegisterType(concreteType).As(interfaceType).SingleInstance());
        }
    }
}