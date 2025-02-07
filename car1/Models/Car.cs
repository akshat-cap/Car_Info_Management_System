using System;
using System.Collections.Generic;

namespace car1.Models;

public partial class Car
{
    public int Id { get; set; }

    public string? ManufacturerName { get; set; }

    public string? Model { get; set; }

    public string? Type { get; set; }

    public string? Engine { get; set; }

    public int? Bhp { get; set; }

    public string? Transmission { get; set; }

    public decimal? Mileage { get; set; }

    public int? Seat { get; set; }

    public string? AirBagDetails { get; set; }

    public int? BootSpace { get; set; }

    public decimal? Price { get; set; }

    public int? CarTypeId { get; set; }

    public int? CarTransmissionTypeId { get; set; }

    public virtual CarTransmissionType? CarTransmissionType { get; set; }

    public virtual CarType? CarType { get; set; }
}
