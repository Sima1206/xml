using XML.Configuration;
using XML.Model;

namespace XML.Controllers
{
    public class AddressController : BaseController<Address>
    {
        public AddressController(ProjectConfiguration configuration) : base(configuration)
        {
        }
    }
}
