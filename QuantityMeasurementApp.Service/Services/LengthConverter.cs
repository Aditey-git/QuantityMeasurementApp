using System.Collections.Generic;
using QuantityAppModel;

namespace QuantityAppService
{
    public class LengthConverter : IMeasurable<LengthUnit>
    {
        public static readonly LengthConverter Instance = new();

        private readonly Dictionary<LengthUnit, double> _toFeet = new()
        {
            { LengthUnit.Feet, 1.0 },
            { LengthUnit.Inch, 1.0 / 12.0 },
            { LengthUnit.Yard, 3.0 },
            { LengthUnit.Centimeters, 1.0 / 30.48 } // since 1 foot = 30.48 cm
        };

        public double ToBaseUnit(LengthUnit unit, double value) => value * _toFeet[unit];
        public double FromBaseUnit(LengthUnit unit, double baseValue) => baseValue / _toFeet[unit];
    }
}
