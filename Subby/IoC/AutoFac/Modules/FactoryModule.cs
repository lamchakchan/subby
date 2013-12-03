﻿using Autofac;
using Subby.Core.Factory;
using Subby.Core.Factory.Impl;
using Subby.Core.Model;

namespace Subby.IoC.AutoFac.Modules
{
    public class FactoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DefaultSubstitutionContextFactory>().As<ISubstitutionContextFactory>().SingleInstance();
            builder.RegisterType<FileVariableContextFactory>().As<IFileVariableContextFactory>().SingleInstance();
            builder.RegisterType<HttpVariableContextFactory>().As<IHttpVariableContextFactory>().SingleInstance();
            builder.RegisterType<DefaultVariableContextFactory>().As<ICompositeVariablesContextFactory>().SingleInstance();
            builder.RegisterType<FileTargetContextFactory>().As<IFileTargetContextFactory>().SingleInstance();
            builder.RegisterType<HttpTargetContextFactory>().As<IHttpTargetContextFactory>().SingleInstance();
            builder.RegisterType<DefaultTargetContextFactory>().As<ICompositeTargetContextFactory>().SingleInstance();
            builder.RegisterType<FileDestinationContextFactory>().As<IFileDestinationContextFactory>().SingleInstance();
        }
    }
}
