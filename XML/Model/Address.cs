namespace XML.Model
{
    public class Address : Entity
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public City City { get; set; }
    }
}
