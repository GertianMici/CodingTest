using System;

namespace ProArch.CodingTest.Models.Suppliers.Exceptions
{
    [Serializable]
    public class InvalidSupplierException : Exception
    {
        public InvalidSupplierException(string parameterName, string parameterValue)
            : base(message: $"Invalid supplier " +
                  $"parameter name: {parameterName}, " +
                  $"parameter value: {parameterValue}")
        { }
    }
}
