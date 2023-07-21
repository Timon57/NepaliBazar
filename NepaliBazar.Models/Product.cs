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
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        public double Weight { get; set; }
        [ValidateNever]

        public string ImageUrl { get; set; }
        [ValidateNever]

        public string? ImageUrl2 { get; set; }
        [ValidateNever]

        public string? ImageUrl3 { get; set; }
        [Required]
        [Range(1,10000)]
        public double Price { get; set; }

        [Range(1, 100)]
        public double DiscountedPrice { get; set; }

        [Range(1,5)]
        public double? Rating { get; set; }

        public string? Brand  { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

        public double actualPrice { get; set; }

    }
}
