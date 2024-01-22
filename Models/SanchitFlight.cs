using System;
using System.Collections.Generic;

namespace flight.Models;

public partial class SanchitFlight
{
    public int Flightid { get; set; }

    public string? Flightname { get; set; }

    public string? Source { get; set; }

    public string? Destination { get; set; }

    public int? Price { get; set; }
}
