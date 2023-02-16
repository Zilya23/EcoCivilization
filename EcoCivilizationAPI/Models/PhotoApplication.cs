using System;
using System.Collections.Generic;

namespace EcoCivilizationAPI.Models;

public partial class PhotoApplication
{
    public int Id { get; set; }

    public byte[]? Photo { get; set; }

    public int? Idapplicatioon { get; set; }

    public virtual Application? IdapplicatioonNavigation { get; set; }
}
