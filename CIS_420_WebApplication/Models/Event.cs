using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CIS_420_WebApplication.Interfaces;
namespace CIS_420_WebApplication.Models
{
    public class Event:IIdentifier<Event>
    {
     
       public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm tt}")]
        public DateTime Time { get; set; }
       public string Description { get; set; }
        [Display(Name = "Button Title")]
        public string LinkTitle { get; set; }
       public string Link { get; set; }
       [Key]
       public int UId { get; set; }
       public List<Event> AsList { get; set; }
    }

}
