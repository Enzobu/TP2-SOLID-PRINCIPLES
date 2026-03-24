namespace HotelReservation.Repositories;

using HotelReservation.Models;

public interface IReservationDataRepository
{
    Room? GetRoomById(string roomId);
    bool IsRoomAvailable(string roomId, DateTime checkIn, DateTime checkOut);
    string NextReservationId();
    void SaveReservation(Reservation reservation);
    Reservation? GetReservation(string id);
    List<Reservation> GetAllReservations();
    List<Room> GetRooms();
    void Reset();
}
