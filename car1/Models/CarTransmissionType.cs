using System;
using System.Collections.Generic;

namespace car1.Models;

public partial class CarTransmissionType
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
