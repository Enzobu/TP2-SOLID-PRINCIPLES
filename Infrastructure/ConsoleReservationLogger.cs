namespace HotelReservation.Infrastructure;

using HotelReservation.Services;

public class ConsoleReservationLogger : IReservationLogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[LOG] {message}");
    }
}
