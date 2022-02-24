using ProjectB.Models;
using System.Collections.Generic;

namespace ProjectB.Repositories
{
    /// <summary>
    /// A general CRUD Interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Use Id to get something from somewhere
        /// </summary>
        /// <param name="id">id of the entity your looking for</param>
        /// <returns>corresponding entity object</returns>
        public T GetById(int id);

        /// <summary>
        /// Get all entities from table
        /// </summary>
        /// <returns>all entities in object</returns>
        public List<T> GetAll();

        /// <summary>
        /// Insert into table using object
        /// </summary>
        /// <param name="entity">the entity to be inserted</param>
        /// <returns>success</returns>
        public bool Add(T entity);

        /// <summary>
        /// Update entity in table
        /// </summary>
        /// <param name="entity">the updated entity</param>
        /// <returns>success</returns>
        public bool Update(T entity);

        /// <summary>
        /// delete from table using object
        /// </summary>
        /// <param name="entity">the entity to be deleted</param>
        /// <returns>success</returns>
        public bool Delete(T entity);

    }
}
