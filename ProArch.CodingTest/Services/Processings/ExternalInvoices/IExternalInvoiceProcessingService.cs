using ProArch.CodingTest.Models.Summaries;
using System.Collections.Generic;

namespace ProArch.CodingTest.Services.Processings.ExternalInvoices
{
    public interface IExternalInvoiceProcessingService
    {
        List<SpendDetail> RetrieveYearlySpendDetails(int supplierId);
    }
}
