namespace HotelReservation.Services;

using HotelReservation.Models;

public class CancellationService
{
    private readonly ICancellationPolicy _cancellationPolicy;

    public CancellationService(ICancellationPolicy cancellationPolicy)
    {
        _cancellationPolicy = cancellationPolicy;
    }

    public decimal CalculateRefund(Reservation reservation, DateTime now)
    {
        return _cancellationPolicy.CalculateRefund(reservation, now);
    }

    public void CancelReservation(Reservation reservation, DateTime now)
    {
        if (reservation.CancellationPolicy != _cancellationPolicy.Name)
            throw new ArgumentException(
                $"CancellationService is configured with {_cancellationPolicy.Name} policy but reservation requires {reservation.CancellationPolicy}");

        var refund = CalculateRefund(reservation, now);
        reservation.Cancel();
        Console.WriteLine(
            $"[OK] Reservation {reservation.Id} cancelled " +
            $"({reservation.CancellationPolicy} policy: " +
            $"{(refund == reservation.TotalPrice ? "full" : "partial")} refund of {refund:F2} EUR)");
    }
}
