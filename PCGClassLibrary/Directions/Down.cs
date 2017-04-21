namespace PCGClassLibrary.Directions
{
    using System.Collections.Generic;
    using PCGClassLibrary.Interfaces;

    public class Down : IDirection
    {
        private IList<string> downForm = new List<string>()
        {
            "[]||||[]",
            "  ||||  ",
            "[]||||[]",
            "  \\||/ "
        };

        public IList<string> CarForm
        {
            get { return this.downForm; }
        }

        public void UpdateCoordinate(IPosition currentPosition)
        {
            currentPosition.YCoordinate++;
        }
    }
}