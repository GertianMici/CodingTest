using Moq;
using ProArch.CodingTest.Models.Summaries;
using ProArch.CodingTest.Models.Suppliers;
using ProArch.CodingTest.Services.Orchestrations.Summaries;
using ProArch.CodingTest.Services.Processings.ExternalInvoices;
using ProArch.CodingTest.Services.Processings.Invoices;
using ProArch.CodingTest.Services.Processings.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using Tynamix.ObjectFiller;

namespace ProArch.CodingTests.Tests.Unit.Services.Orchestrations.Summaries
{
    public partial class SpendServiceTests
    {
        private readonly Mock<ISupplierProcessingService> supplierProcessingServiceMock;
        private readonly Mock<IInvoiceProcessingService> invoiceProcessingServiceMock;
        private readonly Mock<IExternalInvoiceProcessingService> externalInvoiceProcessingServiceMock;
        private readonly ISpendService spendService;

        public SpendServiceTests()
        {
            this.supplierProcessingServiceMock = new Mock<ISupplierProcessingService>();
            this.invoiceProcessingServiceMock = new Mock<IInvoiceProcessingService>();
            this.externalInvoiceProcessingServiceMock = new Mock<IExternalInvoiceProcessingService>();

            this.spendService = new SpendService(
                this.supplierProcessingServiceMock.Object,
                this.invoiceProcessingServiceMock.Object,
                this.externalInvoiceProcessingServiceMock.Object);
        }

        private static Supplier CreateRandomSupplier(bool isExternal) =>
            CreateSupplierFiller(isExternal).Create();

        private static Filler<Supplier> CreateSupplierFiller(bool isExternal)
        {
            var filler = new Filler<Supplier>();

            filler.Setup()
                .OnProperty(x => x.IsExternal).Use(isExternal);

            return filler;
        }

        private static List<SpendDetail> CreateRandomSpendDetails() => CreateSpendDetailFiller()
            .Create(GetRandomNumber())
                .ToList();

        private static Filler<SpendDetail> CreateSpendDetailFiller()
        {
            var filler = new Filler<SpendDetail>();

            filler.Setup()
                .OnProperty(x => x.Year).Use(() => GetRandomYear());

            return filler;
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 1, max: 99).GetValue();

        private static int GetRandomYear() =>
            new IntRange(min: 1980, max: DateTime.Now.Year).GetValue();
    }
}
