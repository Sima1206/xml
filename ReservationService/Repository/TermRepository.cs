using Microsoft.EntityFrameworkCore;
using ReservationService.Core;
using ReservationService.Model;

namespace ReservationService.Repository;

public class TermRepository : BaseRepository<Term>, ITermRepository
{
    public TermRepository(DbContext context) : base(context)
    {
    }

    public List<Term> GetAllTerms()
    {
        return ApplicationContext.Terms.Include(t => t.Accommodation).AsNoTracking().ToList();
    }

    public void UpdateTerm(Term term)
    {
        ApplicationContext.Entry(term).State = EntityState.Modified; 
        ApplicationContext.SaveChanges();
    }
    
    public Term GetTermById(long id)
    {
        return ApplicationContext.Terms.Where(t => t.Id == id).Include(t => t.Accommodation).AsNoTracking().FirstOrDefault();
    }
    
}