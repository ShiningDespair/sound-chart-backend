namespace Backend.Models;

/// <summary>
/// Represents a media type in the Chinook database.
/// </summary>
public partial class MediaType
{
    /// <summary>
    /// Gets or sets the unique identifier for the media type.
    /// </summary>
    public int MediaTypeId { get; set; }

    /// <summary>
    /// Gets or sets the name of the media type.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the collection of tracks associated with this media type.
    /// </summary>
    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
