using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Task_Manager.Models.DTO
{
    public class UserDTO
    {
        [Required]
        [MaxLength(25)]
        public string? Name { get; set; }

        [EmailAddress]
        [Required]
        [MaxLength(25)]
        public string? Email { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(25)]
        public string? Password { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string? Profile { get; set; }
    }
}