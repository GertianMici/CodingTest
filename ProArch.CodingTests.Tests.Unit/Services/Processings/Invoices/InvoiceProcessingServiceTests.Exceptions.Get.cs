using ProArch.CodingTest.Models.Suppliers.Exceptions;
using Xunit;

namespace ProArch.CodingTests.Tests.Unit.Services.Processings.Invoices
{
    public partial class InvoiceProcessingServiceTests
    {
        [Fact]
        public void ShouldThrowInvalidSupplierException()
        {
            //given
            int supplierId = GetRandomNegativeNumber();

            //when
            var getSpendDetailsAction = () =>
                this.invoiceProcessingService.RetrieveYearlySpendDetails(supplierId);

            //then
            Assert.Throws<InvalidSupplierException>(getSpendDetailsAction);

            this.invoiceServiceMock.VerifyNoOtherCalls();
        }
    }
}
