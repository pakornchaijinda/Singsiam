using System;
using System.Collections.Generic;

namespace SingSiamOffice.Models;

public partial class SubjectCost
{
    public int SubjectId { get; set; }

    public string SubjectName { get; set; } = null!;

    public string? SubjectDeital { get; set; }

    public string SubjectCode { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreateAt { get; set; }

    public int SubjectType { get; set; }

    public virtual ICollection<TransactionHistory> TransactionHistories { get; set; } = new List<TransactionHistory>();
}
