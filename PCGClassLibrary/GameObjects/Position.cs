namespace PCGClassLibrary.GameObjects
{
    using PCGClassLibrary.InnerGameExceptions;
    using PCGClassLibrary.Interfaces;

    public class Position : IPosition
    {
        private const byte DEFAULT_VALUE_OF_MAX_COORDINATES = 200;
        private int xCurrentValue = 1;
        private int yCurrentValue = 1;
        private int xMaxValue;
        private int yMaxValue;
        private string exceptionMessageTemplate = "{0} coordinate is out of allowable range (1 - {1})";

        public Position()
        {
            this.xMaxValue = this.yMaxValue = DEFAULT_VALUE_OF_MAX_COORDINATES;
        }

        public Position(int xMaxValue, int yMaxValue)
        {
            if (xMaxValue < 2 || yMaxValue < 2)
            {
                throw new CoordinateOutOfRangeException("Both values of parameters should be greater than 1");
            }

            this.xMaxValue = xMaxValue;
            this.yMaxValue = yMaxValue;
        }

        public int XCoordinate
        {
            get
            {
                return this.xCurrentValue;
            }

            set
            {
                if (value <= 0 || this.xMaxValue <= value)
                {
                    this.xCurrentValue = value;
                    throw new CoordinateOutOfRangeException(string.Format(this.exceptionMessageTemplate, "X", this.xMaxValue - 1));
                }

                this.xCurrentValue = value;
            }
        }

        public int YCoordinate
        {
            get
            {
                return this.yCurrentValue;
            }

            set
            {
                if (value <= 0 || this.yMaxValue <= value)
                {
                    this.yCurrentValue = value;
                    throw new CoordinateOutOfRangeException(string.Format(this.exceptionMessageTemplate, "Y", this.yMaxValue - 1));
                }

                this.yCurrentValue = value;
            }
        }

        public void SetNewPosition(int x, int y)
        {
            this.XCoordinate = x;
            this.YCoordinate = y;
        }
    }
}
