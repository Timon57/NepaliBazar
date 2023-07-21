using Microsoft.AspNetCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NepaliBazar.Models
{
    public class UserModel
    {
        [Required]
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password and Confirm Password must match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public string Role { get; set; }
    }
}
