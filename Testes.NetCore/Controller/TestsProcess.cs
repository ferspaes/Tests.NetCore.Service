using System;
using System.Net;
using System.Text;
using System.Timers;
using Testes.NetCore.Domain.Interface;

namespace Testes.NetCore.Controller
{
    public class TestsProcess : ITestsProcess
    {
        private readonly Timer _timer;
        private readonly ILoggerResultRepository _logResult;
        private readonly ITProcess _tProcess;
        private readonly ITestProject _testProject;

        public TestsProcess(
            ILoggerResultRepository logResult,
            ITProcess tProcess,
            ITestProject testProject)
        {
            _testProject = testProject;
            _logResult = logResult;
            _tProcess = tProcess;
            _timer = new Timer(10000) { AutoReset = true };
            _timer.Elapsed += TimerElapsed;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            BeginProcess();
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (client.OpenRead("http://google.com/generate_204"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private void BeginProcess()
        {
            while (!CheckForInternetConnection())
            {
                System.Threading.Thread.Sleep(1000);
            }

            var senha = Convert.ToBase64String(Encoding.UTF8.GetBytes("sistemas"));

            var key = Encoding.UTF8.GetString(Convert.FromBase64String(senha));

            var dataIni = DateTimeOffset.Parse("17/07/2020").ToUnixTimeSeconds();
            var dateUnix = DateTimeOffset.Now.ToUnixTimeSeconds();
            _tProcess.LogProcess();
            _testProject.SendEmail();
            _testProject.TratarTelefones();
        }

        public void Start()
        {
#if DEBUG
            BeginProcess();
#else
            _timer.Start();
#endif
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}