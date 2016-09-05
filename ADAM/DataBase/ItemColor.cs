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
    
    public partial class ItemColor
    {
        public ItemColor()
        {
            this.DeliveryDataDetails = new HashSet<DeliveryDataDetail>();
            this.DierctSellDetails = new HashSet<DierctSellDetail>();
            this.ExchangeRequestDetailsDatas = new HashSet<ExchangeRequestDetailsData>();
            this.ItemColorSelecteds = new HashSet<ItemColorSelected>();
            this.ItemContentDetails = new HashSet<ItemContentDetail>();
            this.ItemMovements = new HashSet<ItemMovement>();
            this.PurchaseOredrDetails = new HashSet<PurchaseOredrDetail>();
            this.RefluxDetailsDatas = new HashSet<RefluxDetailsData>();
        }
    
        public int Id { get; set; }
        public int Code { get; set; }
        public string ColorName { get; set; }
    
        public virtual ICollection<DeliveryDataDetail> DeliveryDataDetails { get; set; }
        public virtual ICollection<DierctSellDetail> DierctSellDetails { get; set; }
        public virtual ICollection<ExchangeRequestDetailsData> ExchangeRequestDetailsDatas { get; set; }
        public virtual ICollection<ItemColorSelected> ItemColorSelecteds { get; set; }
        public virtual ICollection<ItemContentDetail> ItemContentDetails { get; set; }
        public virtual ICollection<ItemMovement> ItemMovements { get; set; }
        public virtual ICollection<PurchaseOredrDetail> PurchaseOredrDetails { get; set; }
        public virtual ICollection<RefluxDetailsData> RefluxDetailsDatas { get; set; }
    }
}