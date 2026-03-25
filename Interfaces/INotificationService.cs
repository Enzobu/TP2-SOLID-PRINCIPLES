namespace HotelReservation.Interfaces;

public interface INotificationService :
    IEmailNotificationService,
    ISmsNotificationService,
    IPushNotificationService,
    ISlackNotificationService
{
}
