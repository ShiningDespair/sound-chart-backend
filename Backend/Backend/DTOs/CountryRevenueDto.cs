
namespace Backend.DTOs
{
   
    public record class CountryRevenueDto
    {
        public required string Country { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
