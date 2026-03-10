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

        public bool CompareInch(Inch first, Inch second)
        {
            if (first == null)
                throw new ArgumentNullException(nameof(first));

            if (second == null)
                throw new ArgumentNullException(nameof(second));

            return first.Equals(second);
        }

        public bool CompareFeetAndInch(Feet feet, Inch inch)
        {
            if (feet == null)
                throw new ArgumentNullException(nameof(feet));

            if (inch == null)
                throw new ArgumentNullException(nameof(inch));

            double feetInInches = feet.Value * 12;

            return feetInInches.CompareTo(inch.Value) == 0;
        }
    }
}