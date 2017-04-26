namespace PCGClassLibrary.GameObjects
{
    using PCGClassLibrary.InnerGameExceptions;
    using PCGClassLibrary.Interfaces;

    public class Position : IPosition
    {
        private const byte DEFAULT_VALUE_OF_MAX_COORDINATES = 200;
        private int currentValueOfX = 1;
        private int currentValueOfY = 1;
        private int maxValueOfX;
        private int maxValueOfY;
        private string exceptionMessageTemplate = "{0} coordinate is out of allowable range (1 - {1})";

        public Position()
        {
            this.maxValueOfX = this.maxValueOfY = DEFAULT_VALUE_OF_MAX_COORDINATES;
        }

        public Position(int maxValueOfX, int maxValueOfY)
        {
            if (maxValueOfX < 2 || maxValueOfY < 2)
            {
                throw new CoordinateOutOfRangeException("Both values of parameters should be greater than 1");
            }

            this.maxValueOfX = maxValueOfX;
            this.maxValueOfY = maxValueOfY;
        }

        public int XCoordinate
        {
            get
            {
                return this.currentValueOfX;
            }

            set
            {
                if (value <= 0 || this.maxValueOfX <= value)
                {
                    this.currentValueOfX = value;
                    throw new CoordinateOutOfRangeException(string.Format(this.exceptionMessageTemplate, "X", this.maxValueOfX - 1));
                }

                this.currentValueOfX = value;
            }
        }

        public int YCoordinate
        {
            get
            {
                return this.currentValueOfY;
            }

            set
            {
                if (value <= 0 || this.maxValueOfY <= value)
                {
                    this.currentValueOfY = value;
                    throw new CoordinateOutOfRangeException(string.Format(this.exceptionMessageTemplate, "Y", this.maxValueOfY - 1));
                }

                this.currentValueOfY = value;
            }
        }

        public void SetNewPosition(int x, int y)
        {
            this.XCoordinate = x;
            this.YCoordinate = y;
        }
    }
}
