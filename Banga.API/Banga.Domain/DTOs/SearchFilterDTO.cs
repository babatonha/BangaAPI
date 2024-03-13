namespace Banga.Domain.DTOs
{
    public class SearchFilterDTO
    {
        public string[] SearchTerms { get; set; }
        public int PropertyTypeId { get; set; }
        public int RegistrationTypeId { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public int Beds { get; set; }
        public int Baths { get; set; }
    }
}
