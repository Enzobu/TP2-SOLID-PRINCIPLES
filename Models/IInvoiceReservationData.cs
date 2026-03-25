namespace HotelReservation.Models;

public interface IInvoiceReservationData
{
    string Id { get; }
    string GuestName { get; }
    string RoomId { get; }
    DateTime CheckIn { get; }
    DateTime CheckOut { get; }
    int GuestCount { get; }
    string RoomType { get; }
}
