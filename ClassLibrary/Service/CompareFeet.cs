using System;
using QuantityAppModel;

namespace QuantityAppService{
    public class QuantityMeasurementService
    {
        public bool CompareFeet(Feet first, Feet second)
        {
            if (first == null || second == null)
                throw new ArgumentNullException("Feet values cannot be null");

            return first.Equals(second);
        }
    }
}