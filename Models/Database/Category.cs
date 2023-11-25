using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Manager.Models;
public class Category : BaseEntity
{
    public string? Name { get; set; }

    public string? ColorCode { get; set; }
}
