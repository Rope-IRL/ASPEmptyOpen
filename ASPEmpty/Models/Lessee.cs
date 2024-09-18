using System;
using System.Collections.Generic;

namespace ASPEmpty.Models;

public partial class Lessee
{
    public int Lid { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<FlatsContract> FlatsContracts { get; set; } = new List<FlatsContract>();

    public virtual ICollection<HousesContract> HousesContracts { get; set; } = new List<HousesContract>();

    public virtual ICollection<LesseesAdditionalInfo> LesseesAdditionalInfos { get; set; } = new List<LesseesAdditionalInfo>();

    public virtual ICollection<RoomsContract> RoomsContracts { get; set; } = new List<RoomsContract>();
}
