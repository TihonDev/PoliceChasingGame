namespace PCGClassLibrary.Directions
{
    using System.Collections.Generic;
    using PCGClassLibrary.Interfaces;

    public class Left : IDirection
    {
        private IList<string> leftForm = new List<string>()
        {
            "   []  []",
            "/========",
            "\\========",
            "   []  []"
        };

        public IList<string> CarForm
        {
            get { return this.leftForm; }
        }

        public void UpdateCoordinate(IPosition currentPosition)
        {
            currentPosition.XCoordinate--;
        }
    }
}
