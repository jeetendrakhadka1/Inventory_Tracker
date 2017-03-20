using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _383_Phase1_InventoryTracker.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public  string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        
        [Required]
        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "The password must be at least 4 characters long.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
