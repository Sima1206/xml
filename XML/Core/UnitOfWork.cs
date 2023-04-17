using Microsoft.AspNetCore.Hosting.Server.Features;
using XML.Model;
using XML.Repository;

namespace XML.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private Dictionary<string, dynamic> _repositories;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;

            Locations = new LocationRepository(_context);
            Cities = new CityRepository(_context);
            Addresses = new AddressRepository(_context);
        }

        public ILocationRepository Locations { get; private set; }
        public ICityRepository Cities { get; private set; }
        public IAddressRepository Addresses { get; private set; }

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
