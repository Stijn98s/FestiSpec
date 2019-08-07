using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FestiAPI.Persistence;
using FestiDB.Domain;
using FestiDB.Domain.Answers;
using Microsoft.AspNetCore.Mvc;

namespace FestiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MultipleChoiceAnswerController : ControllerBase
    {
        private readonly ApiContext _apiContext;

        public MultipleChoiceAnswerController(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        [HttpGet("{id}")]
        public async Task<MultipleChoiceQuestionAnswer> Get([FromRoute]string id)
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _apiContext.Inspectors.FirstOrDefault(elem => elem.UserAccount.UserName == currentUserName);
            return
                await _apiContext.MultipleChoiceQuestionAnswers.Include(elem => elem.ChosenOption).FirstOrDefaultAsync(elem => elem.Inspector.Id == user.Id && elem.Question.Id == id);
        }

        [HttpPost("{id}")]
        public async Task<MultipleChoiceQuestionAnswer> Create([FromRoute]string id, [FromBody]MultipleChoiceQuestionAnswer answer)
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _apiContext.Inspectors.FirstOrDefault(elem => elem.UserAccount.UserName == currentUserName);
            answer.Id = Guid.NewGuid().ToString("N");
            answer.Inspector = user;
            answer.Question = await _apiContext.MultipleChoiceQuestions.FindAsync(id);
            var option = answer.ChosenOption.Id;
            answer.ChosenOption = null;
            _apiContext.MultipleChoiceQuestionAnswers.Add(answer);
            answer.ChosenOption = await _apiContext.MultipleChoiceQuestionOptions.FindAsync(option);
            await _apiContext.SaveChangesAsync();
            return answer;
        }

        [HttpPut("{id}")]
        public async Task<MultipleChoiceQuestionAnswer> Edit([FromRoute]string id, [FromBody]MultipleChoiceQuestionAnswer answerposted)
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _apiContext.Inspectors.FirstOrDefault(elem => elem.UserAccount.UserName == currentUserName);
            var answer = await _apiContext.MultipleChoiceQuestionAnswers.FindAsync(answerposted.Id);
            answer.ChosenOption = await _apiContext.MultipleChoiceQuestionOptions.FindAsync(answerposted.ChosenOption.Id);
            answer.Inspector = user;
            answer.Question = await _apiContext.MultipleChoiceQuestions.FindAsync(id);
            await _apiContext.SaveChangesAsync();
            return answerposted;
        }
    }
}