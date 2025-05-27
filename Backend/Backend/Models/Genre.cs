using System;
using System.Collections.Generic;

namespace Backend.Models;

/// <summary>
/// Represents a genre in the Chinook database.
/// </summary>
public partial class Genre
{
    /// <summary>
    /// Gets or sets the unique identifier for the genre.
    /// </summary>
    public int GenreId { get; set; }

    /// <summary>
    /// Gets or sets the name of the genre.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the collection of tracks associated with the genre.
    /// </summary>
    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
