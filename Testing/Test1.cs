﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityAppModel;
using QuantityAppService;
using System;

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

        [TestMethod]
        public void GivenOneFeetAndTwelveInch_ShouldReturnTrue()
        {
            Feet feet = new Feet(1);
            Inch inch = new Inch(12);

            QuantityMeasurementService service = new QuantityMeasurementService();

            bool result = service.CompareFeetAndInch(feet, inch);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenTwoFeetAndTwentyFourInch_ShouldReturnTrue()
        {
            Feet feet = new Feet(2);
            Inch inch = new Inch(24);

            QuantityMeasurementService service = new QuantityMeasurementService();

            bool result = service.CompareFeetAndInch(feet, inch);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenOneFeetAndTenInch_ShouldReturnFalse()
        {
            Feet feet = new Feet(1);
            Inch inch = new Inch(10);

            QuantityMeasurementService service = new QuantityMeasurementService();

            bool result = service.CompareFeetAndInch(feet, inch);

            Assert.IsFalse(result);
        }

        
                
    }
}
