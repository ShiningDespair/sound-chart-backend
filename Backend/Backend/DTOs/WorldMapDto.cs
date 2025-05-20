namespace Backend.DTOs
{
    public class WorldMapDto
    {
        public required int CountryIsoCode { get; set; }
        public required string Country { get; set; }
        public required decimal TotalSpent { get; set; }
    }
}
