using System;
using System.Collections.Generic;

namespace SingSiamOffice.Models;

public partial class Receiptdesc
{
    public int Id { get; set; }

    public int PromiseId { get; set; }

    public int BranchId { get; set; }

    public int CustomerId { get; set; }

    public string? Receiptno { get; set; }

    public string? Tdate { get; set; }

    public string? Tdateformat { get; set; }

    public string? Tdatecal { get; set; }

    public string? Tdatecalformat { get; set; }

    public int? Period { get; set; }

    public string? Perioddate { get; set; }

    public decimal? Cappaid { get; set; }

    public decimal? Intpaid { get; set; }

    public decimal? Amount { get; set; }

    public string? Usercode { get; set; }

    public string? Clientno { get; set; }

    public string? Clientbranch { get; set; }

    public decimal? Loanplus { get; set; }

    public decimal? Loanminus { get; set; }

    public decimal? Oldint { get; set; }

    public decimal? Newint { get; set; }

    public int? Periodchg { get; set; }

    public decimal? Deposit { get; set; }

    public decimal? Chargeamt { get; set; }

    public decimal? Lateamt { get; set; }

    public decimal? Srvpaid { get; set; }

    public decimal? Inspaid { get; set; }

    public int ReceipttranId { get; set; }

    public int PeriodtranId { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual Periodtran Periodtran { get; set; } = null!;

    public virtual Promise Promise { get; set; } = null!;

    public virtual Receipttran Receipttran { get; set; } = null!;
}
