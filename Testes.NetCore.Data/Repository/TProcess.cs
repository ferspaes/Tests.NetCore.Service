using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Testes.NetCore.Domain.Interface;
using Testes.NetCore.Domain.Model;

namespace Testes.NetCore.Data.Repository
{
    public class TProcess : ITProcess
    {
        private readonly ILoggerResultRepository _log;

        public TProcess(ILoggerResultRepository log)
        {
            _log = log;
        }

        public bool LogProcess()
        {
#if DEBUG
            Console.WriteLine();
            Console.WriteLine("-------");
            var dataInicio = DateTime.Now;
#endif
            var baseDirectory = Path.Combine(Path.GetPathRoot(AppDomain.CurrentDomain.BaseDirectory), "Response", "Vks");
            var readDirectory = Path.Combine(baseDirectory, "Read_Directory");
            var logResultsDirectory = Path.Combine(baseDirectory, "Log_Results");
            var processedDirectory = Path.Combine(baseDirectory, "Processed");
            var resultsFileName = $"results_{DateTime.Now:yyyyMMdd}.csv";
            var resultsFileFullPath = Path.Combine(logResultsDirectory, resultsFileName);

            Directory.CreateDirectory(readDirectory);
            Directory.CreateDirectory(processedDirectory);
            Directory.CreateDirectory(logResultsDirectory);

            var files = Directory.GetFiles(readDirectory).ToList();
#if DEBUG
            Console.WriteLine();
            int fileIndex = 0;
            int dataCount = 0;
#endif
            foreach (var file in files.Where(f => f.Contains("_log_", StringComparison.InvariantCultureIgnoreCase)))
            {
#if DEBUG
                fileIndex++;
#endif
                var data = _log.Get<VksWgn>(file);
#if DEBUG
                dataCount += data.Count;
                Console.WriteLine($"{DateTime.Now} - {data.Count} registros encontrados no {fileIndex}º arquivo.");
#endif
                WriteResults(resultsFileFullPath, data);

                File.Move(file, Path.Combine(processedDirectory, Path.GetFileName(file)), true);
            }
#if DEBUG
            var executionTime = DateTime.Now - dataInicio;

            Console.WriteLine();
            Console.WriteLine($"{DateTime.Now} - {files.Count} arquivos encontrados, contendo {dataCount} registros.");
            Console.WriteLine();
            Console.WriteLine($"{DateTime.Now} - Horário de início do processo LogProcess às {dataInicio}.");
            Console.WriteLine($"{DateTime.Now} - Horário de fim    do processo LogProcess às {DateTime.Now}.");
            Console.WriteLine();
            Console.WriteLine($"{DateTime.Now} - Tempo total de processamento: {Math.Round(executionTime.TotalSeconds, 0)} segundos.");
            Console.WriteLine();
            Console.WriteLine("------");
            Console.WriteLine();
#endif
            return true;
        }

        private static void WriteResults(string resultsFileFullPath, List<VksWgn> data)
        {
#if DEBUG
            int index = 0;
            int dataCount = data.Count(d => d.ExceptionThrown || !string.IsNullOrWhiteSpace(d.ExceptionMessage));

            if (dataCount > 0)
                Console.WriteLine();
#endif
            foreach (var item in data.Where(d => d.ExceptionThrown || !string.IsNullOrWhiteSpace(d.ExceptionMessage)))
            {
#if DEBUG
                index++;
                Console.WriteLine($"Lendo registro {index} de {dataCount} com problemas.");
#endif
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