using FluentAssertions;
using Moq;
using ProArch.CodingTest.Models.Suppliers;
using Xunit;

namespace ProArch.CodingTests.Tests.Unit.Services.Processings.Suppliers
{
    public partial class SupplierProcessingServiceTests
    {
        [Fact]
        public void ShouldReturnSupplierByid()
        {
            //given
            int inputSupplierId = GetRandomNumber();
            Supplier randomSupplier = CreateRandomSupplier(inputSupplierId);
            Supplier storageSuplier = randomSupplier;
            Supplier expectedSupplier = storageSuplier;

            this.supplierServiceMock.Setup(service =>
                service.GetById(inputSupplierId))
                    .Returns(storageSuplier);

            //when
            Supplier actualSupplier =
                this.supplierProcessingService.RetrieveSupplierById(inputSupplierId);

            //then
            actualSupplier.Should().BeEquivalentTo(expectedSupplier);

            this.supplierServiceMock.Verify(service =>
                service.GetById(inputSupplierId), Times.Once);

            this.supplierServiceMock.VerifyNoOtherCalls();
        }
    }
}
