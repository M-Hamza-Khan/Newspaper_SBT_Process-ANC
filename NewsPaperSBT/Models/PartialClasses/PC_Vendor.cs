using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsPaperSBT.Models.PartialClasses
{
    public class PC_Vendor{

public int Vendorid { get; set; }
public  string  Contactname { get; set; }
public  string  CaptchaResponse { get; set; }
        public  string  VendorName { get; set; }
        public string Address { get; set; }
public string Email { get; set; }
public string City { get; set; }
public string Street { get; set; }
public string upccode { get; set; }
        public string State { get; set; }
public string ZipCode { get; set; }
public string DocumentSigned { get; set; }
        public string Phone { get; set; }
public DateTime ContractDate { get; set; }
public  Boolean IsActive { get; set; }
public string Approvedby { get; set; }
public  DateTime CreatedDate { get; set; }
public DateTime ModifiedDate { get; set; }
public DateTime DeletedDate { get; set; }
 public int UserId { get; set; }


    }
}