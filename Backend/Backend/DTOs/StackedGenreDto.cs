namespace Backend.DTOs
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for genre sales data, used for D3.js stacked bar chart visualizations.
    /// </summary>
    public class StackedGenreDto
    {
        /// <summary>
        /// Represents the country where the sales occurred.
        /// </summary>
        public required string Country { get; set; }
        /// <summary>
        /// Represents the genre of the music or product sold.
        /// </summary>
        public required string Genre { get; set; }
        /// <summary>
        /// Represents the total amount of sales in the specified genre for the given country.
        /// </summary>
        public required decimal TotalSpent { get; set; }
    }
}
