using System.ComponentModel.DataAnnotations;

namespace Banga.Domain.Models
{
    public class Viewing
    {
        public long ViewingId { get; set; }
        [Required]
        public long PropertyId { get; set; }
        [Required]
        public int StatusId { get; set; }

        public int AssignedViewerId { get; set; }

        [MaxLength(500)]
        public string Note { get; set; }
    }
}
