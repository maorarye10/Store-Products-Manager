﻿namespace Backend.DTOs.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role {  get; set; } = string.Empty;
        public DateTime LastLogin { get; set; }
        public string token { get; set; } = string.Empty;

    }
}
