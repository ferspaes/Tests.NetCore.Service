using System;
using System.Collections.Generic;
using System.Text;
using Testes.NetCore.Domain.Interface;
using Testes.NetCore.Domain.Model;

namespace Testes.NetCore.Data.Repository
{
    public class LoggerResultRepository : ILoggerResultRepository
    {
        public void LogResultVolks(LogVolks logVolks)
        {
            Console.WriteLine("Hello Logger");
        }
    }
}