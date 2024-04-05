﻿namespace Banga.Domain.Models
{
    public class EmailSettings
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FromAddress { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty; 
        public int Port { get; set; }
        public bool UseSSL { get; set; }

    }
}
