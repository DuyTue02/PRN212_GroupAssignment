using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Station
{
    public int StationId { get; set; }

    public string StationName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
