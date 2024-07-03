using System;
using System.Collections.Generic;

namespace SingSiamOffice.Models;

public partial class BlackList
{
    public int BlackId { get; set; }

    public int CustomerId { get; set; }

    public string Detial { get; set; } = null!;

    public DateTime CreateTime { get; set; }

    public int BranchId { get; set; }

    public string CNatid { get; set; } = null!;

    public virtual Branch Branch { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;
}
