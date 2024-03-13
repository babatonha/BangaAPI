namespace Banga.Domain.Models
{
    public class LawFirm
    {
       public int LawFirmID {get; set; }
       public string? LogoUrl { get; set; }
       public string? LawFirmName {get; set; }
       public string? Description {get; set; }
       public int RepresentativeUserId {get; set; }
       public string? Address {get; set; }
       public int CityID {get; set; }
       public bool IsDisabled { get; set; }  
    }
}
