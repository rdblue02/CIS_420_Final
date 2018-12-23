using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIS_420_WebApplication.Interfaces
{
    public interface IIdentifier<T>
    {
        [Key]
        int UId { get; set; }
        List<T> AsList { get; set; }
    }
}
