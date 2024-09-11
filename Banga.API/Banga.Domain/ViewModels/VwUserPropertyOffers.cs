namespace Banga.Domain.ViewModels
{
    public class VwUserPropertyOffers
    {
        public long PropertyOfferId { get; set; }
        public long PropertyId { get; set; }
        public string Description { get; set; }
        public bool IsAccepted { get; set; }
        public double Amount { get; set; }
        public int OfferByUserId { get; set; }
        public string PropertyType { get; set; }
        public double Price { get; set; }
        public string Address { get; set; }
        public bool IsOfferConfirmed { get; set; }
    }
}
