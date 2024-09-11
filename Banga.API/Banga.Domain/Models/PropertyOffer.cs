namespace Banga.Data.Models;

public partial class PropertyOffer
{
    public long PropertyOfferId { get; set; }
    public long PropertyId { get; set; }
    public int OfferByUserId { get; set; }
    public string? BuyerName { get; set; }
    public string Description { get; set; }
    public double Amount { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? LastUpdatedDate { get; set;}
    public bool? IsAccepted { get; set; }
    public bool? IsOfferConfirmed { get; set; }
}
