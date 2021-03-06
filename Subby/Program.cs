﻿using Autofac;
using Autofac.Features.ResolveAnything;
using Subby.IoC.AutoFac.Modules;
using Subby.Service.Impl;

namespace Subby
{
    class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            builder.RegisterModule<RegistrationModule>();
            var container = builder.Build();

            var substitutionServiceWrapper = container.Resolve<SubsitutionServiceWrapper>();
            substitutionServiceWrapper.Process(args);
        }
    }
}
