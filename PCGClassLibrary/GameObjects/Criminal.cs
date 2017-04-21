namespace PCGClassLibrary.GameObjects
{
    using System;
    using System.Collections.Generic;
    using PCGClassLibrary.Interfaces;

    public class Criminal : Position, ICriminal, IPosition
    {
        private int xMaxValue;
        private int yMaxValue;
        private IList<string> body = new List<string>() 
        { 
            " @",
            "/|\\",
            "/ \\"
        };

        public Criminal(int xMaxValue, int yMaxValue)
            : base(xMaxValue, yMaxValue)
        {
            this.xMaxValue = xMaxValue;
            this.yMaxValue = yMaxValue;
        }

        public IList<string> BodyFigure
        {
            get { return this.body; }
        }

        public void UpdatePosition(Random coordinatesGenerator)
        {
            if (coordinatesGenerator == null)
            {
                throw new ArgumentNullException("coordinatesGenerator");
            }

            int newXCoordinate = coordinatesGenerator.Next(2, this.xMaxValue - 1);
            int newYCoordinate = coordinatesGenerator.Next(2, this.yMaxValue - 1);
            this.SetNewPosition(newXCoordinate, newYCoordinate);
        }
    }
}
