using System;
using System.Collections.Generic;

namespace SingSiamOffice.Models;

public partial class Periodtran
{
    public int Id { get; set; }

    public int PromiseId { get; set; }

    public int? Taxpromise { get; set; }

    public int? Specialtaxpromise { get; set; }

    public int BranchId { get; set; }

    public int? Ptype { get; set; }

    public int CustomerId { get; set; }

    public int? Period { get; set; }

    public int? Periods { get; set; }

    public string? Tdate { get; set; }

    public string? Tdateformat { get; set; }

    public decimal? Capital { get; set; }

    public decimal? Interest { get; set; }

    public decimal? Service { get; set; }

    public decimal? Insurance { get; set; }

    public decimal? Amount { get; set; }

    public decimal? Cappaid { get; set; }

    public decimal? Intpaid { get; set; }

    public decimal? Paidamount { get; set; }

    public int? Status { get; set; }

    public string? Usercode { get; set; }

    public string? Clientno { get; set; }

    public decimal? Deposit { get; set; }

    public decimal? Loanplus { get; set; }

    public decimal? Loanminus { get; set; }

    public decimal? Srvpaid { get; set; }

    public decimal? Inspaid { get; set; }

    public bool? Ispaid { get; set; }

    public decimal? Paidremain { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual Promise Promise { get; set; } = null!;

    public virtual ICollection<Receiptdesc> Receiptdescs { get; set; } = new List<Receiptdesc>();
}
