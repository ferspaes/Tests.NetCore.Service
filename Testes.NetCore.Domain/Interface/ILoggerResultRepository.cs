using System.Collections.Generic;
using Testes.NetCore.Domain.Model;

namespace Testes.NetCore.Domain.Interface
{
    public interface ILoggerResultRepository
    {
        void LogResultVks(VksWgn vks);
        List<T> Get<T>(string path);
    }
}