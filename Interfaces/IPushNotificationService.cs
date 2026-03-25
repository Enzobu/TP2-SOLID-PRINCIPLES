namespace HotelReservation.Interfaces;

public interface IPushNotificationService
{
    void SendPushNotification(string deviceId, string message);
}
