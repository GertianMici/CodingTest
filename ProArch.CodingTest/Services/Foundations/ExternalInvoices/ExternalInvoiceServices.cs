using ProArch.CodingTest.External;

namespace ProArch.CodingTest.Services.Foundations.ExternalInvoices
{
    public class ExternalInvoiceServices : IExternalInvoiceServices
    {
        public ExternalInvoice[] GetInvoices(string supplierId) =>
            ExternalInvoiceService.GetInvoices(supplierId);
    }
}
