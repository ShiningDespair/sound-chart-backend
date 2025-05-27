using System;
using System.Collections.Generic;

namespace Backend.Models;

/// <summary>
/// Represents an album in the Chinook database.
/// </summary>
public partial class Album
{
    /// <summary>
    /// Gets or sets the unique identifier for the album.
    /// </summary>
    public int AlbumId { get; set; }
    /// <summary>
    /// Gets or sets the title of the album.
    /// </summary>

    public string Title { get; set; } = null!;
    /// <summary>
    /// Gets or sets the unique identifier for the artist associated with the album.
    /// </summary>

    public int ArtistId { get; set; }

    /// <summary>
    /// Gets or sets the artist associated with the album.
    /// </summary>
    public virtual Artist Artist { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of tracks associated with the album.
    /// </summary>
    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
