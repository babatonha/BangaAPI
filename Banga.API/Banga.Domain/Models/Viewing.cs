using System.ComponentModel.DataAnnotations;

namespace Banga.Domain.Models
{
    public class Viewing
    {
        public long ViewingId { get; set; }
        [Required]
        public long PropertyId { get; set; }
        [Required]
        public DateTime ViewingDate { get; set; }

        public int AllocatedTo { get; set; }

        [MaxLength(500)]
        public string Note { get; set; } = string.Empty;
        public string ViewingStatus { get; set; } = string.Empty;
        public bool IsConfirmed { get; set; }
    }
}
