using XML.Configuration;
using XML.Model;

namespace XML.Controllers
{
    public class CityController : BaseController<City>
    {
        public CityController(ProjectConfiguration configuration) : base(configuration)
        {
        }
    }
}
