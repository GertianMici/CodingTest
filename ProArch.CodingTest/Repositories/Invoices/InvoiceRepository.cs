using ProArch.CodingTest.Models.Invoices;
using System.Collections.Generic;
using System.Linq;

namespace ProArch.CodingTest.Repositories.Invoices
{
    public class InvoiceRepository : IInvoiceRepository
    {
        public IQueryable<Invoice> Get()
        {
            return new List<Invoice>().AsQueryable();
        }
    }
}
