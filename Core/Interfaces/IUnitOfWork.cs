using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Manager.Core.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        IProjectRepository Projects { get; }
        ITaskRepositories Tasks { get; }

        Task CompleteAsync();
    }
}