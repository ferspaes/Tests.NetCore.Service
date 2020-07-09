using System;

namespace Testes.NetCore.Domain.Model
{
    public class VksWgn
    {
        public long Id { get; set; }
        public string CustomerName { get; set; }
        public long CustomerId { get; set; }
        public string CustomerEmail { get; set; }
        public long CustomerPhone { get; set; }
        public string LeadSource { get; set; }
        public DateTime LeadCreatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool HasErrors { get; set; }
        public bool ExceptionThrown { get; set; }
        public string ErrorMessage { get; set; }
        public string ExceptionMessage { get; set; }
        public string Message { get; set; }
        public bool Duplicated { get; set; }
    }
}