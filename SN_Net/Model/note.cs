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
    
    public partial class note
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public note()
        {
            this.note_comment = new HashSet<note_comment>();
        }
    
        public int id { get; set; }
        public Nullable<int> users_id { get; set; }
        public System.DateTime date { get; set; }
        public string users_name { get; set; }
        public string sernum { get; set; }
        public string contact { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string duration { get; set; }
        public string problem { get; set; }
        public string remark { get; set; }
        public string is_break { get; set; }
        public string reason { get; set; }
        public string file_path { get; set; }
        public string rec_by { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<note_comment> note_comment { get; set; }
    }
}