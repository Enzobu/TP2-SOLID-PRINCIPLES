namespace HotelReservation.Interfaces;

public interface ISmsNotificationService
{
    void SendSms(string phoneNumber, string message);
}
