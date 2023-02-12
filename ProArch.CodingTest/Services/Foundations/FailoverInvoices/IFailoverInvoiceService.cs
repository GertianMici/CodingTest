using ProArch.CodingTest.Models.FailoverInvoices;

namespace ProArch.CodingTest.Services.Foundations.FailoverInvoices
{
    public interface IFailoverInvoiceService
    {
        FailoverInvoiceCollection GetInvoices(int supplierId);
    }
}
