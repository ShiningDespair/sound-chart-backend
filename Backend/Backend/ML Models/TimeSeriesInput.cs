using Microsoft.ML.Data;

namespace Backend.ML_Models
{
    /// <summary>
    /// Represents the input data structure for time series forecasting models.
    /// </summary>
    public class TimeSeriesInput
    {
        /// <summary>
        /// The date of the observation in the time series.
        /// </summary>
        [LoadColumn(0)]
        public DateTime Date;

        /// <summary>
        /// The value associated with the observation, typically representing sales or some other metric.
        /// </summary>
        [LoadColumn(1)]
        public float Value;
    }
}
