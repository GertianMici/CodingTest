using Moq;
using ProArch.CodingTest.Models.Invoices;
using ProArch.CodingTest.Services.Foundations.Invoices;
using ProArch.CodingTest.Services.Processings.Invoices;
using System.Linq;
using Tynamix.ObjectFiller;

namespace ProArch.CodingTests.Tests.Unit.Services.Processings.Invoices
{
    public partial class InvoiceProcessingServiceTests
    {
        private readonly Mock<IInvoiceService> invoiceServiceMock;
        private readonly IInvoiceProcessingService invoiceProcessingService;

        public InvoiceProcessingServiceTests()
        {
            this.invoiceServiceMock = new Mock<IInvoiceService>();

            this.invoiceProcessingService = new InvoiceProcessingService(
                this.invoiceServiceMock.Object);
        }

        private static IQueryable<Invoice> CreateRandomInvoices(int supplierId) =>
            CreateInvoiceFiller(supplierId)
            .Create(GetRandomNumber())
            .AsQueryable();

        private static Filler<Invoice> CreateInvoiceFiller(int supplierId)
        {
            var filler = new Filler<Invoice>();

            filler.Setup()
                .OnProperty(x => x.SupplierId).Use(supplierId)
                .OnProperty(x => x.Amount).Use(1);

            return filler;
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 1, max: 99).GetValue();

        private static int GetRandomNegativeNumber() =>
            GetRandomNumber() * -1;
    }
}
