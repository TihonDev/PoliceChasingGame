namespace UnitTests
{
    using NUnit.Framework;
    using PCGClassLibrary.GameObjects;
    using PCGClassLibrary.InnerGameExceptions;

    [TestFixture]
    public class PositionTests
    {
        private Position testPosition;

        [TearDown]
        public void Cleanup()
        {
            this.testPosition = null;
        }

        // minimum allowable value of coordinates is always 1
        [TestCase(1, 1)]
        [TestCase(199, 199)]
        [TestCase(1, 199)]
        [TestCase(199, 10)]
        [TestCase(8, 98)]
        [TestCase(163, 2)]
        public void SetNewPositionMethodShouldSetNewCoordinatesCorrectly(int x, int y)
        {
            this.testPosition = new Position();
            this.testPosition.SetNewPosition(x, y);

            Assert.AreEqual(x, this.testPosition.XCoordinate, "X coordinate was not set correctly.");
            Assert.AreEqual(y, this.testPosition.YCoordinate, "Y coordinate was not set correctly.");
        }

        [TestCase(33, 150)]
        [TestCase(150, 151)]
        [TestCase(10, 0)]
        [TestCase(100, -1)]
        public void XCoordinatePropertySetterShouldThrowCoordinateOutOfRangeExceptionIfValueIsNotInAllowableRange(int xMaxValue, int x)
        {
            this.testPosition = new Position(xMaxValue, 10);

            Assert.Throws(typeof(CoordinateOutOfRangeException), () => { testPosition.XCoordinate = x; }, "XCoordinate setter does not work correctly.");
        }

        [TestCase(2, 2)]
        [TestCase(10, 0)]
        [TestCase(150, -1)]
        [TestCase(100, 101)]
        public void YCoordinatePropertySetterShouldThrowCoordinateOutOfRangeExceeptionIfValueIsNotInAllowableRange(int yMaxValue, int y)
        {
            this.testPosition = new Position(10, yMaxValue);

            Assert.Throws(typeof(CoordinateOutOfRangeException), () => { testPosition.YCoordinate = y; }, "YCoordinate setter does not work correctly.");
        }

        [TestCase(1, 12)]
        [TestCase(0, 1)]
        [TestCase(-1, -256)]
        [TestCase(int.MinValue, 10)]
        [TestCase(int.MaxValue, int.MinValue)]
        [TestCase(64, -16)]
        public void PositionConstructorShouldThrowCoordinateOutOfRangeExceptionIfOneOfParametersIsEqualOrLessThanMinimumAllowableValue(int maxValueOfX, int maxValueOfY)
        {
            Assert.Throws(typeof(CoordinateOutOfRangeException), () => new Position(maxValueOfX, maxValueOfY));
        }
    }
}
