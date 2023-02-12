using ProArch.CodingTest.Models.Invoices;
using ProArch.CodingTest.Repositories.Invoices;
using System.Linq;

namespace ProArch.CodingTest.Services.Foundations.Invoices
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository) =>
            this.invoiceRepository = invoiceRepository;

        public IQueryable<Invoice> Get()
        {
            return this.invoiceRepository.Get();
        }
    }
}
