using ProArch.CodingTest.Models.Summaries;
using ProArch.CodingTest.Models.Suppliers.Exceptions;
using ProArch.CodingTest.Services.Foundations.Invoices;
using System.Collections.Generic;
using System.Linq;

namespace ProArch.CodingTest.Services.Processings.Invoices
{
    public class InvoiceProcessingService : IInvoiceProcessingService
    {
        private readonly IInvoiceService invoiceService;

        public InvoiceProcessingService(IInvoiceService invoiceService) =>
            this.invoiceService = invoiceService;

        public List<SpendDetail> RetrieveYearlySpendDetails(int supplierId)
        {
            if (supplierId < 1)
            {
                throw new InvalidSupplierException(nameof(supplierId), supplierId.ToString());
            }

            return this.invoiceService.Get()
                .Where(x => x.SupplierId == supplierId)
                .GroupBy(x => x.InvoiceDate.Year, (year, amount) =>
                    new SpendDetail
                    {
                        Year = year,
                        TotalSpend = amount.Sum(x => x.Amount)
                    })
                .ToList();
        }
    }
}
