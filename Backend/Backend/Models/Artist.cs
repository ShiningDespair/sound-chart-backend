using System;
using System.Collections.Generic;

namespace Backend.Models;

/// <summary>
/// Represents an artist in the Chinook database.
/// </summary>
public partial class Artist
{
    /// <summary>
    /// Gets or sets the unique identifier for the artist.
    /// </summary>
    public int ArtistId { get; set; }

    /// <summary>
    /// Gets or sets the name of the artist.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the collection of albums associated with the artist.
    /// </summary>
    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
}
