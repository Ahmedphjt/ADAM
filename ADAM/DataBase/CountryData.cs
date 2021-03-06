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
    
    public partial class CountryData
    {
        public CountryData()
        {
            this.CityDatas = new HashSet<CityData>();
            this.ClientDatas = new HashSet<ClientData>();
            this.EmployeeDatas = new HashSet<EmployeeData>();
            this.PointOfSales = new HashSet<PointOfSale>();
            this.SupplierDatas = new HashSet<SupplierData>();
        }
    
        public long Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<CityData> CityDatas { get; set; }
        public virtual ICollection<ClientData> ClientDatas { get; set; }
        public virtual ICollection<EmployeeData> EmployeeDatas { get; set; }
        public virtual ICollection<PointOfSale> PointOfSales { get; set; }
        public virtual ICollection<SupplierData> SupplierDatas { get; set; }
    }
}
