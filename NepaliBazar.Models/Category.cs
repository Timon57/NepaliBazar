using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NepaliBazar.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Range(1, 100, ErrorMessage = "Display Order must be between 1 and 100 only!!")]

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }
        public  DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set;} = DateTime.Now;
    }
}
