using Microsoft.ML.Data;

namespace Backend.ML_Models
{
    public class TimeSeriesInput
    {
        [LoadColumn(0)]
        public DateTime Date;

        [LoadColumn(1)]
        public float Value;
    }
}
