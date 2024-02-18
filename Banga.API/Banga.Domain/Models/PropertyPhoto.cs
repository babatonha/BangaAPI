namespace Banga.Data.Models;

public partial class PropertyPhoto
{
    public long PropertyPhotoId { get; set; }

    public long PropertyId { get; set; }

    public string PhotoUrl { get; set; } = null!;
}
