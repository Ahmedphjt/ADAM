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
    
    public partial class ExchangeRequestHeaderData
    {
        public ExchangeRequestHeaderData()
        {
            this.ExchangeRequestDetailsDatas = new HashSet<ExchangeRequestDetailsData>();
        }
    
        public long Id { get; set; }
        public long ExchangeRequestNo { get; set; }
        public System.DateTime ExchangeRequestDate { get; set; }
        public long EmpId { get; set; }
        public long DivisionId { get; set; }
        public System.DateTime RecoredDate { get; set; }
        public int OrderType { get; set; }
        public long ClientId { get; set; }
        public int Posted { get; set; }
    
        public virtual ICollection<ExchangeRequestDetailsData> ExchangeRequestDetailsDatas { get; set; }
    }
}
