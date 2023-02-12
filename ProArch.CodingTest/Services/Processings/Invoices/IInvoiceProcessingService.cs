using ProArch.CodingTest.Models.Summaries;
using System.Collections.Generic;

namespace ProArch.CodingTest.Services.Processings.Invoices
{
    public interface IInvoiceProcessingService
    {
        List<SpendDetail> RetrieveYearlySpendDetails(int supplierId);
    }
}
