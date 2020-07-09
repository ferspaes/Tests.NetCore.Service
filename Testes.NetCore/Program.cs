using Autofac;
using System;
using Testes.NetCore.Configuration;
using Testes.NetCore.Domain.Interface;
using Topshelf;

namespace Testes.NetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = ContainerConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var exitCode = HostFactory.Run(x =>
                {
                    x.Service<ITestsProcess>(s =>
                    {
                        s.ConstructUsing(tests => scope.Resolve<ITestsProcess>());
                        s.WhenStarted(tests => tests.Start());
                        s.WhenStopped(tests => tests.Stop());
                    });

                    x.RunAsLocalService();
                    x.SetServiceName("TestsService");
                    x.SetDisplayName("Tests.Service");
                    x.SetDescription("TestsProjectService");
                });

                int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
                Environment.ExitCode = exitCodeValue;
            }
        }
    }
}