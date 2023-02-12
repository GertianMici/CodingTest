using ProArch.CodingTest.Models.FailoverInvoices;

namespace ProArch.CodingTest.Services.Foundations.FailoverInvoices
{
    public class FailoverInvoiceService : IFailoverInvoiceService
    {
        public FailoverInvoiceCollection GetInvoices(int supplierId)
        {
            return new FailoverInvoiceCollection();
        }
    }
}
