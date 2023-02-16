using System;
using System.Collections.Generic;

namespace EcoCivilizationAPI.Models;

public partial class Application
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? Date { get; set; }

    public int? CountUser { get; set; }

    public string? Place { get; set; }

    public string? Description { get; set; }

    public int? IdCity { get; set; }

    public int? Iduser { get; set; }

    public TimeSpan? TimeStart { get; set; }

    public virtual ICollection<ApplicationUser> ApplicationUsers { get; } = new List<ApplicationUser>();

    public virtual City? IdCityNavigation { get; set; }

    public virtual User? IduserNavigation { get; set; }

    public virtual ICollection<PhotoApplication> PhotoApplications { get; } = new List<PhotoApplication>();
}
