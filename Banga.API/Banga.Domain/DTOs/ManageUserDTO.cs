namespace Banga.Domain.DTOs
{
    public class ManageUserDTO
    {
        public int Id { get; set; }  
        public string RoleName { get; set; } = string.Empty;
        public bool IsBlocked { get; set; }
    }
}
