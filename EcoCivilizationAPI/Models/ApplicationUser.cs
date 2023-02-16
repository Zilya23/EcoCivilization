using System;
using System.Collections.Generic;

namespace EcoCivilizationAPI.Models;

public partial class ApplicationUser
{
    public int Id { get; set; }

    public int? IdApplication { get; set; }

    public int? IdUser { get; set; }

    public DateTime? Date { get; set; }

    public virtual Application? IdApplicationNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
