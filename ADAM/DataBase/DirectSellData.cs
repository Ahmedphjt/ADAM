//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ADAM.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class DirectSellData
    {
        public DirectSellData()
        {
            this.DierctSellDetails = new HashSet<DierctSellDetail>();
        }
    
        public long Id { get; set; }
        public long DirectSellNo { get; set; }
        public System.DateTime DirectSellDate { get; set; }
        public long EmpId { get; set; }
        public long ItemType { get; set; }
    
        public virtual ICollection<DierctSellDetail> DierctSellDetails { get; set; }
        public virtual EmployeeData EmployeeData { get; set; }
        public virtual ItemType ItemType1 { get; set; }
    }
}
