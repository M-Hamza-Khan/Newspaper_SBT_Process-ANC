
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace NewsPaperSBT.Models.DAL
{

using System;
    using System.Collections.Generic;
    
public partial class UPC
{

    public int UPCID { get; set; }

    public string UPCNumber { get; set; }

    public string ChainName { get; set; }

    public string BannerName { get; set; }

    public string StoreNumber { get; set; }

    public Nullable<double> RetailPrice { get; set; }

    public string City { get; set; }

    public string DayOfWeek { get; set; }

    public Nullable<int> VendorID { get; set; }

    public Nullable<System.DateTime> CreatedDate { get; set; }

    public Nullable<System.DateTime> ModifiedDate { get; set; }

    public Nullable<System.DateTime> DeletedDate { get; set; }

}

}
