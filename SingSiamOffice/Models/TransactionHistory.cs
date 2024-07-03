using System;
using System.Collections.Generic;

namespace SingSiamOffice.Models;

public partial class TransactionHistory
{
    public int TransactionId { get; set; }

    public int Price { get; set; }

    public int PaymentMethod { get; set; }

    public int BranchId { get; set; }

    public string? Detial { get; set; }

    public int LoginId { get; set; }

    public DateTime CreateAt { get; set; }

    public int SubjectId { get; set; }

    public string TransectionRef { get; set; } = null!;

    public string? SlipUrl { get; set; }

    public string? TransectionRemark { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual Login Login { get; set; } = null!;

    public virtual SubjectCost Subject { get; set; } = null!;
}
