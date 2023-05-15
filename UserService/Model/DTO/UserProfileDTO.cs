namespace UserService.Model.DTO;

public class UserProfileDTO
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public long CityId { get; set; }
    public string userType { get; set; }

    public static User ConvertToUser(UserProfileDTO dto)
    {
        User user = new User();
        user.Id = dto.Id;
        user.Name = dto.Name;
        user.Surname = dto.Surname;
        user.Email = dto.Email;
        user.Password = dto.Password;
        user.CityId = dto.CityId;
        if (dto.userType.Equals("Host"))
        {
            user.userType = UserType.Host;
        }
        else
        {
            user.userType = UserType.Guest;
        }

        return user;

    }
}