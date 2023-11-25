using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Manager.Models.DTO
{
    public class CategoryDTO
    {
        [Required]
        [MaxLength(25)]
        [MinLength(5)]
        public string? Name { get; set; }
        
        [Required]
        [MaxLength(25)]
        [MinLength(3)]
        public string? ColorCode { get; set; }
    }
}