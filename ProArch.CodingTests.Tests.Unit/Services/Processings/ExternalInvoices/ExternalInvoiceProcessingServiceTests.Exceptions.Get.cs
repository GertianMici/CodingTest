using Moq;
using ProArch.CodingTest.Models.FailoverInvoices;
using ProArch.CodingTest.Models.FailoverInvoices.Exceptions;
using ProArch.CodingTest.Models.Suppliers.Exceptions;
using System;
using Xunit;

namespace ProArch.CodingTests.Tests.Unit.Services.Processings.ExternalInvoices
{
    public partial class ExternalInvoiceProcessingServiceTests
    {
        [Fact]
        public void ShouldThrowInvalidSupplierException()
        {
            //given
            int supplierId = GetRandomNegativeNumber();

            //when
            var getSpendDetailsAction = () =>
                this.externalInvoiceProcessingService.RetrieveYearlySpendDetails(supplierId);

            //then
            Assert.Throws<InvalidSupplierException>(getSpendDetailsAction);

            this.failoverInvoiceServiceMock.VerifyNoOtherCalls();
            this.externalInvoiceServicesMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowOutdatedFailoverInvoiceCollectionWhenTimestamptIsNotUpToDate()
        {
            //given
            int inputSupplierId = GetRandomNumber();
            string inputSupplier = inputSupplierId.ToString();

            FailoverInvoiceCollection randomFailoverInvoiceCollection =
                CreateRandomFailoverCollection(DateTime.Now.AddMonths(GetRandomNegativeNumber()));

            FailoverInvoiceCollection storageFailoverInvoiceCollection = randomFailoverInvoiceCollection;
            var exception = new Exception();

            this.externalInvoiceServicesMock.Setup(service =>
                service.GetInvoices(inputSupplier))
                    .Throws(exception);

            this.failoverInvoiceServiceMock.Setup(service =>
                service.GetInvoices(inputSupplierId))
                    .Returns(storageFailoverInvoiceCollection);

            //when
            var actualSpendDetailsAction = () =>
                this.externalInvoiceProcessingService.RetrieveYearlySpendDetails(inputSupplierId);

            //then
            Assert.Throws<OutdatedFailoverInvoiceException>(actualSpendDetailsAction);

            this.externalInvoiceServicesMock.Verify(service =>
                service.GetInvoices(inputSupplier), Times.Exactly(3));

            this.failoverInvoiceServiceMock.Verify(service =>
                service.GetInvoices(inputSupplierId), Times.Once);

            this.externalInvoiceServicesMock.VerifyNoOtherCalls();
            this.failoverInvoiceServiceMock.VerifyNoOtherCalls();
        }
    }
}
