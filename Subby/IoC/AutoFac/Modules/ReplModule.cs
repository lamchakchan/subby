using Autofac;
using Subby.Core.Repl;
using Subby.Core.Repl.Impl.CLP;

namespace Subby.IoC.AutoFac.Modules
{
    public class ReplModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CLPArgumentParser>().As<IArgumentParser>().SingleInstance();
            builder.RegisterType<CLPHelpPrinter>().As<IHelpPrinter>().SingleInstance();
        }
    }
}