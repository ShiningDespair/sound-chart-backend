namespace Backend.DTOs
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for employee sales data.
    /// </summary>
    public class EmployeeSalesDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the employee.
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// Gets or sets the full name of the employee, combining first and last names.
        /// </summary>
        public string? FullName { get; set; }
        /// <summary>
        /// Gets or sets the total sales amount for the employee, representing the sum of all sales made by the employee.
        /// </summary>
        public decimal TotalSold { get; set; }
    }

}
