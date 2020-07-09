using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Testes.NetCore.Domain.Interface;
using Testes.NetCore.Domain.Model;

namespace Testes.NetCore.Data.Repository
{
    public class LoggerResultRepository : ILoggerResultRepository
    {
        public List<T> Get<T>(string path) =>
            JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(path));

        public void LogResultVks(VksWgn vks)
        {
            Console.WriteLine("Hello Logger");
        }
    }
}