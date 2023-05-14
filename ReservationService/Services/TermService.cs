using ReservationService.Core;
using ReservationService.Model;
using ReservationService.Model.DTO;

namespace TermService.Services;

public class TermService : ITermService
{
    public Term CreateTerm(TermDTO dto)
    {
        try
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());

            Term term = new Term();

            Accommodation acc = unitOfWork.Accommodations.Get(dto.AccommodationId);

            term.Accommodation = acc;
            term.StartDate = dto.StartDate;
            term.EndDate = dto.EndDate;
            term.AdditionPrice = dto.AdditionPrice;

            if (!checkDates(dto.StartDate, dto.EndDate, dto.AccommodationId, term.Id))
                return null;

            unitOfWork.Terms.Add(term);
            unitOfWork.Complete();

            return term;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public Term UpdateTerm(long id, Term term, long accommodationId)
    {
        try
        {
            using UnitOfWork unitOfWork = new(new ApplicationContext());
            List<Reservation> reservations = unitOfWork.Reservations.GetAllReservations()
                .Where(r => r.Term.Id == id).ToList();
            if (reservations.Count > 0)
                return null;
            term.Id = id;
            
            if (!checkDates(term.StartDate, term.EndDate, accommodationId, term.Id))
                return null;
            
            unitOfWork.Terms.UpdateTerm(term);

        }
        catch (Exception e)
        {
            return null;
        }

        return term;
    }
    
    private bool checkDates(DateTime StartDate, DateTime EndDate, long AccommodationId, long TermId)
    {
        using UnitOfWork unitOfWork = new(new ApplicationContext());
            
        if ((EndDate - StartDate).TotalDays <= 0)
        {
            return false;
        }

        var allTerms = unitOfWork.Terms.GetAllTerms().Where(t => t.Id != TermId).ToList();
        if (allTerms.Count > 0)
        {
            var dateIncludes = allTerms
                .Any(d => d.Accommodation.Id == AccommodationId && 
                          ((StartDate >= d.StartDate && StartDate < d.EndDate) || (EndDate >= d.StartDate 
                               && EndDate < d.EndDate) || (d.StartDate >= StartDate  && d.StartDate < EndDate) ||
                           (d.EndDate >= StartDate && d.EndDate < EndDate)));

            if (dateIncludes)
            {
                return false;
            }
        }

        return true;
    }

}