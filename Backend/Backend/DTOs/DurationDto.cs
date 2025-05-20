namespace Backend.DTOs
{
    public class DurationDto
    {

        public int Min { get; set; }
        public int Max { get; set; }
        public DurationDto(int min, int max)
        {
            Min = min;
            Max = max;
        }
    }
}
