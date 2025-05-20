namespace Backend.DTOs
{
    public class EmployeeSalesDto
    {
        public int EmployeeId { get; set; }
        public string? FullName { get; set; }
        public decimal TotalSold { get; set; }
    }

}
