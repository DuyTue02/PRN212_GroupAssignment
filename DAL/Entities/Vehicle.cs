using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Vehicle
{
    public int VehicleId { get; set; }

    public string LicensePlate { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int? CurrentStationId { get; set; }

    public virtual Station? CurrentStation { get; set; }

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}
