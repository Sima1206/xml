using AutoMapper;
using ReservationService.Model;
using ReservationService.Model.DTO;

namespace ReservationService;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ReservationDTO, Reservation>(); //maper iz dto u rezervaciju
        
        CreateMap<AccommodationDTO, Accommodation>();

        CreateMap<TermDTO, Term>();
    }
}