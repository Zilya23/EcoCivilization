﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cora.DataBase
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EcoCivilizationEntities1 : DbContext
    {
        public EcoCivilizationEntities1()
            : base("name=EcoCivilizationEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<Application_User> Application_User { get; set; }
        public virtual DbSet<Chatt> Chatt { get; set; }
        public virtual DbSet<ChatUser> ChatUser { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<Messagee> Messagee { get; set; }
        public virtual DbSet<PhotoApplication> PhotoApplication { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}
