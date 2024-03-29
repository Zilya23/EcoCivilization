﻿using System;
using System.Collections.Generic;

namespace EcoCivilizationAPI.Models;

public partial class Gender
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
