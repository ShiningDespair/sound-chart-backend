namespace Backend.DTOs
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for comments in the Chinook database.
    /// </summary>
    public class CommentDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the comment.
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Gets or sets the content of the comment.
        /// </summary>
        public required string text { get; set; }
        /// <summary>
        /// Gets or sets the date and time when the comment was created.
        /// </summary>

    }
}
