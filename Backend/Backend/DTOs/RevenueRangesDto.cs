namespace Backend.DTOs
{
    public class RevenueRangesDto
    {
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public RevenueRangesDto(decimal min, decimal max)
        {
            Min = min;
            Max = max;
        }
    }
}
