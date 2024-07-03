using System;
using System.Collections.Generic;

namespace SingSiamOffice.Models;

public partial class Branch
{
    public int Id { get; set; }

    public string BranchName { get; set; } = null!;

    public string? BranchCode { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public bool IsActive { get; set; }

    public string? NoBlank { get; set; }

    public string? Province { get; set; }

    public string? BranchMap { get; set; }

    public string? BlankType { get; set; }

    public DateTime? CreateAt { get; set; }

    public int? ProvinceId { get; set; }

    public virtual ICollection<BlackList> BlackLists { get; set; } = new List<BlackList>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Login> Logins { get; set; } = new List<Login>();

    public virtual ICollection<Periodtran> Periodtrans { get; set; } = new List<Periodtran>();

    public virtual ICollection<Promise> Promises { get; set; } = new List<Promise>();

    public virtual Province? ProvinceNavigation { get; set; }

    public virtual ICollection<ReceiptdescCancle> ReceiptdescCancles { get; set; } = new List<ReceiptdescCancle>();

    public virtual ICollection<Receiptdesc> Receiptdescs { get; set; } = new List<Receiptdesc>();

    public virtual ICollection<ReceipttranCancle> ReceipttranCancles { get; set; } = new List<ReceipttranCancle>();

    public virtual ICollection<Receipttran> Receipttrans { get; set; } = new List<Receipttran>();

    public virtual ICollection<TransactionHistory> TransactionHistories { get; set; } = new List<TransactionHistory>();
}
