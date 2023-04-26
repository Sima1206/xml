using Microsoft.AspNetCore.Mvc;
using XML.Configuration;
using XML.Model;

namespace XML.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : BaseController<Location>
    {
        public LocationController(ProjectConfiguration configuration) : base(configuration)
        {
        }
    }
}
