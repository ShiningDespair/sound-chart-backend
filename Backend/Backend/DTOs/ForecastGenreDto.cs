namespace Backend.DTOs
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for genre forecasts.
    /// </summary>
    public class ForecastGenreDto
    {
        /// <summary>
        /// Gets or sets the name of the genre.
        /// </summary>
        public required string GenreName { get; set; }
        /// <summary>
        /// Gets or sets the total amount of sales that happened in the given genre in the last 3 months (Average).
        /// </summary>
        public decimal Last3MonthsAverage { get; set; }
        /// <summary>
        /// Gets or sets the total amount of forecasted sales that will happen in the given genre in the next 3 months (Average).
        /// </summary>
        public decimal Forecast3MonthsAverage { get; set; }


    }
}
