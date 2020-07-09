using System;
using System.Collections.Generic;
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
            var resultsFileName = $"results_{DateTime.Now:yyyyMMdd}.csv";
            var resultsFileFullPath = Path.Combine(logResultsDirectory, resultsFileName);

            Directory.CreateDirectory(readDirectory);
            Directory.CreateDirectory(processedDirectory);
            Directory.CreateDirectory(logResultsDirectory);

            var files = Directory.GetFiles(readDirectory);

            foreach (var file in files.Where(f => f.Contains("_log_", StringComparison.InvariantCultureIgnoreCase)))
            {
                var data = _log.Get<VksWgn>(file);

                WriteResults(resultsFileFullPath, data);

                File.Move(file, Path.Combine(processedDirectory, Path.GetFileName(file)), true);
            }

            return true;
        }

        private static void WriteResults(string resultsFileFullPath, List<VksWgn> data)
        {
            foreach (var item in data.Where(d => d.ExceptionThrown || !string.IsNullOrWhiteSpace(d.ExceptionMessage)))
            {
                if (!File.Exists(resultsFileFullPath))
                {
                    File.WriteAllLines(
                        resultsFileFullPath,
                        new List<string>() { "Id;CustomerName;CustomerId;CustomerEmail;CustomerPhone;LeadSource;CreatedDate;HasErrors;ExceptionThrown;ErrorMessage;ExceptionMessage;Message" });

                    File.AppendAllLines(
                        resultsFileFullPath,
                        new List<string>() { $"{item.Id};{item.CustomerName};{item.CustomerId};{item.CustomerEmail};{item.CustomerPhone};{item.LeadSource};{item.CreatedDate};{item.HasErrors};{item.ExceptionThrown};{item.ErrorMessage};{item.ExceptionMessage};{item.Message}" });
                }
                else
                {
                    File.AppendAllLines(
                        resultsFileFullPath,
                        new List<string>() { $"{item.Id};{item.CustomerName};{item.CustomerId};{item.CustomerEmail};{item.CustomerPhone};{item.LeadSource};{item.CreatedDate};{item.HasErrors};{item.ExceptionThrown};{item.ErrorMessage};{item.ExceptionMessage};{item.Message}" });
                }
            }
        }
    }
}