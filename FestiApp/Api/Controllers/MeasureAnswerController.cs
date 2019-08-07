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
    public class MeasureAnswerController : ControllerBase
    {
        private readonly ApiContext _apiContext;

        public MeasureAnswerController(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        [HttpGet("{id}")]
        public async Task<MeasureQuestionAnswer> Get([FromRoute]string id)
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _apiContext.Inspectors.FirstOrDefault(elem => elem.UserAccount.UserName == currentUserName);
            return
                await _apiContext.MeasureQuestionAnswers.FirstOrDefaultAsync(elem => elem.Inspector.Id == user.Id && elem.Question.Id == id);
        }

        [HttpPost("{id}")]
        public async Task<MeasureQuestionAnswer> Create([FromRoute]string id, [FromBody]MeasureQuestionAnswer answer)
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _apiContext.Inspectors.FirstOrDefault(elem => elem.UserAccount.UserName == currentUserName);
            answer.Id = Guid.NewGuid().ToString("N");
            answer.Inspector = user;
            answer.Question = await _apiContext.MeasureQuestions.FindAsync(id);
            _apiContext.MeasureQuestionAnswers.Add(answer);
            await _apiContext.SaveChangesAsync();
            return answer;
        }

        [HttpPut("{id}")]
        public async Task<MeasureQuestionAnswer> Edit([FromRoute]string id, [FromBody]MeasureQuestionAnswer answerposted)
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _apiContext.Inspectors.FirstOrDefault(elem => elem.UserAccount.UserName == currentUserName);
            var answer = await _apiContext.MeasureQuestionAnswers.FindAsync(answerposted.Id);
            answer.Value = answerposted.Value;
            answer.Inspector = user;
            answer.Question = await _apiContext.MeasureQuestions.FindAsync(id);
            await _apiContext.SaveChangesAsync();
            return answerposted;
        }
    }
}