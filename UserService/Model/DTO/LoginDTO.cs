﻿namespace UserService.Model.DTO
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
    }
}
