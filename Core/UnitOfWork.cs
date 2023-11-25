using System;
using System.Collections.Generic;
using System.Linq;
using Task_Manager.Core.Interfaces;
using Task_Manager.Core.Repositories;
using Task_Manager.Data;
using Task_Manager.Models;
using TaskFunc = System.Threading.Tasks.Task;

namespace Task_Manager.Core
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DatabaseContext _context;

        public ICategoryRepository Categories { get; private set; }

        public IProjectRepository Projects { get; private set; }

        public ITaskRepositories Tasks { get; private set; }

        public UnitOfWork(DatabaseContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            var _logger = loggerFactory.CreateLogger("logs");
            Tasks = new TaskRepository(_context, _logger);
            Projects = new ProjectRepositories(_context, _logger);
            Categories = new CategoryRepository(_context, _logger);
        }


        public async TaskFunc CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}