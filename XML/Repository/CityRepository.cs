using Microsoft.EntityFrameworkCore;
using XML.Core;
using XML.Model;

namespace XML.Repository
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(DbContext context) : base(context)
        {
        }
        public City GetCityById(long id)
        {
            return ApplicationContext.Cities.Where(x => x.Id == id && !x.Deleted).FirstOrDefault();

        }
    }
}
