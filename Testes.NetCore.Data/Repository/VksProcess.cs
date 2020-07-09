using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Testes.NetCore.Domain.Interface;
using Testes.NetCore.Domain.Model;

namespace Testes.NetCore.Data.Repository
{
    public class VksProcess : IVksProcess
    {
        private readonly ILoggerResultRepository _log;

        public VksProcess(ILoggerResultRepository log)
        {
            _log = log;
        }

        public bool LogProcess()
        {
            var baseDirectory = Path.Combine(Path.GetPathRoot(AppDomain.CurrentDomain.BaseDirectory), "Response", "Vks");
            var readDirectory = Path.Combine(baseDirectory, "Read_Directory");
            var logResultsDirectory = Path.Combine(baseDirectory, "Log_Results");
            var processedDirectory = Path.Combine(baseDirectory, "Processed");
            var resultsFileName = $"results_{DateTime.Now:yyyyMMss}.log";
            var resultsFileFullPath = Path.Combine(logResultsDirectory, resultsFileName);

            Directory.CreateDirectory(baseDirectory);

            var files = Directory.GetFiles(readDirectory);

            foreach (var file in files.Where(f => f.Contains("_log_", StringComparison.InvariantCultureIgnoreCase)))
            {
                var fileDirectory = Path.Combine(readDirectory, file);
                var data = _log.Get<VksWgn>(fileDirectory);

            }


            return true;
        }
    }
}