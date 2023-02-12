using Moq;
using ProArch.CodingTest.External;
using ProArch.CodingTest.Models.FailoverInvoices;
using ProArch.CodingTest.Services.Foundations.ExternalInvoices;
using ProArch.CodingTest.Services.Foundations.FailoverInvoices;
using ProArch.CodingTest.Services.Processings.ExternalInvoices;
using System;
using System.Linq;
using Tynamix.ObjectFiller;

namespace ProArch.CodingTests.Tests.Unit.Services.Processings.ExternalInvoices
{
    public partial class ExternalInvoiceProcessingServiceTests
    {
        private readonly Mock<IFailoverInvoiceService> failoverInvoiceServiceMock;
        private readonly Mock<IExternalInvoiceServices> externalInvoiceServicesMock;
        private readonly IExternalInvoiceProcessingService externalInvoiceProcessingService;

        public ExternalInvoiceProcessingServiceTests()
        {
            this.failoverInvoiceServiceMock = new Mock<IFailoverInvoiceService>();
            this.externalInvoiceServicesMock = new Mock<IExternalInvoiceServices>();

            this.externalInvoiceProcessingService = new ExternalInvoiceProcessingService(
                this.failoverInvoiceServiceMock.Object,
                this.externalInvoiceServicesMock.Object);
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 1, max: 99).GetValue();

        private static int GetRandomNegativeNumber() =>
            GetRandomNumber() * -1;

        private static ExternalInvoice[] CreateRandomExternalInvoices() =>
            CreateExternalInvoiceFiller()
            .Create(GetRandomNumber())
            .ToArray();

        private static Filler<ExternalInvoice> CreateExternalInvoiceFiller()
        {
            var filler = new Filler<ExternalInvoice>();

            filler.Setup()
                .OnProperty(x => x.Year).Use(() => GetRandomYear())
                .OnProperty(x => x.TotalAmount).Use(1);

            return filler;
        }

        private static FailoverInvoiceCollection CreateRandomFailoverCollection(
            DateTime failoverTimestamp) =>
            CreateFailoverCollectionFiller(failoverTimestamp).Create();

        private static Filler<FailoverInvoiceCollection> CreateFailoverCollectionFiller(
            DateTime failoverTimestamp)
        {
            var filler = new Filler<FailoverInvoiceCollection>();

            filler.Setup()
                .OnProperty(x => x.Timestamp).Use(failoverTimestamp)
                .OnProperty(x => x.Invoices).Use(() => CreateRandomExternalInvoices());

            return filler;
        }

        private static int GetRandomYear() =>
            new IntRange(min: 1980, max: DateTime.Now.Year).GetValue();
    }
}
