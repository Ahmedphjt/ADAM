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
    
    public partial class ItemPrice
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long ItemColorId { get; set; }
        public decimal MainClausePrice { get; set; }
        public decimal MainSalesPrice { get; set; }
        public decimal MainShowsPrice { get; set; }
        public decimal TesterClausePrice { get; set; }
        public decimal TesterSalesPrice { get; set; }
        public decimal TesterShowsPrice { get; set; }
    }
}