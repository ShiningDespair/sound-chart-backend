
namespace Backend.DTOs
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for country revenue visualization.
    /// </summary>
    public record class CountryRevenueDto
    {
        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        public required string Country { get; set; }
        /// <summary>
        /// Gets or sets the total revenue generated from the country.
        /// </summary>
        public decimal TotalRevenue { get; set; }
    }
}
