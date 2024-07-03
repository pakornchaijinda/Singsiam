using System;
using System.Collections.Generic;

namespace SingSiamOffice.Models;

public partial class Role
{
    public int Id { get; set; }

    public string? RoleName { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Login> Logins { get; set; } = new List<Login>();
}
