namespace Banga.Data.Models;

public partial class PropertyOffer
{
    public long PropertyOfferId { get; set; }
    public long PropertyId { get; set; }
    public int OfferByUserId { get; set; }
    public string? BuyerName { get; set; }
    public string Description { get; set; } = string.Empty;
    public int StatusId { get; set; }
    public string? Status { get; set; }
    public int PaymentMethodId { get; set; }
    public string? PaymentMethod { get; set; }
    public double Amount { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? LastUpdatedDate { get; set;}

}
