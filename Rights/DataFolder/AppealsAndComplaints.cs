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
    
    public partial class AppealsAndComplaints
    {
        public int IdAppeal { get; set; }
        public Nullable<int> IdStaff { get; set; }
        public string Date { get; set; }
        public string Discription { get; set; }
        public Nullable<int> IdStatus { get; set; }
    
        public virtual Staff Staff { get; set; }
        public virtual Status Status { get; set; }
    }
}