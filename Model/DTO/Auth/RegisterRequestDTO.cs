﻿namespace WebApiRis.Model.DTO.Auth
{
    public class RegisterRequestDTO
    {
        public string? Login { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
