namespace Backend.ML_Models
{
    /// <summary>
    /// Represents the output data structure for forecasting models.
    /// </summary>
    public class ForecastOutput
    {
        /// <summary>
        /// The forecasted values for the next time periods.
        /// </summary>
        public float[]? ForecastedValue { get; set; }
    }
}
