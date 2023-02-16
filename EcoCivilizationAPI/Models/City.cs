using System;
using System.Collections.Generic;

namespace EcoCivilizationAPI.Models;

public partial class City
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Application> Applications { get; } = new List<Application>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
