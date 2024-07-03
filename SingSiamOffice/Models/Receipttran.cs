using System;
using System.Collections.Generic;

namespace SingSiamOffice.Models;

public partial class Receipttran
{
    public int Id { get; set; }

    public int PromiseId { get; set; }

    public int BranchId { get; set; }

    public int CustomerId { get; set; }

    public int? Taxpromise { get; set; }

    public int? Specialtaxpromise { get; set; }

    public int? Ptype { get; set; }

    public string? Receiptno { get; set; }

    public string? Receiptdesc { get; set; }

    public string? Tdate { get; set; }

    public string? Tdateformat { get; set; }

    public string? Tdatecal { get; set; }

    public string? Tdatecalformat { get; set; }

    public decimal? Amount { get; set; }

    public decimal? Deposit { get; set; }

    public decimal? Charge1amt { get; set; }

    public decimal? Charge2amt { get; set; }

    public decimal? Arbalance { get; set; }

    public int? Arperiod { get; set; }

    public decimal? Cappaid { get; set; }

    public decimal? Intpaid { get; set; }

    public decimal? Arremain { get; set; }

    public decimal? Capremain { get; set; }

    public decimal? Intremain { get; set; }

    public decimal? Closefee { get; set; }

    public decimal? Intdiscamt { get; set; }

    public int? Periodremain { get; set; }

    public decimal? Intplus { get; set; }

    public decimal? Discount { get; set; }

    public decimal? Netamount { get; set; }

    public decimal? Resultamount { get; set; }

    public string? Usercode { get; set; }

    public string? Clientno { get; set; }

    public int? Cashpaid { get; set; }

    public int? Transferpaid { get; set; }

    public string? Transferdate { get; set; }

    public int? Otherpaid { get; set; }

    public string? Clientbranch { get; set; }

    public decimal? Loanplus { get; set; }

    public decimal? Loanminus { get; set; }

    public decimal? Oldint { get; set; }

    public decimal? Newint { get; set; }

    public int? Periodchg { get; set; }

    public decimal? Srvpaid { get; set; }

    public decimal? Inspaid { get; set; }

    public int? Currentperiod { get; set; }

    public string? Closecase { get; set; }

    public string? UploadImg { get; set; }

    public decimal? RemainingPrincipal { get; set; }

    public int? PaidBy { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual Promise Promise { get; set; } = null!;

    public virtual ICollection<Receiptdesc> Receiptdescs { get; set; } = new List<Receiptdesc>();
}
