using System.Collections.Generic;
using QuantityAppModel;

namespace QuantityAppService
{
    public class VolumeConverter : IMeasurable<VolumeUnit>
    {
        public static readonly VolumeConverter Instance = new();

        private readonly Dictionary<VolumeUnit, double> _toLitres = new()
        {
            { VolumeUnit.Litre, 1.0 },
            { VolumeUnit.Millilitre, 0.001 },
            { VolumeUnit.Gallon, 3.78541 }
        };

        public double ToBaseUnit(VolumeUnit unit, double value) => value * _toLitres[unit];
        public double FromBaseUnit(VolumeUnit unit, double baseValue) => baseValue / _toLitres[unit];
    }
}
