namespace Banga.Domain.DTOs
{
    public class ManagePropertyDTO
    {
        public long PropertyId { get; set; }
        public bool? IsSold { get; set; }
        public bool? IsActive { get; set; }
    }
}
