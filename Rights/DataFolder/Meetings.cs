//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Rights.DataFolder
{
    using System;
    using System.Collections.Generic;
    
    public partial class Meetings
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Meetings()
        {
            this.Solutions = new HashSet<Solutions>();
        }
    
        public int IdMeetings { get; set; }
        public System.DateTime MeetingsDate { get; set; }
        public string Discription { get; set; }
        public int IdCommittee { get; set; }
    
        public virtual Committee Committee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Solutions> Solutions { get; set; }
    }
}
