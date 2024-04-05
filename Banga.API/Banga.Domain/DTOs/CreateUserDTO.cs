namespace Banga.Domain.DTOs
{
    public class CreateUserDTO
    {
        public int Id { get; set; }   
        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? FullName { get; set; } = string.Empty;

        public string? FirstName { get; set; } = string.Empty;

        public string? LastName { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }
        public string? IdNumber { get; set;} = string.Empty;
    }
}
