using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FestiDB.Persistence
{
    public abstract class Repository<T> : IRepository<T> where T : AbstractEntity
    {
        protected Repository(DbContext context)
        {
            Context = context;
        }

        protected DbContext Context { get; set; }

        public void Add(T elem)
        {
            Context.Set<T>().Add(elem);
        }

        public T Get(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public void Remove(T elem)
        {
            Context.Set<T>().Remove(elem);
        }
    }
}