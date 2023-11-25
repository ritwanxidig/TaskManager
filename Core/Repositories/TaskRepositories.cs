using Task_Manager.Core.Interfaces;
using Task_Manager.Core.Repositories;
using Task_Manager.Data;
using Task_Manager.Models;
using Task = Task_Manager.Models.Task;

namespace Task_Manager.Core.Repositories
{
    public class TaskRepository : GenericRepository<Task>, ITaskRepositories
    {
        public TaskRepository(DatabaseContext context, ILogger logger) : base(context, logger)
        {

        }
    }
}