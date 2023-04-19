using Microsoft.EntityFrameworkCore;
using XML.Core;
using XML.Model;

namespace XML.Repository
{
    public class AddressRepository : BaseRepository<Address>
    {
        public AddressRepository(DbContext context) : base(context)
        {
        }
    }
}
