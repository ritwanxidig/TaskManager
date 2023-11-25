using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Manager.Models;
public class Task : BaseEntity
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime DueDate { get; set; }

    public string? Status { get; set; }

    public string? PriorityLevel { get; set; }

    public int? ProjectId { get; set; }
    public Project? Project { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

 
}
