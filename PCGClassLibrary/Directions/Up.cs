namespace PCGClassLibrary.Directions
{
    using System.Collections.Generic;
    using PCGClassLibrary.Interfaces;

    public class Up : IDirection
    {
        private IList<string> upperForm = new List<string>()
        {
            "  /||\\ ",
            "[]||||[]",
            "  ||||  ",
            "[]||||[]"
        };

        public IList<string> CarForm
        {
            get { return this.upperForm; }
        }

        public void UpdateCoordinate(IPosition currentPosition)
        {
            currentPosition.YCoordinate--;
        }
    }
}
