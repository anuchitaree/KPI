//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class Exclusion_ItemsTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Exclusion_ItemsTable()
        {
            this.Exclusion_RecordTable = new HashSet<Exclusion_RecordTable>();
        }
    
        public int run { get; set; }
        public string exclusionID { get; set; }
        public string exclusionName { get; set; }
        public Nullable<short> sort { get; set; }
        public string divisionID { get; set; }
        public string plantID { get; set; }
        public Nullable<System.DateTime> recordDateTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Exclusion_RecordTable> Exclusion_RecordTable { get; set; }
    }
}
