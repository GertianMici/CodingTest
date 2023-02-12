using ProArch.CodingTest.Models.Suppliers;

namespace ProArch.CodingTest.Services.Processings.Suppliers
{
    public interface ISupplierProcessingService
    {
        Supplier RetrieveSupplierById(int id);
    }
}
