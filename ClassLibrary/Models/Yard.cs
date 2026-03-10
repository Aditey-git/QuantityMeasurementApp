using System;

namespace QuantityAppModel
{
    public sealed class Yard : IEquatable<Yard>
    {
        public double Value { get; }

        public Yard(double value)
        {
            Value = value;
        }

        public bool Equals(Yard? other)
        {
            if (other is null)
                return false;

            return Value.CompareTo(other.Value) == 0;
        }

        public override bool Equals(object? obj)
        {
            return obj is Yard other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}