using System;
using System.Collections.Generic;

namespace SingSiamOffice.Models;

public partial class Guarantor
{
    public int Id { get; set; }

    public int PromiseId { get; set; }

    public string? GuarantorName { get; set; }

    public string GuarantorNatId { get; set; } = null!;

    public string? GuarantorRelation { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public int? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Promise Promise { get; set; } = null!;
}
