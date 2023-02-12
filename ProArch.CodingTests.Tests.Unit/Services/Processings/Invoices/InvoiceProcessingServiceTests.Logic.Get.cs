using FluentAssertions;
using Moq;
using ProArch.CodingTest.Models.Invoices;
using ProArch.CodingTest.Models.Summaries;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProArch.CodingTests.Tests.Unit.Services.Processings.Invoices
{
    public partial class InvoiceProcessingServiceTests
    {
        [Fact]
        public void ShouldReturnSpendDetailsBySupplierId()
        {
            //given
            int inputSupplier1Id = GetRandomNumber();
            int inputSupplier2Id = GetRandomNumber();

            IQueryable<Invoice> supplied1Invoices = CreateRandomInvoices(inputSupplier1Id);
            IQueryable<Invoice> supplied2Invoices = CreateRandomInvoices(inputSupplier2Id);

            IQueryable<Invoice> storageInvoices = supplied1Invoices.Concat(supplied2Invoices);

            List<SpendDetail> expectedSpendDetails = supplied1Invoices
                .GroupBy(x => x.InvoiceDate.Year, (year, amount) =>
                    new SpendDetail
                    {
                        Year = year,
                        TotalSpend = amount.Sum(x => x.Amount)
                    })
                .ToList();

            this.invoiceServiceMock.Setup(service =>
                service.Get())
                    .Returns(storageInvoices);

            //when
            List<SpendDetail> actualSpendDetails =
                this.invoiceProcessingService.RetrieveYearlySpendDetails(inputSupplier1Id);

            //then
            actualSpendDetails.Should().BeEquivalentTo(expectedSpendDetails);

            this.invoiceServiceMock.Verify(service =>
                service.Get(), Times.Once);

            this.invoiceServiceMock.VerifyNoOtherCalls();
        }
    }
}
