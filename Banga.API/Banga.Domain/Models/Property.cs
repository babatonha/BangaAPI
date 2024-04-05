namespace Banga.Data.Models;

public partial class Property
{
    public long PropertyId { get; set; }
    public int? OwnerId { get; set; }
    public string? OwnerName { get; set; }
    public int? AssignedLawyerId { get; set; }
    public int? StatusID { get; set; }
    public string? StatusName { get; set; }
    public string? AssignedLawyerName { get; set; }
    public int PropertyTypeId { get; set; }
    public string? PropertyTypeName { get; set; }
    public int? CityId { get; set; }
    public string? CityName { get; set; }
    public int? ProvinceId { get; set; }
    public string? ProvinceName { get; set; }
    public string Address { get; set; } = null!;
    public double Price { get; set; }
    public string Description { get; set; } = null!;
    public int? NumberOfRooms { get; set; }
    public int? NumberOfBathrooms { get; set; }
    public int? ParkingSpots { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? YoutubeUrl { get; set; }
    public bool? HasLawyer { get; set; }
    public int? NumberOfLikes { get; set; }
    public string? SuburbName { get; set; }
    public int? SuburbId { get; set; }
    public string? SqureMeters { get; set; }
    public string? Amenities { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? LastUpdatedDate { get; set; }  
    public bool? IsSold { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsDeleted { get; set; }
}
