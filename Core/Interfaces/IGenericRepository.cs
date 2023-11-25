using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Manager.Core.Interfaces;
public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();

    Task<T?> GetById(int id);

    bool Add(T entityDTO);
    bool Update(T entityDTO);
    bool Delete(T entityDTO);
}
