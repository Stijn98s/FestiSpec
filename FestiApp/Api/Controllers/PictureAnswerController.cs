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
    public class PictureAnswerController : ControllerBase
    {
        private readonly ApiContext _apiContext;

        public PictureAnswerController(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        [HttpGet("{id}")]
        public async Task<PictureQuestionAnswer> Get([FromRoute]string id)
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _apiContext.Inspectors.FirstOrDefault(elem => elem.UserAccount.UserName == currentUserName);
            return
                await _apiContext.PictureQuestionAnswers.FirstOrDefaultAsync(elem => elem.Inspector.Id == user.Id && elem.Question.Id == id);
        }

        [HttpPost("{id}")]
        public async Task<PictureQuestionAnswer> Create([FromRoute]string id, [FromBody]PictureQuestionAnswer answer)
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _apiContext.Inspectors.FirstOrDefault(elem => elem.UserAccount.UserName == currentUserName);
            answer.Id = Guid.NewGuid().ToString("N");
            answer.Inspector = user;
            answer.Question = await _apiContext.PictureQuestions.FindAsync(id);
            _apiContext.PictureQuestionAnswers.Add(answer);
            await _apiContext.SaveChangesAsync();
            return answer;
        }

        [HttpPut("{id}")]
        public async Task<PictureQuestionAnswer> Edit([FromRoute]string id, [FromBody]PictureQuestionAnswer answerposted)
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _apiContext.Inspectors.FirstOrDefault(elem => elem.UserAccount.UserName == currentUserName);
            var answer = await _apiContext.PictureQuestionAnswers.FindAsync(answerposted.Id);
            answer.ImageUrl = answerposted.ImageUrl;
            await _apiContext.SaveChangesAsync();
            return answerposted;
        }
    }
}