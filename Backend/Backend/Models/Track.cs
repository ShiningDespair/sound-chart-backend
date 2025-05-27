using System;
using System.Collections.Generic;

namespace Backend.Models;

/// <summary>
/// Represents a track in the Chinook database.
/// </summary>
public partial class Track
{
    /// <summary>
    /// Gets or sets the unique identifier for the track.
    /// </summary>
    public int TrackId { get; set; }

    /// <summary>
    /// Gets or sets the name of the track.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the album ID associated with the track.
    /// </summary>
    public int? AlbumId { get; set; }

    /// <summary>
    /// Gets or sets the media type ID for the track.
    /// </summary>
    public int MediaTypeId { get; set; }

    /// <summary>
    /// Gets or sets the genre ID for the track.
    /// </summary>
    public int? GenreId { get; set; }

    /// <summary>
    /// Gets or sets the composer of the track.
    /// </summary>
    public string? Composer { get; set; }

    /// <summary>
    /// Gets or sets the duration of the track in milliseconds.
    /// </summary>
    public int Milliseconds { get; set; }

    /// <summary>
    /// Gets or sets the size of the track in bytes.
    /// </summary>
    public int? Bytes { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the track.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the album associated with the track.
    /// </summary>
    public virtual Album? Album { get; set; }

    /// <summary>
    /// Gets or sets the genre associated with the track.
    /// </summary>
    public virtual Genre? Genre { get; set; }

    /// <summary>
    /// Gets or sets the collection of invoice lines for the track.
    /// </summary>
    public virtual ICollection<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();

    /// <summary>
    /// Gets or sets the media type associated with the track.
    /// </summary>
    public virtual MediaType MediaType { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of playlists that include the track.
    /// </summary>
    public virtual ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
}
