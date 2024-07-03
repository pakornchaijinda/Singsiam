using System;
using System.Collections.Generic;

namespace SingSiamOffice.Models;

public partial class Collateral
{
    public string? Code { get; set; }

    public string? Name { get; set; }

    public decimal? Financerate { get; set; }

    public decimal? Financemax { get; set; }

    public decimal? Loanrate { get; set; }

    public decimal? Loanmax { get; set; }

    public decimal? Cancelfee { get; set; }

    public int Id { get; set; }

    public string? Refcode { get; set; }

    public virtual ICollection<Promise> Promises { get; set; } = new List<Promise>();
}
