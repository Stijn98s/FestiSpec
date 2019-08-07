using System.Collections;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FestiAPI.Persistence;
using FestiDB.Domain.Table;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FestiAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TableAnswerController : ControllerBase
    {
        private readonly ApiContext _apiContext;
        private readonly TableQuestionAnswerRepository _answerRepository;
        public TableAnswerController(ApiContext apiContext)
        {
            _apiContext = apiContext;
            _answerRepository = new TableQuestionAnswerRepository(_apiContext);
        }

        [HttpGet("{id}")]
        public async Task<TableQuestionAnswer> Get([FromRoute]string id)
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _apiContext.Inspectors.FirstOrDefault(elem => elem.UserAccount.UserName == currentUserName);
            return
                await _apiContext.TableQuestionsAnswers.FirstOrDefaultAsync(elem => elem.Inspector.Id == user.Id && elem.Question.Id == id);
        }

        [HttpPost("{id}")]
        public async Task<TableQuestionAnswer> Create([FromRoute]string id, [FromBody]TableQuestionAnswer answer)
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _apiContext.Inspectors.FirstOrDefault(elem => elem.UserAccount.UserName == currentUserName);
            await _answerRepository.Add(answer, id, user);
            return answer;
        }

        [HttpPut("{id}")]
        public async Task<TableQuestionAnswer> Edit([FromRoute]string id, [FromBody]TableQuestionAnswer answerposted)
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _apiContext.Inspectors.FirstOrDefault(elem => elem.UserAccount.UserName == currentUserName);
            await _answerRepository.Update(answerposted, id, user);
            return answerposted;
        }
    }
}