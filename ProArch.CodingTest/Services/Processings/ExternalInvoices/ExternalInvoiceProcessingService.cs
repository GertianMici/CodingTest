using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using ProArch.CodingTest.External;
using ProArch.CodingTest.Models.FailoverInvoices;
using ProArch.CodingTest.Models.FailoverInvoices.Exceptions;
using ProArch.CodingTest.Models.Summaries;
using ProArch.CodingTest.Models.Suppliers.Exceptions;
using ProArch.CodingTest.Services.Foundations.ExternalInvoices;
using ProArch.CodingTest.Services.Foundations.FailoverInvoices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProArch.CodingTest.Services.Processings.ExternalInvoices
{
    public class ExternalInvoiceProcessingService : IExternalInvoiceProcessingService
    {
        private readonly CircuitBreakerPolicy circuitBreakerPolicy = Policy
                .Handle<Exception>()
                .CircuitBreaker(1, TimeSpan.FromSeconds(60));

        private readonly RetryPolicy retryPolicy = Policy
            .Handle<Exception>()
            .Retry(2);

        private readonly IFailoverInvoiceService failoverInvoiceService;
        private readonly IExternalInvoiceServices externalInvoiceServices;

        public ExternalInvoiceProcessingService(
            IFailoverInvoiceService failoverInvoiceService,
            IExternalInvoiceServices externalInvoiceServices)
        {
            this.failoverInvoiceService = failoverInvoiceService;
            this.externalInvoiceServices = externalInvoiceServices;
        }

        public List<SpendDetail> RetrieveYearlySpendDetails(int supplierId)
        {
            string supplier = supplierId.ToString();

            if (supplierId < 1)
            {
                throw new InvalidSupplierException(nameof(supplierId), supplier);
            }

            PolicyResult<ExternalInvoice[]> invoicesPolicyResult = this.circuitBreakerPolicy.ExecuteAndCapture(
                     () => this.retryPolicy.Execute(
                         () => this.externalInvoiceServices.GetInvoices(supplier)));

            ExternalInvoice[] externalInvoices;

            if (invoicesPolicyResult.Outcome == OutcomeType.Failure)
            {
                FailoverInvoiceCollection invoiceCollection =
                    this.failoverInvoiceService.GetInvoices(supplierId);

                if (invoiceCollection.Timestamp <= DateTime.Today.AddMonths(-1))
                {
                    throw new OutdatedFailoverInvoiceException(
                        timestamp: invoiceCollection.Timestamp,
                        innerException: invoicesPolicyResult.FinalException);
                }

                externalInvoices = invoiceCollection.Invoices;
            }
            else
            {
                externalInvoices = invoicesPolicyResult.Result;
            }

            return externalInvoices
                .GroupBy(x => x.Year, (year, amount) =>
                    new SpendDetail
                    {
                        Year = year,
                        TotalSpend = amount.Sum(x => x.TotalAmount)
                    })
                .ToList();
        }
    }
}
