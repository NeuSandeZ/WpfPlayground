using AutoMapper;
using Hotel.Application.DTOS.ReservationListingDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Domain.Entities;
using Hotel.Domain.IRepositories;

namespace Hotel.Application.Services;

public class ReservationListingService : IReservationListingService
{
    private readonly IMapper _mapper;
    private readonly IReservationListingRepository _reservationListingRepository;

    public ReservationListingService(IReservationListingRepository reservationListingRepository, IMapper mapper)
    {
        _reservationListingRepository = reservationListingRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReservationDto>> GetAllReservations()
    {
        var reservations = await _reservationListingRepository.GetAllReservations();
        return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
    }

    public async Task CreateReservation(AddReservationDto reservationDto)
    {
        var reservation = _mapper.Map<Reservation>(reservationDto);
        await _reservationListingRepository.CreateReservation(reservation);
    }

    public IEnumerable<AvailableRoomsDto> GetAllRoomsWithRoomStatus(DateTime checkInDate, DateTime checkOutDate)
    {
        var allRoomsWithRoomStatus = _reservationListingRepository.GetAllRoomsWithRoomStatus(checkInDate, checkOutDate);
        return _mapper.Map<IEnumerable<AvailableRoomsDto>>(allRoomsWithRoomStatus);
    }
}