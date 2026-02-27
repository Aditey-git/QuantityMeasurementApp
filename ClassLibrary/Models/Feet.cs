using System;

namespace QuantityAppModel{
    public class Feet
    {
        private readonly double val;
        public Feet(double val)
        {
            this.val = val;
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

            if(this.val.CompareTo(other.val) != 0)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return val.GetHashCode();
        }
    }
}