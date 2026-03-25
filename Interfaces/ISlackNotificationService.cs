namespace HotelReservation.Interfaces;

public interface ISlackNotificationService
{
    void SendSlackMessage(string channel, string message);
}
