namespace PCGClassLibrary.Managers
{
    using System;
    using System.Collections.Generic;
    using PCGClassLibrary.Directions;
    using PCGClassLibrary.Interfaces;

    public class DirectionsManager
    {
        private Dictionary<string, IDirection> directionsContainer;
        private Queue<IDirection> directionsHistory;

        public DirectionsManager()
        {
            this.directionsContainer = new Dictionary<string, IDirection>();
            this.directionsContainer.Add("UpArrow", new Up());
            this.directionsContainer.Add("DownArrow", new Down());
            this.directionsContainer.Add("RightArrow", new Right());
            this.directionsContainer.Add("LeftArrow", new Left());
            this.directionsHistory = new Queue<IDirection>();
        }

        public IDirection GetDirection(string userChoice)
        {
            IDirection result = null;
            try
            {
                result = this.directionsContainer[userChoice];
            }
            catch (KeyNotFoundException)
            {
                result = this.GetTheLastDirection();
            }
            catch (ArgumentNullException)
            {
                result = this.GetTheLastDirection();
            }

            this.directionsHistory.Enqueue(result);
            if (this.directionsHistory.Count > 1)
            {
                this.directionsHistory.Dequeue();
            }

            return result;
        }

        public void ClearDirectionsHistory()
        {
            this.directionsHistory.Clear();
        }

        private IDirection GetTheLastDirection()
        {
            if (this.directionsHistory.Count == 0)
            {
                this.directionsHistory.Enqueue(new Right());
            }

            return this.directionsHistory.Peek();
        }
    }
}
