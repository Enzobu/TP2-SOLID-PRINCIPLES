namespace HotelReservation.Services;

using HotelReservation.Models;

public class NonRefundablePolicy : ICancellationPolicy
{
    public string Name => "NonRefundable";

    public decimal CalculateRefund(Reservation reservation, DateTime now)
    {
        return 0m;
    }
}
