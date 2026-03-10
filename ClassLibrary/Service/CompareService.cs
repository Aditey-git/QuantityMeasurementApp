using System;
using QuantityAppModel;

namespace QuantityAppService{
    public class QuantityMeasurementService
    {
        public bool CompareLength(QuantityLength first, QuantityLength second)
        {
            if (first == null || second == null)
                throw new ArgumentNullException("Length cannot be null");

            return first.Equals(second);
        }

    }
}