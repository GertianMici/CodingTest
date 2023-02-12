using ProArch.CodingTest.Models.Invoices;
using System.Linq;

namespace ProArch.CodingTest.Repositories.Invoices
{
    public interface IInvoiceRepository
    {
        IQueryable<Invoice> Get();
    }
}
