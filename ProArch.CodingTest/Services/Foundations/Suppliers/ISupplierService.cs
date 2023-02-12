using ProArch.CodingTest.Models.Suppliers;

namespace ProArch.CodingTest.Services.Foundations.Suppliers
{
    public interface ISupplierService
    {
        Supplier GetById(int id);
    }
}
