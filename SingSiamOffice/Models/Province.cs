using System;
using System.Collections.Generic;

namespace SingSiamOffice.Models;

public partial class Province
{
    public int Id { get; set; }

    public string ProvinceName { get; set; } = null!;

    public string ProvinceShortTh { get; set; } = null!;

    public string ProvinceShortEn { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual ICollection<Promise> Promises { get; set; } = new List<Promise>();
}
