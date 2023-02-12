using Moq;
using ProArch.CodingTest.Models.Suppliers;
using ProArch.CodingTest.Services.Foundations.Suppliers;
using ProArch.CodingTest.Services.Processings.Suppliers;
using Tynamix.ObjectFiller;

namespace ProArch.CodingTests.Tests.Unit.Services.Processings.Suppliers
{
    public partial class SupplierProcessingServiceTests
    {
        private readonly Mock<ISupplierService> supplierServiceMock;
        private readonly ISupplierProcessingService supplierProcessingService;

        public SupplierProcessingServiceTests()
        {
            this.supplierServiceMock = new Mock<ISupplierService>();

            this.supplierProcessingService = new SupplierProcessingService(
                this.supplierServiceMock.Object);
        }

        private static Supplier CreateRandomSupplier(int supplierId) =>
            CreateSupplierFiller(supplierId).Create();

        private static Filler<Supplier> CreateSupplierFiller(int supplierId)
        {
            var filler = new Filler<Supplier>();

            filler.Setup()
                .OnProperty(x => x.Id).Use(supplierId);

            return filler;
        }

        private static int GetRandomNegativeNumber() =>
            GetRandomNumber() * -1;

        private static int GetRandomNumber() =>
            new IntRange(min: 1, max: 99).GetValue();
    }
}
