using System;
using System.Collections.Generic;

namespace ASPEmpty.Models;

public partial class House
{
    public int Pid { get; set; }

    public string Header { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal AvgMark { get; set; }

    public string City { get; set; } = null!;

    public string Address { get; set; } = null!;

    public short NumberOfRooms { get; set; }

    public short NumberOfFloors { get; set; }

    public bool BathroomAvailability { get; set; }

    public bool WiFiAvailability { get; set; }

    public decimal CostPerDay { get; set; }

    public int? Llid { get; set; }

    public virtual ICollection<HousesContract> HousesContracts { get; set; } = new List<HousesContract>();

    public virtual LandLord? Ll { get; set; }
}
