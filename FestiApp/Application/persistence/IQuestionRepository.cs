using FestiApp.ViewModel.Questions;
using FestiDB.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FestiApp.persistence
{
    public interface IQuestionRepository
    {
        Task<List<QuestionViewModel>> GetAll(string id);
        Task InsertAsync<T>(T entity) where T : Question;
        Task Delete<T>(string id) where T : Question;
    }
}