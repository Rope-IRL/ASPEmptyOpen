using System;
using System.Collections.Generic;

namespace ASPEmpty.Models;

public partial class Hotel
{
    public int Hid { get; set; }

    public string Header { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal AvgMark { get; set; }

    public string City { get; set; } = null!;

    public string Address { get; set; } = null!;

    public bool RestrauntAvailability { get; set; }

    public bool ElevatorAvailability { get; set; }

    public int? Llid { get; set; }

    public virtual ICollection<HotelsRoom> HotelsRooms { get; set; } = new List<HotelsRoom>();

    public virtual LandLord? Ll { get; set; }
}
