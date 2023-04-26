using Microsoft.AspNetCore.Mvc;
using XML.Configuration;
using XML.Model;

namespace XML.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : BaseController<Address>
    {
        public AddressController(ProjectConfiguration configuration) : base(configuration)
        {
        }
    }
}
