namespace Banga.Data.Models;

public partial class City
{
    public int CityId { get; set; }

    public int ProviceId { get; set; }

    public string Name { get; set; } = null!;
}
