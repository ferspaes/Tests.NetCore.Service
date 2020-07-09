using Autofac;
using Testes.NetCore.Controller;
using Testes.NetCore.Data.Repository;
using Testes.NetCore.Domain.Interface;

namespace Testes.NetCore.Configuration
{
    public class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<LoggerResultRepository>().As<ILoggerResultRepository>();
            builder.RegisterType<TestsProcess>().As<ITestsProcess>();
            builder.RegisterType<VksProcess>().As<IVksProcess>();

            return builder.Build();
        }
    }
}