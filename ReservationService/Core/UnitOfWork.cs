using ReservationService.Model;
using ReservationService.Repository;

namespace ReservationService.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private Dictionary<string, dynamic> _repositories;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;

            Accommodations = new AccommodationRepository(_context);
            Reservations = new ReservationRepository(_context);
        }

        public IAccommodationRepository Accomodations { get; private set; }
        public IReservationRepository Reservations { get; private set; }

        public IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, dynamic>();
            }

            string type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type))
            {
                return (IBaseRepository<TEntity>)_repositories[type];
            }
            Type repositoryType = typeof(IBaseRepository<>);

            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context));

            return (IBaseRepository<TEntity>)_repositories[type];
        }

        public ApplicationContext Context
        {
            get { return _context; }
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
