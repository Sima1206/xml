using XML.Configuration;
using XML.Model;

namespace XML.Controllers
{
    public class LocationController : BaseController<Location>
    {
        public LocationController(ProjectConfiguration configuration) : base(configuration)
        {
        }
    }
}
