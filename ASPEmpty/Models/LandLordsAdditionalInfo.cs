using System;
using System.Collections.Generic;

namespace ASPEmpty.Models;

public partial class LandLordsAdditionalInfo
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public string Telephone { get; set; } = null!;

    public string PassportId { get; set; } = null!;

    public decimal AvgMark { get; set; }

    public int? Llid { get; set; }

    public virtual LandLord? Ll { get; set; }
}
