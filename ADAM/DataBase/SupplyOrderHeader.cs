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
    
    public partial class SupplyOrderHeader
    {
        public SupplyOrderHeader()
        {
            this.SupplyOrderDetails = new HashSet<SupplyOrderDetail>();
        }
    
        public long Id { get; set; }
        public long SupplyOrderNo { get; set; }
        public System.DateTime SupplyOrderDate { get; set; }
        public long SupplierId { get; set; }
        public System.DateTime RecDate { get; set; }
        public int Posted { get; set; }
        public long CostCenter { get; set; }
    
        public virtual SupplierData SupplierData { get; set; }
        public virtual ICollection<SupplyOrderDetail> SupplyOrderDetails { get; set; }
    }
}