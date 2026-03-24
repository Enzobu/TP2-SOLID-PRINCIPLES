namespace HotelReservation.Services;

using HotelReservation.Models;
using HotelReservation.Infrastructure;
using HotelReservation.Repositories;

// SRP VIOLATION (Example 1): This class mixes three levels of concern:
// - INFRASTRUCTURE: direct data access, logging
// - BUSINESS: availability check, price calculation, validation
// - APPLICATION: workflow orchestration
public class ReservationService
{
    private readonly IReservationDataRepository _repository;
    private readonly IReservationDomainService _domainService;
    private readonly IReservationLogger _logger;

    public ReservationService()
        : this(new InMemoryReservationDataRepository(), null, new ConsoleReservationLogger())
    {
    }

    public ReservationService(
        IReservationDataRepository repository,
        IReservationDomainService? domainService,
        IReservationLogger logger)
    {
        _repository = repository;
        _domainService = domainService ?? new ReservationDomainService(repository);
        _logger = logger;
    }

    public string CreateReservation(string guestName, string roomId, DateTime checkIn,
        DateTime checkOut, int guestCount, string roomType, string email)
    {
        _logger.Log($"Creating reservation for {guestName}...");

        var room = _domainService.GetRoomOrThrow(roomId);
        _domainService.ValidateGuestCapacity(room, guestCount);
        _domainService.EnsureAvailability(roomId, checkIn, checkOut);
        var total = _domainService.CalculateTotalPrice(room, checkIn, checkOut);

        var reservation = new Reservation
        {
            Id = _repository.NextReservationId(),
            GuestName = guestName,
            RoomId = roomId,
            CheckIn = checkIn,
            CheckOut = checkOut,
            GuestCount = guestCount,
            RoomType = roomType,
            Status = "Confirmed",
            Email = email,
            TotalPrice = total
        };

        _repository.SaveReservation(reservation);
        _logger.Log($"Reservation {reservation.Id} created.");

        return reservation.Id;
    }

    public Reservation? GetReservation(string id)
    {
        return _repository.GetReservation(id);
    }

    public List<Reservation> GetAllReservations()
    {
        return _repository.GetAllReservations();
    }

    public static List<Room> GetRooms() => new InMemoryReservationDataRepository().GetRooms();

    public static void Reset()
    {
        new InMemoryReservationDataRepository().Reset();
    }
}
