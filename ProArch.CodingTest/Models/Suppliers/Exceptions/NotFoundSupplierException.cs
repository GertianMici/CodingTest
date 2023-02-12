using System;

namespace ProArch.CodingTest.Models.Suppliers.Exceptions
{
    [Serializable]
    public class NotFoundSupplierException : Exception
    {
        public NotFoundSupplierException(int supplierId)
            : base(message: $"Couldn't find supplier with id: {supplierId}")
        { }
    }
}
