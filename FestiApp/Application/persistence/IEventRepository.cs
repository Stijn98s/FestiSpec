using FestiDB.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FestiApp.persistence
{
    public interface IEventRepository
    {
        Task<List<Event>> GetUpcommingEvents();
    }
}