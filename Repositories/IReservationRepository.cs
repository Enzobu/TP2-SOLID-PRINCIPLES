namespace HotelReservation.Repositories;

public interface IReservationRepository :
    IReservationReader,
    IReservationWriter,
    IReservationRevenueReader,
    IOccupancyStatsReader
{
}
