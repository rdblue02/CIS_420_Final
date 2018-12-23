using CIS_420_WebApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIS_420_WebApplication.Models
{
    public class BoardMember:IIdentifier<BoardMember>
    {
       
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }

        [Key]
        public int UId { get; set; }
        public List<BoardMember> AsList { get; set; }
    }
    
   

}
