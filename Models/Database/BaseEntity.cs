using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Manager.Models;

public class BaseEntity
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }
}
