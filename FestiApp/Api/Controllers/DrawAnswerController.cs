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
    public class DrawAnswerController : ControllerBase
    {
        private readonly ApiContext _apiContext;

        public DrawAnswerController(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        [HttpGet("{id}")]
        public async Task<DrawQuestionAnswer> Get([FromRoute]string id)
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _apiContext.Inspectors.FirstOrDefault(elem => elem.UserAccount.UserName == currentUserName);
            return
                await _apiContext.DrawQuestionAnswers.FirstOrDefaultAsync(elem => elem.Inspector.Id == user.Id && elem.Question.Id == id);
        }

        [HttpPost("{id}")]
        public async Task<DrawQuestionAnswer> Create([FromRoute]string id, [FromBody]DrawQuestionAnswer answer)
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _apiContext.Inspectors.FirstOrDefault(elem => elem.UserAccount.UserName == currentUserName);
            answer.Id = Guid.NewGuid().ToString("N");
            answer.Inspector = user;
            answer.Question = await _apiContext.DrawQuestions.FindAsync(id);
            _apiContext.DrawQuestionAnswers.Add(answer);
            await _apiContext.SaveChangesAsync();
            return answer;
        }

        [HttpPut("{id}")]
        public async Task<DrawQuestionAnswer> Edit([FromRoute]string id, [FromBody]DrawQuestionAnswer answerposted)
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _apiContext.Inspectors.FirstOrDefault(elem => elem.UserAccount.UserName == currentUserName);
            var answer = await _apiContext.DrawQuestionAnswers.FindAsync(answerposted.Id);
            answer.ImageUrl = answerposted.ImageUrl;
            await _apiContext.SaveChangesAsync();
            return answerposted;
        }
    }
}