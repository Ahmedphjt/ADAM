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
    
    public partial class ItemMovement
    {
        public long Id { get; set; }
        public long MovmentnameId { get; set; }
        public long ItemId { get; set; }
        public long StoreId { get; set; }
        public long DocmentId { get; set; }
        public System.DateTime MovementDate { get; set; }
        public decimal MainQty { get; set; }
        public decimal AdditionalQty { get; set; }
        public System.DateTime RecDate { get; set; }
        public long LocatioId { get; set; }
        public long ItemUnitId { get; set; }
        public int ItemColorId { get; set; }
        public long SupplyOrderDetailsId { get; set; }
        public long IncommingOrderNo { get; set; }
        public long AuditDetailsId { get; set; }
        public decimal MainQtyOut { get; set; }
        public decimal AdditionalQtyOut { get; set; }
        public long ParentItemMoveMentId { get; set; }
    
        public virtual ItemColor ItemColor { get; set; }
        public virtual ItemLocation ItemLocation { get; set; }
        public virtual Item Item { get; set; }
        public virtual ItemUnit ItemUnit { get; set; }
        public virtual MovmentName MovmentName { get; set; }
        public virtual StoreData StoreData { get; set; }
        public virtual StoreData StoreData1 { get; set; }
    }
}
