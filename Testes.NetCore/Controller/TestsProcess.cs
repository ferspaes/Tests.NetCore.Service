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

        private void BeginProcess()
        {
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