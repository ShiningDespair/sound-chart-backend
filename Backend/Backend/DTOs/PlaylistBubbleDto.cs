namespace Backend.DTOs
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for playlist bubbles in a D3.js Bubble Chart.
    /// </summary>
    public class PlaylistBubbleDto
    {
        /// <summary>
        /// Gets or sets the name of the playlist.
        /// </summary>
        public required string Name { get; set; }
        /// <summary>
        /// Represents the total sales amount for the playlist.
        /// </summary>
        public decimal Sale { get; set; }

    }
}
