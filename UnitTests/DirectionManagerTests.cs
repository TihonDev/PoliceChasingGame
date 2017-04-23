namespace UnitTests
{
    using System.Collections.Generic;
    using NUnit.Framework;
    using PCGClassLibrary.Directions;
    using PCGClassLibrary.Interfaces;
    using PCGClassLibrary.Managers;

    [TestFixture]
    public class DirectionManagerTests
    {
        private DirectionsManager directionsManager;

        [OneTimeSetUp]
        public void FixtureInitialize()
        {
            this.directionsManager = new DirectionsManager();
        }

        [OneTimeTearDown]
        public void FixtureCleanup()
        {
            this.directionsManager = null;
        }

        [Test]
        public void GetDirectionMethodShouldReturnCorrectDirectionAccordingToPassedParameter()
        {
            var testCases = new List<KeyValuePair<string, IDirection>>()
            {
                new KeyValuePair<string, IDirection>("  ", new Right()),
                new KeyValuePair<string, IDirection>("RightArrow", new Right()),
                new KeyValuePair<string, IDirection>("LeftArrow", new Left()),
                new KeyValuePair<string, IDirection>(string.Empty, new Left()),
                new KeyValuePair<string, IDirection>("UpArrow", new Up()),
                new KeyValuePair<string, IDirection>("DownArrow", new Down()),
                new KeyValuePair<string, IDirection>("x", new Down()),
                new KeyValuePair<string, IDirection>(null, new Down()),
            };

            for (int i = 0; i < testCases.Count; i++)
            {
                var result = this.directionsManager.GetDirection(testCases[i].Key);
                Assert.IsTrue(result.GetType() == testCases[i].Value.GetType(), string.Format("Expected type: {0}, but was: {1}. Case index: {2}", testCases[i].Value.GetType().Name, result.GetType().Name, i));
            }
        }
    }
}
