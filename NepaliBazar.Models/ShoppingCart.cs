using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NepaliBazar.Models
{
    public class ShoppingCart
    {
        
        public int Id { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product product { get; set; }
        
        [Range(1, 100, ErrorMessage = "Please enter a value between 1 and 100")]
        public int Count { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever] 
        public IdentityUser IdentityUser { get; set; }
        
    }
}
