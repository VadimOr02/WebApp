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
    
    public partial class Edits
    {
        public int id { get; set; }
        public int fotografie_id { get; set; }
        public int utilizator_id { get; set; }
        public string tip_editare { get; set; }
        public System.DateTime data_editare { get; set; }
        public bool salvat { get; set; }
    
        public virtual Fotografii Fotografii { get; set; }
        public virtual Utilizatori Utilizatori { get; set; }
    }
}
