namespace Backend.DTOs
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for duration filtering in the Chinook database.
    /// </summary>
    public class DurationDto
    {
        /// <summary>
        /// Gets or sets the minimum duration in seconds.
        /// </summary>
        public int Min { get; set; }
        /// <summary>
        /// Gets or sets the maximum duration in seconds.
        /// </summary>
        public int Max { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DurationDto"/> class with specified minimum and maximum durations.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public DurationDto(int min, int max)
        {
            Min = min;
            Max = max;
        }
    }
}
