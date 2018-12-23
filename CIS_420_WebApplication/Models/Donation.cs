using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CIS_420_WebApplication.Interfaces;
namespace CIS_420_WebApplication.Models
{
    public class Donation:IIdentifier<Donation>
    {     
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public ApplicationUser ApplicationUser {get;set;}
        public List<Donation> Donations { get; set; }
        [Key]
        public int UId { get; set; }
        public List<Donation> AsList { get; set; }

        public Donation()
        {
            Date = DateTime.Now;
        }
    }
}
