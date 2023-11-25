using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Manager.Models;
using Task = Task_Manager.Models.Task;

namespace Task_Manager.Core.Interfaces;
public interface ITaskRepositories : IGenericRepository<Task>
{

}
