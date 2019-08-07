using System.Collections.Generic;

namespace FestiDB.Persistence
{
    public interface IRepository<T> where T : AbstractEntity
    {
        T Get(int id);

        IEnumerable<T> GetAll();

        void Add(T elem);

        void Remove(T elem);
    }
}