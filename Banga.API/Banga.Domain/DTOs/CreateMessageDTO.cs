﻿namespace Banga.Domain.DTOs
{
    public class CreateMessageDTO
    {
        public string RecipientUsername { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
