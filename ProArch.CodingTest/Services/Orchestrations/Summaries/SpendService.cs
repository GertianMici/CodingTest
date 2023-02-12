using ProArch.CodingTest.Models.Summaries;
using ProArch.CodingTest.Models.Suppliers;
using ProArch.CodingTest.Models.Suppliers.Exceptions;
using ProArch.CodingTest.Services.Processings.ExternalInvoices;
using ProArch.CodingTest.Services.Processings.Invoices;
using ProArch.CodingTest.Services.Processings.Suppliers;

namespace ProArch.CodingTest.Services.Orchestrations.Summaries
{
    public class SpendService : ISpendService
    {
        private readonly ISupplierProcessingService supplierProcessingService;
        private readonly IInvoiceProcessingService invoiceProcessingService;
        private readonly IExternalInvoiceProcessingService externalInvoiceProcessingService;

        public SpendService(
            ISupplierProcessingService supplierProcessingService,
            IInvoiceProcessingService invoiceProcessingService,
            IExternalInvoiceProcessingService externalInvoiceProcessingService)
        {
            this.supplierProcessingService = supplierProcessingService;
            this.invoiceProcessingService = invoiceProcessingService;
            this.externalInvoiceProcessingService = externalInvoiceProcessingService;
        }

        public SpendSummary GetTotalSpend(int supplierId)
        {
            Supplier supplier = this.supplierProcessingService.RetrieveSupplierById(supplierId);

            if (supplier is null)
            {
                throw new NotFoundSupplierException(supplierId);
            }

            var spendSummary = new SpendSummary
            {
                Name = supplier.Name,
            };

            if (supplier.IsExternal)
            {
                spendSummary.Years = this.externalInvoiceProcessingService
                    .RetrieveYearlySpendDetails(supplier.Id);
            }
            else
            {
                spendSummary.Years = this.invoiceProcessingService
                    .RetrieveYearlySpendDetails(supplier.Id);
            }

            return spendSummary;
        }
    }
}
