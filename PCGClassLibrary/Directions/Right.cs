namespace PCGClassLibrary.Directions
{
    using System.Collections.Generic;
    using PCGClassLibrary.Interfaces;

    public class Right : IDirection
    {
        private IList<string> rightForm = new List<string>()
        {
            "[]  []   ",
            "========\\",
            "========/",
            "[]  []   "
        };

        public IList<string> CarForm
        {
            get { return this.rightForm; }
        }

        public void UpdateCoordinate(IPosition currentPosition)
        {
            currentPosition.XCoordinate++;
        }
    }
}
