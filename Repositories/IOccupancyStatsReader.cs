namespace HotelReservation.Repositories;

public interface IOccupancyStatsReader
{
    Dictionary<string, int> GetOccupancyStats(DateTime from, DateTime to);
}
