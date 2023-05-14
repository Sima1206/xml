using ReservationService.Model;

namespace ReservationService.Core;

public interface ITermRepository: IBaseRepository<Term>
{
    List<Term> GetAllTerms();

    void UpdateTerm(Term term);

    Term GetTermById(long id);
}