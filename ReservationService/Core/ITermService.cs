using ReservationService.Model;
using ReservationService.Model.DTO;

namespace ReservationService.Core;

public interface ITermService
{
    public Term CreateTerm(TermDTO dto);

}