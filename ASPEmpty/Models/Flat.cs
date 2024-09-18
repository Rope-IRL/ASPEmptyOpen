using System;
using System.Collections.Generic;

namespace ASPEmpty.Models;

public partial class Flat
{
    public int Fid { get; set; }

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

    public virtual ICollection<FlatsContract> FlatsContracts { get; set; } = new List<FlatsContract>();

    public virtual LandLord? Ll { get; set; }

    public override string ToString()
    {
        return $"Header is {Header} Description is {Description} AvgMark is {AvgMark} City is {City} Address is {Address} Number of roomms is {NumberOfRooms} Number of floors is " +
            $"{NumberOfFloors} Cost per day is {CostPerDay}\n";
    }
}
