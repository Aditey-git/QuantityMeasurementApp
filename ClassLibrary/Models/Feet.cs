using System;

namespace QuantityAppModel{
    public class Feet
    {
        public double Value{get; set; }
        public Feet(double val)
        {
            this.Value = val;
        }

        public override bool Equals(object? obj)
        {
            if(ReferenceEquals(this,obj))
            {
                return true;
            }

            if(obj == null)
            {
                return false;
            }

            var typeOfObj = obj.GetType();

            if(this.GetType() != typeOfObj)
            {
                return false;
            }

            Feet other = (Feet) obj;

            if(this.Value.CompareTo(other.Value) != 0)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}