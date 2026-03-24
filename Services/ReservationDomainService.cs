namespace HotelReservation.Services;

using HotelReservation.Models;
using HotelReservation.Repositories;

public class ReservationDomainService : IReservationDomainService
{
    private readonly IReservationDataRepository _repository;

    public ReservationDomainService(IReservationDataRepository repository)
    {
        _repository = repository;
    }

    public Room GetRoomOrThrow(string roomId)
    {
        var room = _repository.GetRoomById(roomId);
        if (room == null)
            throw new Exception($"Room {roomId} not found");

        return room;
    }

    public void ValidateGuestCapacity(Room room, int guestCount)
    {
        if (guestCount > room.MaxGuests)
            throw new Exception($"Room {room.Id} max capacity is {room.MaxGuests}");
    }

    public void EnsureAvailability(string roomId, DateTime checkIn, DateTime checkOut)
    {
        var isAvailable = _repository.IsRoomAvailable(roomId, checkIn, checkOut);
        if (!isAvailable)
            throw new Exception($"Room {roomId} is not available for {checkIn:dd/MM} -> {checkOut:dd/MM}");
    }

    public decimal CalculateTotalPrice(Room room, DateTime checkIn, DateTime checkOut)
    {
        var nights = (checkOut - checkIn).Days;
        return nights * room.PricePerNight;
    }
}
