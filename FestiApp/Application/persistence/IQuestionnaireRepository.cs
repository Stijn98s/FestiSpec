using FestiDB.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FestiApp.persistence
{
    public interface IQuestionnaireRepository
    {
        Task InsertAsync<T>(T entity) where T : Inspector;
        Task RemoveAsync<T>(T entity) where T : Inspector;
        Task<List<Questionnaire>> GetAll(string eventModelId);
    }
}