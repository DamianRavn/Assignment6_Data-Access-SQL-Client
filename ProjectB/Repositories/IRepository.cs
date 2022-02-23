using ProjectB.Models;
using System.Collections.Generic;

namespace ProjectB.Repositories
{
    public interface IRepository<T>
    {
        public T GetById(int id);

        public List<T> GetAll();

        public bool Add(T entity);

        public bool Update(T entity);

        public bool Delete(T entity);

    }
}
