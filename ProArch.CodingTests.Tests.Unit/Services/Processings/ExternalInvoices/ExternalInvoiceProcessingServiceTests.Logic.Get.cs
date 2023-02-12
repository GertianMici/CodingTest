using FluentAssertions;
using Moq;
using ProArch.CodingTest.External;
using ProArch.CodingTest.Models.FailoverInvoices;
using ProArch.CodingTest.Models.Summaries;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProArch.CodingTests.Tests.Unit.Services.Processings.ExternalInvoices
{
    public partial class ExternalInvoiceProcessingServiceTests
    {
        [Fact]
        public void ShouldReturnSpendDetailsByExternalService()
        {
            //given
            int inputSupplierId = GetRandomNumber();
            string inputSupplier = inputSupplierId.ToString();
            ExternalInvoice[] randomExternalInvoices = CreateRandomExternalInvoices();
            ExternalInvoice[] storageExternalInvoices = randomExternalInvoices;

            List<SpendDetail> expectedSpendDetails = storageExternalInvoices
                .GroupBy(x => x.Year, (year, amount) =>
                    new SpendDetail
                    {
                        Year = year,
                        TotalSpend = amount.Sum(x => x.TotalAmount)
                    })
                .ToList();

            this.externalInvoiceServicesMock.Setup(service =>
                service.GetInvoices(inputSupplier))
                    .Returns(storageExternalInvoices);

            //when
            List<SpendDetail> actualSpendDetails =
                this.externalInvoiceProcessingService.RetrieveYearlySpendDetails(inputSupplierId);

            //then
            actualSpendDetails.Should().BeEquivalentTo(expectedSpendDetails);

            this.externalInvoiceServicesMock.Verify(service =>
                service.GetInvoices(inputSupplier), Times.Once);

            this.externalInvoiceServicesMock.VerifyNoOtherCalls();
            this.failoverInvoiceServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldReturnSpendDetailsByFailoverService()
        {
            //given
            int inputSupplierId = GetRandomNumber();
            string inputSupplier = inputSupplierId.ToString();

            FailoverInvoiceCollection randomFailoverInvoiceCollection =
                CreateRandomFailoverCollection(DateTime.Now.AddMinutes(-1));

            FailoverInvoiceCollection storageFailoverInvoiceCollection = randomFailoverInvoiceCollection;
            var exception = new Exception();

            List<SpendDetail> expectedSpendDetails = storageFailoverInvoiceCollection.Invoices
                .GroupBy(x => x.Year, (year, amount) =>
                    new SpendDetail
                    {
                        Year = year,
                        TotalSpend = amount.Sum(x => x.TotalAmount)
                    })
                .ToList();

            this.externalInvoiceServicesMock.Setup(service =>
                service.GetInvoices(inputSupplier))
                    .Throws(exception);

            this.failoverInvoiceServiceMock.Setup(service =>
                service.GetInvoices(inputSupplierId))
                    .Returns(storageFailoverInvoiceCollection);

            //when
            List<SpendDetail> actualSpendDetails =
                this.externalInvoiceProcessingService.RetrieveYearlySpendDetails(inputSupplierId);

            //then
            actualSpendDetails.Should().BeEquivalentTo(expectedSpendDetails);

            this.externalInvoiceServicesMock.Verify(service =>
                service.GetInvoices(inputSupplier), Times.Exactly(3));

            this.failoverInvoiceServiceMock.Verify(service =>
                service.GetInvoices(inputSupplierId), Times.Once);

            this.externalInvoiceServicesMock.VerifyNoOtherCalls();
            this.failoverInvoiceServiceMock.VerifyNoOtherCalls();
        }
    }
}
