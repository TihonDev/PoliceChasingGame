namespace UnitTests
{
    using System;
    using NUnit.Framework;
    using PCGClassLibrary.Directions;
    using PCGClassLibrary.GameObjects;
    using PCGClassLibrary.InnerGameExceptions;
    using PCGClassLibrary.Managers;

    [TestFixture]
    public class CarTests
    {
        private const int X_MAX_VALUE = 130;
        private const int Y_MAX_VALUE = 30;
        private Car myCar;
        private Criminal criminalGuy;

        [OneTimeSetUp]
        public void FixtureInitialize()
        {
            this.criminalGuy = new Criminal(X_MAX_VALUE, Y_MAX_VALUE);
            this.myCar = new Car(this.criminalGuy, X_MAX_VALUE, Y_MAX_VALUE);
        }

        [OneTimeTearDown]
        public void FixtureCleanup()
        {
            this.criminalGuy = null;
            this.myCar = null;
        }

        [Test]
        public void CarConstructorShouldThrowArgumentNullExceptionIfTargetCriminalParameterIsNull()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new Car(null, X_MAX_VALUE, Y_MAX_VALUE));
        }

        [TestCase("")]
        [TestCase("i")]
        [TestCase(null)]
        [TestCase("x   ")]
        [TestCase("    ")]
        [TestCase(" p    ")]
        [TestCase("     s")]
        public void BrandPropertySetterShouldThrowInvalidCarBrandExceptionIfValueIsNullEmptyOrTooShort(string brand)
        {
            Assert.Throws(typeof(InvalidCarBrandException), () => { this.myCar.Brand = brand; }, "Brand property validator does not work correctly.");
        }

        [TestCase("     ")]
        [TestCase("")]
        [TestCase("   ")]
        [TestCase(null)]
        public void ModelPropertySetterShouldThrowInvalidCarModelExceptionIfValueIsNullEmptyOrTooShort(string model)
        {
            Assert.Throws(typeof(InvalidCarModelException), () => { this.myCar.Model = model; }, "Model property validator does not work correctly.");
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("     ")]
        [TestCase(" l    ")]
        [TestCase("      u ")]
        [TestCase("   f    ")]
        public void DriverNamelPropertySetterShouldThrowInvalidCarModelExceptionIfValueIsNullEmptyOrTooShort(string driver)
        {
            Assert.Throws(typeof(InvalidPlayerNameException), () => { this.myCar.DriverName = driver; }, "DriverName property validator does not work correctly.");
        }

        [TestCase(10, 20, 15, 21, "LeftArrow", true)]
        [TestCase(5, 15, 5, 15, "UpArrow", true)]
        [TestCase(30, 20, 36, 23, "DownArrow", true)]
        [TestCase(50, 18, 49, 20, "RightArrow", true)]
        [TestCase(10, 15, 12, 12, "UpArrow", false)]
        [TestCase(64, 20, 62, 21, "RightArrow", false)]
        [TestCase(100, 26, 40, 10, "DownArrow", false)]
        [TestCase(60, 17, 70, 22, "LeftArrow", false)]
        public void IsCriminalCaughtMethodShouldReturnCorrectResults(int carX, int carY, int criminalX, int criminalY, string direction, bool expectedValue)
        {
            this.myCar.XCoordinate = carX;
            this.myCar.YCoordinate = carY;
            var directionsManager = new DirectionsManager();
            myCar.Direction = directionsManager.GetDirection(direction);
            this.criminalGuy.XCoordinate = criminalX;
            this.criminalGuy.YCoordinate = criminalY;

            Assert.AreEqual(expectedValue, this.myCar.IsCriminalCaught(), "IsCriminalCaught method does not work correctly.");
        }

        [TestCase(int.MaxValue, int.MaxValue - 20)]
        [TestCase(100, 100 - 20)]
        [TestCase(80, 80 - 20)]
        [TestCase(74, 74 - 20)]
        [TestCase(21, 21 - 20)]
        [TestCase(20, 20 - 1)]
        [TestCase(19, 19 - 1)]
        [TestCase(10, 10 - 1)]
        [TestCase(1, 1 - 1)]
        public void SpeedUpMethodShouldUpdateThreadSleepTimeCorrectly(int sleepTime, int expectedValue)
        {
            this.myCar.SpeedUp(ref sleepTime);
            Assert.AreEqual(expectedValue, sleepTime, "SpeedUp method does not work correctly.");
        }
    }
}
