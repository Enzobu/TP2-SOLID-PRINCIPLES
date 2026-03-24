namespace HotelReservation.Services;

using HotelReservation.Models;

public interface IReservationDomainService
{
    Room GetRoomOrThrow(string roomId);
    void ValidateGuestCapacity(Room room, int guestCount);
    void EnsureAvailability(string roomId, DateTime checkIn, DateTime checkOut);
    decimal CalculateTotalPrice(Room room, DateTime checkIn, DateTime checkOut);
}
