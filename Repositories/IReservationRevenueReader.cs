namespace HotelReservation.Repositories;

public interface IReservationRevenueReader
{
    decimal GetTotalRevenue(DateTime from, DateTime to);
}
