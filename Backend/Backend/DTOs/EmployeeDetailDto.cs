namespace Backend.DTOs
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for employee details.
    /// </summary>
    public class EmployeeDetailDto
    {
        /// <summary>
        /// Gets or sets the first name of the employee.
        /// </summary>
        public required string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name of the employee.
        /// </summary>
        public required string LastName { get; set; }
        /// <summary>
        /// Gets or sets the title of the employee.
        /// </summary>
        public required string Title { get; set; }
        /// <summary>
        /// Gets or sets the country of the employee's location.
        /// </summary>
        public required string Country { get; set; }
        /// <summary>
        /// Gets or sets the city of the employee's location.
        /// </summary>
        public required string City { get; set; }
        /// <summary>
        /// Gets or sets the short text that describes the employee's role or responsibilities.
        /// </summary>
        public required string Description { get; set; }

    }
}
