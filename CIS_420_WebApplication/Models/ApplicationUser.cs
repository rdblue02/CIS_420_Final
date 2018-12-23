using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CIS_420_WebApplication.Models
{
    public class ApplicationUser : IdentityUser
    {
      
       
        public int ApplicationUserId { get; set; }
        public string StringId { get; set; }
        private static int _nextId = 1;
        [RegularExpression("[a-zA-Z]", ErrorMessage = "Please enter a First Name that only contains letters")]
        [StringLength(20, ErrorMessage = "Please enter a First Name that is at least 1 but less than 20 characters", MinimumLength = 1)]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [RegularExpression("[a-zA-Z]", ErrorMessage = "Please enter a First Name that only contains letters")]
        [StringLength(20, ErrorMessage = "Please enter a First Name that is at least 1 but less than 20 characters", MinimumLength = 1)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }      
 
        public string AccountCreationDate { get; set; }
        public Permissions Access { get; set; }

        public List<Donation>Donations { get; set; }
        
        [NotMapped]
        public List<SelectListItem> PermissionLevels { get; set; }
        
        public ApplicationUser()
        {
            ApplicationUserId = _nextId;
            StringId = ApplicationUserId.ToString();
            _nextId++;
            PermissionLevels = new List<SelectListItem>();

            foreach (var name in Enum.GetValues(typeof(Permissions)))
            {
                PermissionLevels.Add(new SelectListItem()
                {
                    Value = ((int)name).ToString(),
                    Text = name.ToString()
                });
            }
            Access = Permissions.Basic;
        }

        public enum Permissions
        {
            Basic,
            PartialAdmin,
            FullAdmin
        }

    }
}
