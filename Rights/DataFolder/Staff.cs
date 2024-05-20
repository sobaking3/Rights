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
    
    public partial class Staff
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Staff()
        {
            this.AppealsAndComplaints = new HashSet<AppealsAndComplaints>();
            this.Committee = new HashSet<Committee>();
            this.CommitteePayment = new HashSet<CommitteePayment>();
            this.Department = new HashSet<Department>();
        }
    
        public int IdStaff { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Number { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public int IdGender { get; set; }
        public int IdUser { get; set; }
        public System.DateTime WorkStartDate { get; set; }
        public Nullable<int> IdDepartment { get; set; }
        public Nullable<int> IdCommittee { get; set; }
        public byte[] PhotoStaff { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppealsAndComplaints> AppealsAndComplaints { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Committee> Committee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommitteePayment> CommitteePayment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Department> Department { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual User User { get; set; }
    }
}
