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
    public class ScaleAnswerController : ControllerBase
    {
        private readonly ApiContext _apiContext;

        public ScaleAnswerController(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        [HttpGet("{id}")]
        public async Task<ScaleQuestionAnswer> Get([FromRoute]string id)
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _apiContext.Inspectors.FirstOrDefault(elem => elem.UserAccount.UserName == currentUserName);
            return
                await _apiContext.ScaleQuestionAnswers.FirstOrDefaultAsync(elem => elem.Inspector.Id == user.Id && elem.Question.Id == id);
        }

        [HttpPost("{id}")]
        public async Task<ScaleQuestionAnswer> Create([FromRoute]string id, [FromBody]ScaleQuestionAnswer answer)
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _apiContext.Inspectors.FirstOrDefault(elem => elem.UserAccount.UserName == currentUserName);
            answer.Id = Guid.NewGuid().ToString("N");
            answer.Inspector = user;
            answer.Question = await _apiContext.ScaleQuestions.FindAsync(id);
            _apiContext.ScaleQuestionAnswers.Add(answer);
            await _apiContext.SaveChangesAsync();
            return answer;
        }

        [HttpPut("{id}")]
        public async Task<ScaleQuestionAnswer> Edit([FromRoute]string id, [FromBody]ScaleQuestionAnswer answerposted)
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _apiContext.Inspectors.FirstOrDefault(elem => elem.UserAccount.UserName == currentUserName);
            var answer = await _apiContext.ScaleQuestionAnswers.FindAsync(answerposted.Id);
            answer.Value = answerposted.Value;
            answer.Inspector = user;
            answer.Question = await _apiContext.ScaleQuestions.FindAsync(id);
            await _apiContext.SaveChangesAsync();
            return answerposted;
        }
    }
}