namespace HotelReservation.Housekeeping.Domain;

using HotelReservation.Models;

public interface ICleaningNotifier
{
    void Notify(CleaningTask task);
}
