namespace HotelReservation.Repositories;

using HotelReservation.Models;

public class InMemoryReservationDataRepository : IReservationDataRepository
{
    private static readonly Dictionary<string, Reservation> Reservations = new();
    private static readonly List<Room> Rooms = new()
    {
        new Room { Id = "101", Type = "Standard", MaxGuests = 2, PricePerNight = 80m },
        new Room { Id = "102", Type = "Standard", MaxGuests = 2, PricePerNight = 80m },
        new Room { Id = "201", Type = "Suite", MaxGuests = 2, PricePerNight = 200m },
        new Room { Id = "301", Type = "Family", MaxGuests = 4, PricePerNight = 120m }
    };

    private static int _counter;

    public Room? GetRoomById(string roomId)
    {
        return Rooms.FirstOrDefault(r => r.Id == roomId);
    }

    public bool IsRoomAvailable(string roomId, DateTime checkIn, DateTime checkOut)
    {
        return !Reservations.Values.Any(r =>
            r.RoomId == roomId &&
            r.Status != "Cancelled" &&
            r.CheckIn < checkOut &&
            r.CheckOut > checkIn);
    }

    public string NextReservationId()
    {
        _counter++;
        return $"R-{_counter:D3}";
    }

    public void SaveReservation(Reservation reservation)
    {
        Reservations[reservation.Id] = reservation;
    }

    public Reservation? GetReservation(string id)
    {
        return Reservations.TryGetValue(id, out var reservation) ? reservation : null;
    }

    public List<Reservation> GetAllReservations()
    {
        return Reservations.Values.ToList();
    }

    public List<Room> GetRooms()
    {
        return Rooms;
    }

    public void Reset()
    {
        Reservations.Clear();
        _counter = 0;
    }
}
