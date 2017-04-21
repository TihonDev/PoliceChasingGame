﻿namespace PCGClassLibrary.Managers
{
    using System.Collections.Generic;
    using PCGClassLibrary.Directions;
    using PCGClassLibrary.Interfaces;

    public class DirectionsManager
    {
        private IDirection up;
        private IDirection down;
        private IDirection right;
        private IDirection left;
        private Stack<IDirection> directionsHistory;

        public DirectionsManager()
        {
            this.directionsHistory = new Stack<IDirection>();
        }

        public IDirection GetDirection(string userChoice)
        {
            IDirection result = null;
            switch (userChoice)
            {
                case "UpArrow":
                    if (this.up == null)
                    {
                        this.up = new Up();
                    }

                    result = this.up;
                    break;
                case "DownArrow":
                    if (this.down == null)
                    {
                        this.down = new Down();
                    }

                    result = this.down;
                    break;
                case "RightArrow":
                    if (this.right == null)
                    {
                        this.right = new Right();
                    }

                    result = this.right;
                    break;
                case "LeftArrow":
                    if (this.left == null)
                    {
                        this.left = new Left();
                    }

                    result = this.left;
                    break;
                default:
                    if (this.directionsHistory.Count == 0)
                    {
                        this.directionsHistory.Push(new Right());
                    }

                    return this.directionsHistory.Peek();
            }

            this.directionsHistory.Push(result);
            return result;
        }

        public void ClearDirectionsHistory()
        {
            this.directionsHistory.Clear();
        }
    }
}