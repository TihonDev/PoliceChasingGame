namespace PCGClassLibrary.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface ICriminal : IPosition
    {
        IList<string> BodyFigure { get; }

        void UpdatePosition(Random coordinatesGenerator);
    }
}
