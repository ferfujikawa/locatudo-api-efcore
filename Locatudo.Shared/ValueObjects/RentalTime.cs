namespace Locatudo.Shared.ValueObjects
{
    public class RentalTime
    {
        public RentalTime(DateTime start)
        {
            Start = new DateTime(start.Year, start.Month, start.Day, start.Hour, 0, 0);
        }

        public DateTime Start { get; private set; }
    }
}
