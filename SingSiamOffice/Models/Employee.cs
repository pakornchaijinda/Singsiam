using System;
using System.Collections.Generic;

namespace SingSiamOffice.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string EmCode { get; set; } = null!;

    public string? EmNicknane { get; set; }

    public string? EmAddress { get; set; }

    public string EmPicture { get; set; } = null!;

    public int LoginId { get; set; }
}
