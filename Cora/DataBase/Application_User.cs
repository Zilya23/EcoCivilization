//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cora.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Application_User
    {
        public int ID { get; set; }
        public Nullable<int> ID_Application { get; set; }
        public Nullable<int> ID_User { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual Application Application { get; set; }
        public virtual User User { get; set; }
    }
}
