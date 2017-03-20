using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class UserAccount
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Username required." )]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password" , ErrorMessage = "Confirm Password required.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "First Name required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name required.")]
        public string LastName { get; set; }

        
    }
}