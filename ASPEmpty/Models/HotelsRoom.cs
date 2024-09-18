using System;
using System.Collections.Generic;

namespace ASPEmpty.Models;

public partial class HotelsRoom
{
    public int Rid { get; set; }

    public string Header { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal AvgMark { get; set; }

    public decimal CostPerDay { get; set; }

    public int? Hid { get; set; }

    public virtual Hotel? HidNavigation { get; set; }

    public virtual ICollection<RoomsContract> RoomsContracts { get; set; } = new List<RoomsContract>();
}
