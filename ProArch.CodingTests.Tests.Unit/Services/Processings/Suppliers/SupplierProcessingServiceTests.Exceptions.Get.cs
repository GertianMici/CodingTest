using ProArch.CodingTest.Models.Suppliers.Exceptions;
using Xunit;

namespace ProArch.CodingTests.Tests.Unit.Services.Processings.Suppliers
{
    public partial class SupplierProcessingServiceTests
    {
        [Fact]
        public void ShouldThrowInvalidSupplierException()
        {
            //given
            int supplierId = GetRandomNegativeNumber();

            //when
            var getSupplierAction = () =>
                this.supplierProcessingService.RetrieveSupplierById(supplierId);

            //then
            Assert.Throws<InvalidSupplierException>(getSupplierAction);

            this.supplierServiceMock.VerifyNoOtherCalls();
        }
    }
}
