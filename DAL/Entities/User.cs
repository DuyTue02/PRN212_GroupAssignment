using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Rental> RentalCustomers { get; set; } = new List<Rental>();

    public virtual ICollection<Rental> RentalStaffs { get; set; } = new List<Rental>();
}
