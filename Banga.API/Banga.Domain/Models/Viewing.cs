using System.ComponentModel.DataAnnotations;

namespace Banga.Domain.Models
{
    public class Viewing
    {
        public long Id { get; set; }
        [Required]
        public long PropertyId { get; set; }
        [MaxLength(250)]
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public DateTime Start { get; set; }

        public int AllocatedTo { get; set; }

        [MaxLength(500)]
        public string Note { get; set; } = string.Empty;
        public string ViewingStatus { get; set; } = string.Empty;
    }
}
