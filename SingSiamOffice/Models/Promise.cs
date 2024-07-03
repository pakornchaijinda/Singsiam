using System;
using System.Collections.Generic;

namespace SingSiamOffice.Models;

public partial class Promise
{
    public int Id { get; set; }

    public string? Promiseno { get; set; }

    public string? Refcode { get; set; }

    public DateTime? Tdatetime { get; set; }

    public string? Tdate { get; set; }

    public string? Tdateformat { get; set; }

    public string? Prddesc { get; set; }

    public string? Insurance1 { get; set; }

    public string? Insurance1relation { get; set; }

    public string? Insurance2 { get; set; }

    public string? Insurance2relation { get; set; }

    public int? Ptype { get; set; }

    public decimal? Chargeamt { get; set; }

    public int? Taxpromise { get; set; }

    public decimal? Totaldown { get; set; }

    public int? Specialtaxpromise { get; set; }

    public decimal? Downamount { get; set; }

    public decimal? Capital { get; set; }

    public decimal? Intrate { get; set; }

    public decimal? Service { get; set; }

    public decimal? Insurance { get; set; }

    public decimal? Latepc { get; set; }

    public int? Periods { get; set; }

    public decimal? Amount { get; set; }

    public decimal? Deposit { get; set; }

    public string? Daypaid { get; set; }

    public string? Firstdate { get; set; }

    public int? Status { get; set; }

    public string? Usercode { get; set; }

    public string? Clientno { get; set; }

    public string? Person1 { get; set; }

    public string? Person2 { get; set; }

    public string? Person3 { get; set; }

    public string? Person4 { get; set; }

    public string? Person5 { get; set; }

    public string? Datewarn { get; set; }

    public string? Datewarnformat { get; set; }

    public string? Warndesc { get; set; }

    public decimal? Nampa { get; set; }

    public string? Dateclose { get; set; }

    public string? Datecloseformat { get; set; }

    public string? Dateclosecal { get; set; }

    public string? Dateclosecalformat { get; set; }

    public string? Closedocno { get; set; }

    public string? Closecase { get; set; }

    public decimal? Closeresult { get; set; }

    public string? Colname1 { get; set; }

    public string? Colname2 { get; set; }

    public string? Colname3 { get; set; }

    public string? Colname4 { get; set; }

    public string? Colname5 { get; set; }

    public string? Colname6 { get; set; }

    public string? Colname7 { get; set; }

    public string? Colname8 { get; set; }

    public string? Colname9 { get; set; }

    public string? Coldata1 { get; set; }

    public string? Coldata2 { get; set; }

    public string? Coldata3 { get; set; }

    public string? Coldata4 { get; set; }

    public string? Coldata5 { get; set; }

    public string? Coldata6 { get; set; }

    public string? Coldata7 { get; set; }

    public string? Coldata8 { get; set; }

    public string? Coldata9 { get; set; }

    public int? Cancelno { get; set; }

    public string? Stockcode { get; set; }

    public int? Compromise { get; set; }

    public int? Sumstatus { get; set; }

    public decimal? Sumcharge1 { get; set; }

    public decimal? Sumcharge2 { get; set; }

    public int BranchId { get; set; }

    public int ProductId { get; set; }

    public int CustomerId { get; set; }

    public int? ProvinceId { get; set; }

    public int? ContractType { get; set; }

    public string? UploadImg { get; set; }

    public bool? Guarantor { get; set; }

    public string? JsonPrddesc { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Guarantor> Guarantors { get; set; } = new List<Guarantor>();

    public virtual ICollection<Periodtran> Periodtrans { get; set; } = new List<Periodtran>();

    public virtual Collateral Product { get; set; } = null!;

    public virtual Province? Province { get; set; }

    public virtual ICollection<ReceiptdescCancle> ReceiptdescCancles { get; set; } = new List<ReceiptdescCancle>();

    public virtual ICollection<Receiptdesc> Receiptdescs { get; set; } = new List<Receiptdesc>();

    public virtual ICollection<ReceipttranCancle> ReceipttranCancles { get; set; } = new List<ReceipttranCancle>();

    public virtual ICollection<Receipttran> Receipttrans { get; set; } = new List<Receipttran>();
}
