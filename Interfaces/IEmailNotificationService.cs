namespace HotelReservation.Interfaces;

public interface IEmailNotificationService
{
    void SendEmail(string to, string subject, string body);
}
