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
    
    public partial class ExchangeRequestDetailsData
    {
        public ExchangeRequestDetailsData()
        {
            this.ExchangeRequestPricings = new HashSet<ExchangeRequestPricing>();
        }
    
        public long Id { get; set; }
        public long ExchangeRequestHeaderDataId { get; set; }
        public long ItemId { get; set; }
        public decimal Qty { get; set; }
        public decimal FreeQty { get; set; }
        public decimal Bounce { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public long IncommingOrderId { get; set; }
        public long LocationId { get; set; }
        public int ItemColorId { get; set; }
        public long MovementId { get; set; }
        public long ExchangeRequestOrder { get; set; }
        public int PriceTester { get; set; }
        public long IncommingOrderNo { get; set; }
        public long ProdctionLineId { get; set; }
        public long ItemTypeId { get; set; }
    
        public virtual ExchangeRequestHeaderData ExchangeRequestHeaderData { get; set; }
        public virtual ItemColor ItemColor { get; set; }
        public virtual Item Item { get; set; }
        public virtual ICollection<ExchangeRequestPricing> ExchangeRequestPricings { get; set; }
    }
}
