using System;
using System.Collections.Generic;

namespace flight.Models;

public partial class SanchitBooking
{
    public int Bookingid { get; set; }

    public int? Flightid { get; set; }

    public int? Cid { get; set; }

    public DateTime? BookingDate { get; set; }

    public int? NoPassengers { get; set; }

    public int? TotalPrice { get; set; }

    public string? Source { get; set; }

    public string? Destination { get; set; }
}
