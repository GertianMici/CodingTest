using ProArch.CodingTest.Models.Suppliers;
using ProArch.CodingTest.Models.Suppliers.Exceptions;
using ProArch.CodingTest.Services.Foundations.Suppliers;

namespace ProArch.CodingTest.Services.Processings.Suppliers
{
    public class SupplierProcessingService : ISupplierProcessingService
    {
        private readonly ISupplierService supplierService;

        public SupplierProcessingService(ISupplierService supplierService) =>
            this.supplierService = supplierService;

        public Supplier RetrieveSupplierById(int id)
        {
            if (id < 1)
            {
                throw new InvalidSupplierException(nameof(id), id.ToString());
            }

            return this.supplierService.GetById(id);
        }
    }
}
