namespace Backend.Models;

/// <summary>
/// Represents a playlist in the Chinook database.
/// </summary>
public partial class Playlist
{
    /// <summary>
    /// Gets or sets the unique identifier for the playlist.
    /// </summary>
    public int PlaylistId { get; set; }

    /// <summary>
    /// Gets or sets the name of the playlist.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the collection of tracks associated with the playlist.
    /// </summary>
    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
