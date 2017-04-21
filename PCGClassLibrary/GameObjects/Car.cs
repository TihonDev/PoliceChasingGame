namespace PCGClassLibrary.GameObjects
{
    using System;
    using PCGClassLibrary.InnerGameExceptions;
    using PCGClassLibrary.Interfaces;

    public delegate void CrashedCarEventHandler(ICar crashedCar);

    public delegate void CriminalCaughtEventHandler(ICar policeCar, ICriminal caughtCriminal);

    public class Car : Position, ICar, IPosition
    {
        private string brand;
        private string model;
        private string driverName;
        private ICriminal targetCriminal;

        public Car(ICriminal target, int xMaxValue, int yMaxValue)
            : base(xMaxValue, yMaxValue)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            this.targetCriminal = target;
        }

        public event CrashedCarEventHandler CarCrashedEvent;

        public event CriminalCaughtEventHandler CaughtCriminalEvent;

        public string Brand
        {
            get
            {
                return this.brand;
            }

            set
            {
                if (!this.IsValidValue(value, 2))
                {
                    throw new InvalidCarBrandException();
                }

                this.brand = value;
            }
        }

        public string Model
        {
            get
            {
                return this.model;
            }

            set
            {
                if (!this.IsValidValue(value, 1))
                {
                    throw new InvalidCarModelException();
                }

                this.model = value;
            }
        }

        public string DriverName
        {
            get
            {
                return this.driverName;
            }

            set
            {
                if (!this.IsValidValue(value, 2))
                {
                    throw new InvalidPlayerNameException();
                }

                this.driverName = value;
            }
        }

        public IDirection Direction { get; set; }

        public void Move()
        {
            try
            {
                this.Direction.UpdateCoordinate(this);
            }
            catch (CoordinateOutOfRangeException)
            {
                this.RaiseCarCrashedEvent();
            }

            if (this.IsCriminalCaught())
            {
                this.RaiseCriminalCaughtEvent();
            }
        }

        public bool IsCriminalCaught()
        {
            bool isCarOverTheCriminalX = this.XCoordinate - 1 <= this.targetCriminal.XCoordinate && this.targetCriminal.XCoordinate <= this.XCoordinate + this.Direction.CarForm[0].Length - 1;
            bool isCarOverTheCriminalY = this.YCoordinate <= this.targetCriminal.YCoordinate && this.targetCriminal.YCoordinate <= this.YCoordinate + this.Direction.CarForm.Count - 1;
            bool result = isCarOverTheCriminalX && isCarOverTheCriminalY;
            return result;
        }

        public void SpeedUp(ref int delayTime)
        {
            if (delayTime <= 20)
            {
                delayTime -= 1;
            }
            else
            {
                delayTime -= 20;
            }
        }

        public override string ToString()
        {
            var result = string.Format("{0} {1}", this.Brand, this.Model);
            return result;
        }

        private void RaiseCarCrashedEvent()
        {
            if (this.CarCrashedEvent != null)
            {
                this.CarCrashedEvent(this);
            }
        }

        private void RaiseCriminalCaughtEvent()
        {
            if (this.CaughtCriminalEvent != null)
            {
                this.CaughtCriminalEvent(this, this.targetCriminal);
            }
        }

        private bool IsValidValue(string newValue, byte minLength)
        {
            var isNullOrEmptyValue = string.IsNullOrEmpty(newValue) || string.IsNullOrWhiteSpace(newValue);
            if (isNullOrEmptyValue)
            {
                return false;
            }
            else
            {
                var trimmedValue = newValue.Trim();
                if (trimmedValue.Length < minLength)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
