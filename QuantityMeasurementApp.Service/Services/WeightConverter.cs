using System.Collections.Generic;
using QuantityAppModel;

namespace QuantityAppService
{
    public class WeightConverter : IMeasurable<WeightUnit>
    {
        public static readonly WeightConverter Instance = new();

        private readonly Dictionary<WeightUnit, double> _toKiloGrams = new()
        {
            { WeightUnit.Kilogram, 1.0 },
            { WeightUnit.Gram, 0.001 },
            { WeightUnit.Pound, 0.453592 }
        };

        public double ToBaseUnit(WeightUnit unit, double value) => value * _toKiloGrams[unit];
        public double FromBaseUnit(WeightUnit unit, double baseValue) => baseValue / _toKiloGrams[unit];
    }
}
