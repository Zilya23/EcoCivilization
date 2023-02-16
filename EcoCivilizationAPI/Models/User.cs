using System;
using System.Collections.Generic;

namespace EcoCivilizationAPI.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Telephone { get; set; }

    public int? IdCity { get; set; }

    public int? IdGender { get; set; }

    public int? IdRole { get; set; }

    public int? CountApplication { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public DateTime? DateRegist { get; set; }

    public virtual ICollection<ApplicationUser> ApplicationUsers { get; } = new List<ApplicationUser>();

    public virtual ICollection<Application> Applications { get; } = new List<Application>();

    public virtual City? IdCityNavigation { get; set; }

    public virtual Gender? IdGenderNavigation { get; set; }

    public virtual Role? IdRoleNavigation { get; set; }
}
