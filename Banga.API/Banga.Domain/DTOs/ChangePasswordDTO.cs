﻿namespace Banga.Domain.DTOs
{
    public class ChangePasswordDTO
    {
        public string Username { get; set; } = string.Empty;
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;    
    }
}
