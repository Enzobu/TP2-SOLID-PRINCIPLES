namespace HotelReservation.Services;

using HotelReservation.Repositories;

public class BillingService
{
    private readonly IReservationRevenueReader _repo;

    public BillingService(IReservationRevenueReader repo)
    {
        _repo = repo;
    }

    public decimal GetRevenueForPeriod(DateTime from, DateTime to)
    {
        return _repo.GetTotalRevenue(from, to);
    }
}
