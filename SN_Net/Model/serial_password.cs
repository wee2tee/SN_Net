//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SN_Net.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class serial_password
    {
        public int id { get; set; }
        public string pass_word { get; set; }
        public Nullable<int> serial_id { get; set; }
        public Nullable<int> creby_id { get; set; }
        public System.DateTime credat { get; set; }
        public Nullable<int> chgby_id { get; set; }
        public Nullable<System.DateTime> chgdat { get; set; }
        public int flag { get; set; }
    
        public virtual serial serial { get; set; }
        public virtual users users { get; set; }
        public virtual users users1 { get; set; }
    }
}
