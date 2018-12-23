using CIS_420_WebApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIS_420_WebApplication.Models
{
    public class AuctionItem:IIdentifier<AuctionItem>
    {
      public string Name { get; set; }
      public decimal SetPrice { get; set; }
      public int Quantity { get; set; } 
      public string Description { get; set; }
      public List<AuctionItem> Inventory { get; set; }
      [Key]
      public int UId { get; set; }
      public List<AuctionItem> AsList { get; set; }
    }
}
