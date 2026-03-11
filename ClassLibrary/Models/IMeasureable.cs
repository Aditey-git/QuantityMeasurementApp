using System;

namespace QuantityAppModel
{
    public interface IMeasurable
    {
        double ConvertToBaseUnit(double value);

        double ConvertFromBaseUnit(double baseValue);

        // Default arithmetic validation
        void ValidateOperationSupport(string operation)
        {
            
        }

        // Functional interface support
        Func<bool> SupportsArithmetic { get; }
    }
}