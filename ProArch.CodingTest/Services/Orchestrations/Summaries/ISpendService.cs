using ProArch.CodingTest.Models.Summaries;

namespace ProArch.CodingTest.Services.Orchestrations.Summaries
{
    public interface ISpendService
    {
        SpendSummary GetTotalSpend(int supplierId);
    }
}
