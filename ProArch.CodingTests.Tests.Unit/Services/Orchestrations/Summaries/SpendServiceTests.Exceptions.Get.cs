using Moq;
using ProArch.CodingTest.Models.Suppliers;
using ProArch.CodingTest.Models.Suppliers.Exceptions;
using Xunit;

namespace ProArch.CodingTests.Tests.Unit.Services.Orchestrations.Summaries
{
    public partial class SpendServiceTests
    {
        [Fact]
        public void ShouldThrowNotFoundSupplierException()
        {
            //given
            int supplierId = GetRandomNumber();

            this.supplierProcessingServiceMock.Setup(service =>
                service.RetrieveSupplierById(supplierId))
                    .Returns<Supplier>(null);

            //when
            var getSpentAction = () => this.spendService.GetTotalSpend(supplierId);

            //then
            Assert.Throws<NotFoundSupplierException>(getSpentAction);

            this.supplierProcessingServiceMock.Verify(service =>
                service.RetrieveSupplierById(supplierId), Times.Once);

            this.supplierProcessingServiceMock.VerifyNoOtherCalls();
            this.externalInvoiceProcessingServiceMock.VerifyNoOtherCalls();
            this.invoiceProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
