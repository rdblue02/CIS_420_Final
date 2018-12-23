using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CIS_420_WebApplication.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace CIS_420_WebApplication.Models
{
    public class Horse:IIdentifier<Horse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<Horse> Horses { get; set; }
        [Key]
        public int UId { get; set; }
        public List<Horse> AsList { get; set; }
    }
}
