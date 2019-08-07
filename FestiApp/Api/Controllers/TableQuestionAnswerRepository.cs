using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FestiAPI.Persistence;
using FestiDB.Domain;
using FestiDB.Domain.Table;

namespace FestiAPI.Controllers
{
    internal class TableQuestionAnswerRepository
    {
        private readonly ApiContext _apiContext;

        public TableQuestionAnswerRepository(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public async Task Add(TableQuestionAnswer answer, string id, Inspector user)
        {
            answer.Id = Guid.NewGuid().ToString();
            answer.Inspector = user;
            answer.Question = await _apiContext.TableQuestions.FindAsync(id);
            answer.AnswerEntries = PrepareAnswerEntries(answer.AnswerEntries);
            _apiContext.TableQuestionsAnswers.Add(answer);
            await _apiContext.SaveChangesAsync();
        }

        private static List<TableQuestionAnswerEntry> PrepareAnswerEntries(IEnumerable<TableQuestionAnswerEntry> answerEntries)
        {
            var tableQuestionAnswerEntries = answerEntries.ToList();
            foreach (var tableQuestionAnswerEntry in tableQuestionAnswerEntries)
            {
                tableQuestionAnswerEntry.Id = Guid.NewGuid().ToString();
                tableQuestionAnswerEntry.Value.Id = Guid.NewGuid().ToString();
                tableQuestionAnswerEntry.Key.Id = Guid.NewGuid().ToString();

                if (tableQuestionAnswerEntry.Value is TableQuestionAnswerMultipleValue val)
                {
                    val.AnswerValueId = val.AnswerValue.Id;
                    val.AnswerValue = null;
                }
            }

            return tableQuestionAnswerEntries.ToList();
        }

        public async Task Update(TableQuestionAnswer answerposted, string id, Inspector user)
        {
            var answer = await _apiContext.TableQuestionsAnswers.FindAsync(answerposted.Id);
            answer.Inspector = user;
            answer.Question = await _apiContext.TableQuestions.FindAsync(id);
            _apiContext.TableQuestionAnswerEntries.RemoveRange(answer.AnswerEntries);
            answer.AnswerEntries = PrepareAnswerEntries(answerposted.AnswerEntries); ;
            await _apiContext.SaveChangesAsync();
        }
    }
}