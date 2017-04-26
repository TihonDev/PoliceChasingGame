namespace PCGClassLibrary.GameObjects
{
    using System;
    using System.Collections.Generic;
    using PCGClassLibrary.Interfaces;

    public class Criminal : Position, ICriminal, IPosition
    {
        private int maxValueOfX;
        private int maxValueOfY;
        private IList<string> body = new List<string>()
        {
            " @",
            "/|\\",
            "/ \\"
        };

        public Criminal(int maxValueOfX, int maxValueOfY)
            : base(maxValueOfX, maxValueOfY)
        {
            this.maxValueOfX = maxValueOfX;
            this.maxValueOfY = maxValueOfY;
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

            int newXCoordinate = coordinatesGenerator.Next(2, this.maxValueOfX - 1);
            int newYCoordinate = coordinatesGenerator.Next(2, this.maxValueOfY - 1);
            this.SetNewPosition(newXCoordinate, newYCoordinate);
        }
    }
}
