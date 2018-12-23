using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CIS_420_WebApplication.Interfaces;
namespace CIS_420_WebApplication.Models
{
    public class DonationPackage:IIdentifier<DonationPackage>
    {
        public string PackageName { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        [Key]
        public int UId { get; set; }
        public List<DonationPackage> AsList { get; set; }
    }
}
