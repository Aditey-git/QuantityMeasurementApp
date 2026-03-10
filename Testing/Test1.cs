﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityAppModel;

namespace QuantityAppTesting
{
    [TestClass]
    public class FeetTests
    {
        [TestMethod]
        public void GivenSameFeetValues_ShouldReturnTrue()
        {
            Feet first = new Feet(5.0);
            Feet second = new Feet(5.0);

            Assert.IsTrue(first.Equals(second));
        }

        [TestMethod]
        public void GivenDifferentFeetValues_ShouldReturnFalse()
        {
            Feet first = new Feet(5.0);
            Feet second = new Feet(6.0);

            Assert.IsFalse(first.Equals(second));
        }

        [TestMethod]
        public void GivenNullComparison_ShouldReturnFalse()
        {
            Feet first = new Feet(5.0);

            Assert.IsFalse(first.Equals(null));
        }

        [TestMethod]
        public void GivenSameReference_ShouldReturnTrue()
        {
            Feet first = new Feet(5.0);

            Assert.IsTrue(first.Equals(first));
        }

        [TestMethod]
        public void GivenFeetObjectsWithSameValue_ShouldHaveSameHashCode()
        {
            Feet first = new Feet(5.0);
            Feet second = new Feet(5.0);

            Assert.AreEqual(first.GetHashCode(), second.GetHashCode());
        }

        [TestMethod]
        public void GivenFeetObjectsWithDifferentValues_ShouldHaveDifferentHashCode()
        {
            Feet first = new Feet(5.0);
            Feet second = new Feet(6.0);

            Assert.AreNotEqual(first.GetHashCode(), second.GetHashCode());
        }
        
    }
}
