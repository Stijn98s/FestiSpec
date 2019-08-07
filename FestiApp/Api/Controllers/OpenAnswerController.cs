using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FestiAPI.Persistence;
using FestiDB.Domain;
using FestiDB.Domain.Answers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FestiAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OpenAnswerController : ControllerBase
    {
        private readonly ApiContext _apiContext;

        public OpenAnswerController(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        [HttpGet("{id}")]
        public async Task<OpenQuestionAnswer> Get([FromRoute]string id)
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _apiContext.Inspectors.FirstOrDefault(elem => elem.UserAccount.UserName == currentUserName);
            return
                await _apiContext.OpenQuestionAnswers.FirstOrDefaultAsync(elem => elem.Inspector.Id == user.Id && elem.Question.Id == id);
        }

        [HttpPost("{id}")]
        public async Task<OpenQuestionAnswer> Create([FromRoute]string id, [FromBody]OpenQuestionAnswer answer)
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _apiContext.Inspectors.FirstOrDefault(elem => elem.UserAccount.UserName == currentUserName);
            answer.Id = Guid.NewGuid().ToString("N");
            answer.Inspector = user;
            answer.Question = await _apiContext.OpenQuestions.FindAsync(id);
            _apiContext.OpenQuestionAnswers.Add(answer);
            await _apiContext.SaveChangesAsync();
            return answer;
        }

        [HttpPut("{id}")]
        public async Task<OpenQuestionAnswer> Edit([FromRoute]string id, [FromBody]OpenQuestionAnswer answerposted)
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _apiContext.Inspectors.FirstOrDefault(elem => elem.UserAccount.UserName == currentUserName);
            var answer = await _apiContext.OpenQuestionAnswers.FindAsync(answerposted.Id);
            answer.Answer = answerposted.Answer;
            answer.Inspector = user;
            answer.Question = await _apiContext.OpenQuestions.FindAsync(id);
            await _apiContext.SaveChangesAsync();
            return answerposted;
        }
    }
}