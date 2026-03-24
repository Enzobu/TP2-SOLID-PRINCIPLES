namespace HotelReservation.Services;

using HotelReservation.Models;

// SRP VIOLATION (Example 2): A single method mixes multiple levels of abstraction.
// High-level business rules sit next to low-level cache manipulation and config reading.
public class CheckInService
{
    private readonly Dictionary<string, CacheEntry> _cache = new();
    private readonly Dictionary<string, Reservation> _dataStore;
    private const decimal LateCheckInFee = 25m;

    public CheckInService(Dictionary<string, Reservation> dataStore)
    {
        _dataStore = dataStore;
    }

    public void ProcessCheckIn(Reservation reservation)
    {
        EnsureCanCheckIn(reservation);
        RefreshCacheForCheckIn(reservation);
        ApplyLateCheckInFeeIfNeeded(reservation);
        MarkCheckedIn(reservation);
        NotifyRoomOccupied(reservation);
    }

    public void ProcessCheckOut(Reservation reservation)
    {
        EnsureCanCheckOut(reservation);
        MarkCheckedOut(reservation);
        RemoveFromCache(reservation.Id);
        NotifyRoomFree(reservation);
    }

    private static void EnsureCanCheckIn(Reservation reservation)
    {
        if (reservation.Status != "Confirmed")
            throw new Exception($"Cannot check in: reservation is {reservation.Status}");
    }

    private void RefreshCacheForCheckIn(Reservation reservation)
    {
        RemoveFromCache(reservation.Id);
        _cache[reservation.Id] = new CacheEntry(DateTime.Now, "CheckedIn");
    }

    private static void ApplyLateCheckInFeeIfNeeded(Reservation reservation)
    {
        if (DateTime.Now.Hour >= 22)
            reservation.TotalPrice += LateCheckInFee;
    }

    private static void MarkCheckedIn(Reservation reservation)
    {
        reservation.Status = "CheckedIn";
    }

    private static void NotifyRoomOccupied(Reservation reservation)
    {
        Console.WriteLine($"[SMS] Room {reservation.RoomId} is now occupied");
    }

    private static void EnsureCanCheckOut(Reservation reservation)
    {
        if (reservation.Status != "CheckedIn")
            throw new Exception($"Cannot check out: reservation is {reservation.Status}");
    }

    private static void MarkCheckedOut(Reservation reservation)
    {
        reservation.Status = "CheckedOut";
    }

    private void RemoveFromCache(string reservationId)
    {
        if (_cache.ContainsKey(reservationId))
            _cache.Remove(reservationId);
    }

    private static void NotifyRoomFree(Reservation reservation)
    {
        Console.WriteLine($"[SMS] Room {reservation.RoomId} is now free");
    }
}
