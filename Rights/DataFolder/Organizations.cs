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
    
    public partial class Organizations
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Organizations()
        {
            this.Bonus = new HashSet<Bonus>();
        }
    
        public int IdOrganization { get; set; }
        public string NameOrganization { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public int IdStatus { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bonus> Bonus { get; set; }
        public virtual Status Status { get; set; }
    }
}
