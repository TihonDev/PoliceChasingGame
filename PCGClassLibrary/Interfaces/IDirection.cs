namespace PCGClassLibrary.Interfaces
{
    using System.Collections.Generic;

    public interface IDirection
    {
        IList<string> CarForm { get; }

        void UpdateCoordinate(IPosition currentPosition);
    }
}
