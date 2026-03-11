using System;
using System.Collections.Generic;

namespace QuantityAppModel
{
    public sealed class Quantity<U> : IEquatable<Quantity<U>>
        where U : struct, Enum
    {
        private const double TOLERANCE = 1e-6;

        public double Value { get; }
        public U Unit { get; }

        public Quantity(double value, U unit)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be finite.");

            Value = value;
            Unit = unit;
        }

        // Convert current value to base unit
        private double ConvertToBase()
        {
            return ((object)Unit) switch
            {
                LengthUnit l => l.ToBaseUnit(Value),
                WeightUnit w => w.ToBaseUnit(Value),
                _ => throw new InvalidOperationException("Unsupported category")
            };
        }

        // Convert to target unit (instance method)
        public Quantity<U> ConvertTo(U targetUnit)
        {
            double baseValue = ConvertToBase();

            double converted = ((object)targetUnit) switch
            {
                LengthUnit l => l.FromBaseUnit(baseValue),
                WeightUnit w => w.FromBaseUnit(baseValue),
                _ => throw new InvalidOperationException("Unsupported category")
            };

            return new Quantity<U>(converted, targetUnit);
        }

        // Adds another length and returns result in current object's unit
        public Quantity<U> Add(Quantity<U> other)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other));

            double sumBase =
                ConvertToBase() + other.ConvertToBase();

            double result = ((object)Unit) switch
            {
                LengthUnit l => l.FromBaseUnit(sumBase),
                WeightUnit w => w.FromBaseUnit(sumBase),
                _ => throw new InvalidOperationException("Unsupported category")
            };

            return new Quantity<U>(result, Unit);
        }

        // Equality comparison (cross-unit safe)
        public bool Equals(Quantity<U>? other)
        {
            if (other is null)
                return false;

            return Math.Abs(
                ConvertToBase() - other.ConvertToBase()
            ) < TOLERANCE;
        }

        public override bool Equals(object? obj)
        {
            return obj is Quantity<U> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return ConvertToBase().GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value:F4} {Unit}";
        }
    }
}