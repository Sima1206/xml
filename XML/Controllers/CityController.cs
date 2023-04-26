using Microsoft.AspNetCore.Mvc;
using XML.Configuration;
using XML.Model;

namespace XML.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : BaseController<City>
    {
        public CityController(ProjectConfiguration configuration) : base(configuration)
        {
        }
    }
}
