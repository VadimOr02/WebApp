//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Repository_DBFirst
{
    using System;
    using System.Collections.Generic;
    
    public partial class Fotografii
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fotografii()
        {
            this.Edits = new HashSet<Edits>();
        }
    
        public int id { get; set; }
        public int utilizator_id { get; set; }
        public string nume_fisier { get; set; }
        public string cale { get; set; }
        public System.DateTime data_incarcare { get; set; }
        public int dimensiune { get; set; }
        public string format { get; set; }
        public bool IsDeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Edits> Edits { get; set; }
        public virtual Utilizatori Utilizatori { get; set; }
    }
}
