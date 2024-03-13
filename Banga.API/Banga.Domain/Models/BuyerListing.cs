namespace Banga.Domain.Models
{
    public class BuyerListing
    {
        public long BuyerListingId { get; set; }    
        public int UserId { get; set; }
        public double Budget { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }   
        public bool IsDisabled { get; set; }    
    }
}
