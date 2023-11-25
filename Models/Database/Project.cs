using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Task_Manager.Models;
public class Project : BaseEntity
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime StartDate { get; set; }

    public bool Completed { get; set; }

    public DateTime EndDate { get; set; }

    public int UserId { get; set; }
    public IdentityUser? User { get; set; }
}
