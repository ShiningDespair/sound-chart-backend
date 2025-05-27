namespace Backend.DTOs
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for world map visualization of total sales by country on D3.js World Map Chart.
    /// </summary>
    public class WorldMapDto
    {
        /// <summary>
        /// Gets or sets the ISO code of the country.
        /// </summary>
        public required int CountryIsoCode { get; set; }
        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        public required string Country { get; set; }
        /// <summary>
        /// Gets or sets the amount of total spendings for the country.
        /// </summary>
        public required decimal TotalSpent { get; set; }
    }
}
