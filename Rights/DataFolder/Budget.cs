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
    
    public partial class Budget
    {
        public int IdBudget { get; set; }
        public Nullable<int> IdCommitte { get; set; }
        public int Year { get; set; }
        public int Amount { get; set; }
        public int UsageMoney { get; set; }
        public int UnUsageMoney { get; set; }
    
        public virtual Committee Committee { get; set; }
    }
}
