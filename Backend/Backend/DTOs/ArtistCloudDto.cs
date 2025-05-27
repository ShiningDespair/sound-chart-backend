namespace Backend.DTOs
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for artist cloud visualization.
    /// </summary>
    public class ArtistCloudDto
    {
        /// <summary>
        /// Represents the name of the artist.
        /// </summary>
        public required string Name { get; set; }
        /// <summary>
        /// Represents the total sales amount for the artist.
        /// </summary>
        public decimal Sale { get; set; }
    }
}
