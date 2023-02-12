using ProArch.CodingTest.External;

namespace ProArch.CodingTest.Services.Foundations.ExternalInvoices
{
    public interface IExternalInvoiceServices
    {
        ExternalInvoice[] GetInvoices(string supplierId);
    }
}
