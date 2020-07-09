using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Testes.NetCore.Domain.Interface;

namespace Testes.NetCore.Controller
{
    public class TestsProcess : ITestsProcess
    {
        private readonly Timer _timer;
        private readonly ILoggerResultRepository _logResult;
        private readonly IVksProcess _vksProcess;

        public TestsProcess(
            ILoggerResultRepository logResult,
            IVksProcess vksProcess)
        {
            _logResult = logResult;
            _vksProcess = vksProcess;
            _timer = new Timer(10000) { AutoReset = true };
            _timer.Elapsed += TimerElapsed;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            BeginProcess();
        }

        private void BeginProcess()
        {
            _vksProcess.LogProcess();
            Console.WriteLine("Hello .NetCore Services");
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