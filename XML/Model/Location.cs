using XML.Repository;

namespace XML.Model
{
    public class Location : Entity
    {
        public Address Address { get; set; }
        public string Country { get; set; }
    }
}
