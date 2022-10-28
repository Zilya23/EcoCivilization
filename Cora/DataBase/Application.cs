//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Application
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Application()
        {
            this.Application_User = new HashSet<Application_User>();
            this.PhotoApplication = new HashSet<PhotoApplication>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> Count_User { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }
        public Nullable<int> ID_City { get; set; }
        public Nullable<int> IDUser { get; set; }
        public Nullable<System.TimeSpan> TimeStart { get; set; }
    
        public virtual City City { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Application_User> Application_User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhotoApplication> PhotoApplication { get; set; }
    }
}
