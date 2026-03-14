using System;
using QuantityAppModel;

namespace QuantityAppService
{
    // A custom exception type used when a measurement value is not valid.
    public class InvalidMeasurementException : Exception
    {
        public InvalidMeasurementException(string message) : base(message)
        {
        }
    }
}
