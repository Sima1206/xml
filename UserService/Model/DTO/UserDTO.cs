namespace UserService.Model.DTO
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long CityId { get; set; }
        public bool Enabled { get; set; }
        public UserType userType { get; set; }
    }
}
