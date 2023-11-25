using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task_Manager.Core.Interfaces;
using Task_Manager.Data;

namespace Task_Manager.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        private readonly DatabaseContext _context;

        private readonly ILogger _logger;
        public GenericRepository(DatabaseContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            _dbSet = _context.Set<T>();
        }

        public bool Add(T entityDTO)
        {
            _dbSet.Add(entityDTO);
            return true;
        }

        public bool Delete(T entityDTO)
        {
            _dbSet.Remove(entityDTO);
            return true;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public bool Update(T entityDTO)
        {
            _dbSet.Update(entityDTO);
            return true;
        }
    }
}