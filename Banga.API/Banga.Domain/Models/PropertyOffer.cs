namespace Banga.Data.Models;

public partial class PropertyOffer
{
    public long PropertyOfferId { get; set; }

    public long PropertyId { get; set; }

    public int OfferBy { get; set; }

    public double Amount { get; set; }
}
