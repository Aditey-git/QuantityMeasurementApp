using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityAppModel;
using QuantityAppService;
using System;

namespace QuantityAppTesting
{
    [TestClass]
    public class QuantityTests
    {
        private const double TOLERANCE = 1e-6;

        // Converter instances shared across tests
        private static readonly LengthConverter _lengthConv = LengthConverter.Instance;
        private static readonly WeightConverter _weightConv = WeightConverter.Instance;
        private static readonly VolumeConverter _volumeConv = VolumeConverter.Instance;
        private static readonly TemperatureConverter _tempConv = TemperatureConverter.Instance;

        // LENGTH TESTS

        [TestMethod]
        public void Length_Equality_CrossUnit()
        {
            var a = new Quantity<LengthUnit>(1, LengthUnit.Feet, _lengthConv);
            var b = new Quantity<LengthUnit>(12, LengthUnit.Inch, _lengthConv);

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void Length_Conversion()
        {
            var length = new Quantity<LengthUnit>(3, LengthUnit.Feet, _lengthConv);
            var result = length.ConvertTo(LengthUnit.Yard);

            Assert.AreEqual(1, result, TOLERANCE);
        }

        [TestMethod]
        public void Length_Addition()
        {
            var a = new Quantity<LengthUnit>(1, LengthUnit.Feet, _lengthConv);
            var b = new Quantity<LengthUnit>(12, LengthUnit.Inch, _lengthConv);

            var result = a.Add(b, LengthUnit.Feet);

            Assert.AreEqual(2, result.ConvertTo(LengthUnit.Feet), TOLERANCE);
        }


        // WEIGHT TESTS

        [TestMethod]
        public void Weight_Equality_CrossUnit()
        {
            var a = new Quantity<WeightUnit>(1, WeightUnit.Kilogram, _weightConv);
            var b = new Quantity<WeightUnit>(1000, WeightUnit.Gram, _weightConv);

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void Weight_Conversion()
        {
            var weight = new Quantity<WeightUnit>(1, WeightUnit.Kilogram, _weightConv);
            var result = weight.ConvertTo(WeightUnit.Pound);

            Assert.AreEqual(2.20462, result, 1e-5);
        }

        [TestMethod]
        public void Weight_Addition()
        {
            var a = new Quantity<WeightUnit>(1, WeightUnit.Kilogram, _weightConv);
            var b = new Quantity<WeightUnit>(1000, WeightUnit.Gram, _weightConv);

            var result = a.Add(b, WeightUnit.Kilogram);

            Assert.AreEqual(2, result.ConvertTo(WeightUnit.Kilogram), TOLERANCE);
        }

        // VOLUME TESTS

        [TestMethod]
        public void Volume_Equality_Litre_Millilitre()
        {
            var a = new Quantity<VolumeUnit>(1, VolumeUnit.Litre, _volumeConv);
            var b = new Quantity<VolumeUnit>(1000, VolumeUnit.Millilitre, _volumeConv);

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void Volume_Equality_Litre_Gallon()
        {
            var a = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre, _volumeConv);
            var b = new Quantity<VolumeUnit>(1, VolumeUnit.Gallon, _volumeConv);

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void Volume_Conversion()
        {
            var gallon = new Quantity<VolumeUnit>(1, VolumeUnit.Gallon, _volumeConv);
            var result = gallon.ConvertTo(VolumeUnit.Litre);

            Assert.AreEqual(3.78541, result, TOLERANCE);
        }

        [TestMethod]
        public void Volume_Addition()
        {
            var litre = new Quantity<VolumeUnit>(1, VolumeUnit.Litre, _volumeConv);
            var ml = new Quantity<VolumeUnit>(1000, VolumeUnit.Millilitre, _volumeConv);

            var result = litre.Add(ml, VolumeUnit.Litre);

            Assert.AreEqual(2, result.ConvertTo(VolumeUnit.Litre), TOLERANCE);
        }

        // MATHEMATICAL PROPERTIES

        [TestMethod]
        public void Commutativity_Volume()
        {
            var a = new Quantity<VolumeUnit>(1, VolumeUnit.Litre, _volumeConv);
            var b = new Quantity<VolumeUnit>(1000, VolumeUnit.Millilitre, _volumeConv);

            var r1 = a.Add(b, VolumeUnit.Litre);
            var r2 = b.Add(a, VolumeUnit.Litre);

            Assert.IsTrue(r1.Equals(r2));
        }

        [TestMethod]
        public void Transitive_Property_Volume()
        {
            var a = new Quantity<VolumeUnit>(1, VolumeUnit.Litre, _volumeConv);
            var b = new Quantity<VolumeUnit>(1000, VolumeUnit.Millilitre, _volumeConv);
            var c = new Quantity<VolumeUnit>(1, VolumeUnit.Litre, _volumeConv);

            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(b.Equals(c));
            Assert.IsTrue(a.Equals(c));
        }

        [TestMethod]
        public void RoundTrip_Volume()
        {
            var original = new Quantity<VolumeUnit>(1.5, VolumeUnit.Litre, _volumeConv);
            var millis = original.ConvertTo(VolumeUnit.Millilitre);
            var reconstructed = new Quantity<VolumeUnit>(millis, VolumeUnit.Millilitre, _volumeConv);
            var backToLitre = reconstructed.ConvertTo(VolumeUnit.Litre);

            Assert.AreEqual(original.ConvertTo(VolumeUnit.Litre), backToLitre, TOLERANCE);
        }

        // EDGE CASES

        [TestMethod]
        public void Zero_Value()
        {
            var a = new Quantity<VolumeUnit>(0, VolumeUnit.Litre, _volumeConv);
            var b = new Quantity<VolumeUnit>(0, VolumeUnit.Millilitre, _volumeConv);

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void Negative_Value()
        {
            var a = new Quantity<VolumeUnit>(-1, VolumeUnit.Litre, _volumeConv);
            var b = new Quantity<VolumeUnit>(-1000, VolumeUnit.Millilitre, _volumeConv);

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void Large_Value_Addition()
        {
            var a = new Quantity<VolumeUnit>(1_000_000, VolumeUnit.Litre, _volumeConv);
            var b = new Quantity<VolumeUnit>(1_000_000, VolumeUnit.Litre, _volumeConv);

            var result = a.Add(b, VolumeUnit.Litre);

            Assert.AreEqual(2_000_000, result.ConvertTo(VolumeUnit.Litre), TOLERANCE);
        }
        
        // TEMPERATURE EQUALITY
        [TestMethod]
        public void Temp_Equality_Celsius_Fahrenheit()
        {
            var a = new Quantity<TemperatureUnit>(0, TemperatureUnit.Celsius, _tempConv);
            var b = new Quantity<TemperatureUnit>(32, TemperatureUnit.Fahrenheit, _tempConv);

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void Temp_Equality_Kelvin_Celsius()
        {
            var a = new Quantity<TemperatureUnit>(273.15, TemperatureUnit.Kelvin, _tempConv);
            var b = new Quantity<TemperatureUnit>(0, TemperatureUnit.Celsius, _tempConv);

            Assert.IsTrue(a.Equals(b));
        }

        // NULL HANDLING
        
        [TestMethod]
        public void Equals_Null_ReturnsFalse()
        {
            var a = new Quantity<VolumeUnit>(1, VolumeUnit.Litre, _volumeConv);

            Assert.IsFalse(a.Equals(null));
        }
    }
}