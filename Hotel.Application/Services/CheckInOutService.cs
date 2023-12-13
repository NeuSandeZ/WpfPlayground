using AutoMapper;
using Hotel.Application.DTOS.CheckInsOutsDto;
using Hotel.Application.Services.Interfaces;
using Hotel.Domain.Entities;
using Hotel.Domain.IRepositories;

namespace Hotel.Application.Services;

public class CheckInOutService : ICheckInOutService
{
    private readonly ICheckInOutRepository _checkInOutRepository;
    private readonly IMapper _mapper;

    public CheckInOutService(ICheckInOutRepository checkInOutRepository, IMapper mapper)
    {
        _checkInOutRepository = checkInOutRepository;
        _mapper = mapper;
    }
    
    public async Task<int> GetTodaysCheckIns()
    {
        return  await _checkInOutRepository.GetTodaysCheckIns();
    }

    public async Task<int> GetTodaysCheckOuts()
    {
        return await _checkInOutRepository.GetTodaysCheckOuts();
    }

    public async Task<IEnumerable<ReservationComboBoxDto>> GetAllReservationNumbers()
    {
        var allReservationNumbers = await _checkInOutRepository.GetAllReservationNumbers();
        return _mapper.Map<IEnumerable<ReservationComboBoxDto>>(allReservationNumbers);
    }

    public void CreateCheckIn(CheckInDto checkInDto)
    {
        var checkIn = _mapper.Map<CheckIns>(checkInDto);
        _checkInOutRepository.CreateCheckIn(checkIn);
    }

    public async Task<IEnumerable<CheckInListingDto>> GetAllCheckIns()
    {
        var checkInsEnumerable = await _checkInOutRepository.GetAllCheckIns();
        return _mapper.Map<IEnumerable<CheckInListingDto>>(checkInsEnumerable);
    }

    public void CheckOut(CheckOutDto selectedCheckOut)
    {
        var checkOut = new CheckOuts()
        {
            CheckInsId = selectedCheckOut.CheckInId,
            CheckOutDate = selectedCheckOut.CheckOutDate
        };
        
        _checkInOutRepository.CheckOut(checkOut);
    }
}