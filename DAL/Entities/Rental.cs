using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Rental
{
    public int RentalId { get; set; }

    public int CustomerId { get; set; }

    public int VehicleId { get; set; }

    public int StaffId { get; set; }

    public int PickupStationId { get; set; }

    public DateTime RentalTime { get; set; }

    public DateTime? ReturnTime { get; set; }

    public decimal? TotalCost { get; set; }

    public virtual User Customer { get; set; } = null!;

    public virtual Station PickupStation { get; set; } = null!;

    public virtual User Staff { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}
