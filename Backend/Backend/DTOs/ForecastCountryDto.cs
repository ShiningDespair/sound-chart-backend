namespace Backend.DTOs
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for country-specific forecast data.
    /// </summary>
    public class ForecastCountryDto
    {
        /// <summary>
        /// Gets or sets the name of the country for which the forecast is made.
        /// </summary>
        public required string CountryName { get; set; }
        /// <summary>
        /// Gets or sets the total amount of sales that happend in the given country in last 3 months (Average).
        /// </summary>
        public decimal Last3MonthsAverage { get; set; }
        /// <summary>
        /// Gets or sets the total amount of forcasted sales that happend in the given country in next 3 months (Average).
        /// </summary>
        public decimal Forecast3MonthsAverage { get; set; }
    }
}
