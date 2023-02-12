using ProArch.CodingTest.Models.Invoices;
using System.Linq;

namespace ProArch.CodingTest.Services.Foundations.Invoices
{
    public interface IInvoiceService
    {
        IQueryable<Invoice> Get();
    }
}
