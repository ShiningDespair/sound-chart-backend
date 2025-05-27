namespace Backend.Controllers
{

    /// <summary>
    /// Data Transfer Object (DTO) for albums grouped by artists.
    /// </summary>
    public class AlbumsByArtistsDto
    {
        /// <summary>
        /// Gets or sets the title of the album.
        /// </summary>
        public required string Album { get; set; }
    }
}