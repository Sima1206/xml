using XML.Model;

namespace XML.Core
{
    public interface ICityRepository : IBaseRepository<City>
    {
        City GetCityById(long id);
    }
}
