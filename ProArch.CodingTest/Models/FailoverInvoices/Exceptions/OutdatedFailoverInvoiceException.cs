using System;

namespace ProArch.CodingTest.Models.FailoverInvoices.Exceptions
{
    [Serializable]
    public class OutdatedFailoverInvoiceException : Exception
    {
        public OutdatedFailoverInvoiceException(DateTime timestamp, Exception innerException)
            : base(
                  message: $"Failover invoices were last refreshed on {timestamp}",
                  innerException)
        { }
    }
}
