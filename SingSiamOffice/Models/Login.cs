using System;
using System.Collections.Generic;

namespace SingSiamOffice.Models;

public partial class Login
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public string? Email { get; set; }

    public int RoleId { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Phone { get; set; }

    public DateTime? Dob { get; set; }

    public int BranchId { get; set; }

    public string? Img { get; set; }

    public string? Address { get; set; }

    public string? Code { get; set; }

    public string? EmNickname { get; set; }

    public string? Position { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<TransactionHistory> TransactionHistories { get; set; } = new List<TransactionHistory>();
}
