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
    
    public partial class ItemContentDetail
    {
        public long Id { get; set; }
        public long ItemContentHeaderId { get; set; }
        public long ItemTypeId { get; set; }
        public long ProductionLineId { get; set; }
        public long ItemId { get; set; }
        public int ItemColorId { get; set; }
        public decimal Qty { get; set; }
    
        public virtual ItemColor ItemColor { get; set; }
        public virtual ItemContentHeader ItemContentHeader { get; set; }
        public virtual Item Item { get; set; }
        public virtual ItemType ItemType { get; set; }
    }
}
