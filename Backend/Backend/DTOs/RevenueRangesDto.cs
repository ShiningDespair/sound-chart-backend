namespace Backend.DTOs
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for revenue ranges.
    /// </summary>
    public class RevenueRangesDto
    {
        /// <summary>
        /// Gets or sets the minimum revenue value.
        /// </summary>
        public decimal Min { get; set; }
        /// <summary>
        /// Gets or sets the maximum revenue value.
        /// </summary>
        public decimal Max { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="RevenueRangesDto"/> class with specified minimum and maximum revenue values.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public RevenueRangesDto(decimal min, decimal max)
        {
            Min = min;
            Max = max;
        }
    }
}
