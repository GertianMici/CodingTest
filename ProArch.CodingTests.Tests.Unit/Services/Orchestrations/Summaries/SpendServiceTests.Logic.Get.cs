using FluentAssertions;
using Moq;
using ProArch.CodingTest.Models.Summaries;
using ProArch.CodingTest.Models.Suppliers;
using System.Collections.Generic;
using Xunit;

namespace ProArch.CodingTests.Tests.Unit.Services.Orchestrations.Summaries
{
    public partial class SpendServiceTests
    {
        [Fact]
        public void ShouldReturnTotalSpendForYearByExternalInvoice()
        {
            //given
            Supplier randomSupplier = CreateRandomSupplier(isExternal: true);
            int inputSupplierId = randomSupplier.Id;
            Supplier storageSupplier = randomSupplier;
            List<SpendDetail> randomSpendDetails = CreateRandomSpendDetails();
            List<SpendDetail> storageSpendDetails = randomSpendDetails;

            var expectedSpendSummary = new SpendSummary
            {
                Name = storageSupplier.Name,
                Years = storageSpendDetails
            };

            this.supplierProcessingServiceMock.Setup(service =>
                service.RetrieveSupplierById(inputSupplierId))
                    .Returns(storageSupplier);

            this.externalInvoiceProcessingServiceMock.Setup(service =>
                service.RetrieveYearlySpendDetails(inputSupplierId))
                    .Returns(storageSpendDetails);

            //when
            SpendSummary getSpentAction = this.spendService.GetTotalSpend(inputSupplierId);

            //then
            getSpentAction.Should().BeEquivalentTo(expectedSpendSummary);

            this.supplierProcessingServiceMock.Verify(service =>
                service.RetrieveSupplierById(inputSupplierId), Times.Once);

            this.externalInvoiceProcessingServiceMock.Verify(service =>
                service.RetrieveYearlySpendDetails(inputSupplierId), Times.Once);

            this.invoiceProcessingServiceMock.Verify(service =>
                service.RetrieveYearlySpendDetails(inputSupplierId), Times.Never);

            this.supplierProcessingServiceMock.VerifyNoOtherCalls();
            this.externalInvoiceProcessingServiceMock.VerifyNoOtherCalls();
            this.invoiceProcessingServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldReturnTotalSpendForYearByInvoiceService()
        {
            //given
            Supplier randomSupplier = CreateRandomSupplier(isExternal: false);
            int inputSupplierId = randomSupplier.Id;
            Supplier storageSupplier = randomSupplier;
            List<SpendDetail> randomSpendDetails = CreateRandomSpendDetails();
            List<SpendDetail> storageSpendDetails = randomSpendDetails;

            var expectedSpendSummary = new SpendSummary
            {
                Name = storageSupplier.Name,
                Years = storageSpendDetails
            };

            this.supplierProcessingServiceMock.Setup(service =>
                service.RetrieveSupplierById(inputSupplierId))
                    .Returns(storageSupplier);

            this.invoiceProcessingServiceMock.Setup(service =>
                service.RetrieveYearlySpendDetails(inputSupplierId))
                    .Returns(storageSpendDetails);

            //when
            SpendSummary getSpentAction = this.spendService.GetTotalSpend(inputSupplierId);

            //then
            getSpentAction.Should().BeEquivalentTo(expectedSpendSummary);

            this.supplierProcessingServiceMock.Verify(service =>
                service.RetrieveSupplierById(inputSupplierId), Times.Once);

            this.externalInvoiceProcessingServiceMock.Verify(service =>
                service.RetrieveYearlySpendDetails(inputSupplierId), Times.Never);

            this.invoiceProcessingServiceMock.Verify(service =>
                service.RetrieveYearlySpendDetails(inputSupplierId), Times.Once);

            this.supplierProcessingServiceMock.VerifyNoOtherCalls();
            this.externalInvoiceProcessingServiceMock.VerifyNoOtherCalls();
            this.invoiceProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
