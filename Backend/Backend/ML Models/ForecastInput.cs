namespace Backend.ML_Models
{
    /// <summary>
    /// Represents the input data structure for forecasting models.
    /// </summary>
    public class ForecastInput
    {
        /// <summary>
        /// The name of the item being forecasted (e.g., country or genre).
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// The date associated with the forecast input, formatted as yyyy-MM-dd.
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// The value associated with the forecast input, typically representing sales or some other metric.
        /// </summary>
        public float Value { get; set; }
    }
}
