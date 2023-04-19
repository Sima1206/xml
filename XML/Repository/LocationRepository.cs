using Microsoft.EntityFrameworkCore;
using XML.Core;
using XML.Model;

namespace XML.Repository
{
    public class LocationRepository : BaseRepository<Location>
    {
        public LocationRepository(DbContext context) : base(context)
        {
        }
    }
}
