using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WorkloadCalculator.Persistence.Interfaces
{
    public interface IGenericRepository
    {
        Task SaveAsync<T>(T data) where T : class;
        Task<IEnumerable<T>> GetAllAsync<T>() where T : class;
        Task<IEnumerable<T>> FindAllAsync<T>(Expression<Func<T, bool>> match) where T : class;
    }
}
