namespace PCGClassLibrary.Interfaces
{
    public interface ICar : IPosition
    {
        string Brand { get; set; }

        string Model { get; set; }

        string DriverName { get; set; }

        IDirection Direction { get; set; }

        void Move();

        bool IsCriminalCaught();

        void SpeedUp(ref int delayTime);

        string ToString();
    }
}
