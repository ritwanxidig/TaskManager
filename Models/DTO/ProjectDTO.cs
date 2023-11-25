using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Task_Manager.Models.DTO;
public class ProjectDTO
{
    [Required]
    [MinLength(3)]
    [MaxLength(25)]
    public string? Name { get; set; }

    [Required]
    [MinLength(10)]
    [MaxLength(125)]
    public string? Description { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

}
