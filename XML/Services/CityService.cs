using XML.Core;
using XML.Model;

namespace XML.Services
{
    public class CityService : ICityService
    {
        public City GetCityById(long id)
        {
            try
            {
                using UnitOfWork unitOfWork = new(new ApplicationContext());
                City city = unitOfWork.Cities.GetCityById(id);

                return city;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
