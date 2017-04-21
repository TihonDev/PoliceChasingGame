namespace PCGClassLibrary.Interfaces
{
    public interface IPosition
    {
        int XCoordinate { get; set; }

        int YCoordinate { get; set; }

        void SetNewPosition(int x, int y);
    }
}
