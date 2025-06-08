using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Country
{
    public int CountryId { get; set; }

    public string? CountryName { get; set; }

    public int? CountryIsoCode { get; set; }

    public string? CountryCode { get; set; }
}
