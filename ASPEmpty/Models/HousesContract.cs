using System;
using System.Collections.Generic;

namespace ASPEmpty.Models;

public partial class HousesContract
{
    public int Id { get; set; }

    public int? Lid { get; set; }

    public int? Llid { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public decimal Cost { get; set; }

    public int? Pid { get; set; }

    public virtual Lessee? LidNavigation { get; set; }

    public virtual LandLord? Ll { get; set; }

    public virtual House? PidNavigation { get; set; }
}
