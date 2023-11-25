using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Manager.Models.DTO
{
    public class TaskDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string? Title { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(105)]
        public string? Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        [MaxLength(20)]
        public string? Status { get; set; }

        [Required]
        [MaxLength(20)]
        public string? PriorityLevel { get; set; }


        public int? ProjectId { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }

    public class ModifiedTaskDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string? Title { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(105)]
        public string? Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        [MaxLength(20)]
        public string? Status { get; set; }

        [Required]
        [MaxLength(20)]
        public string? PriorityLevel { get; set; }


        [Required]
        public int CategoryId { get; set; }
    }
}