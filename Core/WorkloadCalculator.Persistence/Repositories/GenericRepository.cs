using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkloadCalculator.Persistence.Interfaces;

namespace WorkloadCalculator.Persistence.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        private readonly DataContext _dataContext;
        public GenericRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Saves entity to database repository
        /// </summary>
        /// <typeparam name="T">Generic entity type</typeparam>
        /// <param name="data">entity content</param>
        /// <returns></returns>
        public async Task SaveAsync<T>(T data) where T : class
        {
            _dataContext.Set<T>().Add(data);
            await _dataContext.SaveChangesAsync();
        }

        /// <summary>
        /// Gets all data for specified entity from database
        /// </summary>
        /// <typeparam name="T">Generic entity type</typeparam>
        /// <returns>IEnumerable generic entity data</returns>
        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class
        {
            return await this.GetQueryable<T>().ToListAsync();
        }

        /// <summary>
        /// Gets filtered data for specified entity from database
        /// </summary>
        /// <typeparam name="T">Generic entity type</typeparam>
        /// <param name="match">Filter Expression</param>
        /// <returns>IEnumerable generic entity data</returns>
        public async Task<IEnumerable<T>> FindAllAsync<T>(Expression<Func<T, bool>> match) where T : class
        {
            return await this.GetQueryable<T>().Where(match).ToListAsync();
        }

        /// <summary>
        /// Gets Queryable for generic entity type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private IQueryable<T> GetQueryable<T>() where T : class
        {
            return this._dataContext.Set<T>().AsNoTracking();
        }
    }
}
