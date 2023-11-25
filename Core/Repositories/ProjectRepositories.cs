using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Manager.Core.Interfaces;
using Task_Manager.Data;
using Task_Manager.Models;

namespace Task_Manager.Core.Repositories
{
    public class ProjectRepositories : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepositories(DatabaseContext context, ILogger logger) : base(context, logger)
        {

        }
    }
}