using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FestiAPI.Persistence;
using FestiDB.Domain;
using FestiDB.Domain.Table;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FestiAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionnaireController : ControllerBase
    {
        private readonly ApiContext _apiContext;

        public QuestionnaireController(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        // GET: api/Questionnaire
        [HttpGet]
        public async Task<ICollection<Questionnaire>> GetAllAsync()
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
             return await _apiContext.QuestionnaireInspectors
                .Where(el => el.Inspector.UserAccount.UserName == currentUserName).Select(el => el.Questionnaire)
                .Where(el => el.Event.EndDate >= DateTime.Today.Date).ToListAsync();

        }


        [HttpPut("{id}/description")]
        public async Task<IActionResult> ChangeComment([FromRoute]string id, [FromBody]string comment)
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var questionnaireInspector = await _apiContext.QuestionnaireInspectors.FirstOrDefaultAsync(el => el.Inspector.UserAccount.UserName == currentUserName && el.QuestionnaireId == id);
            if(questionnaireInspector == null) return NotFound();
            questionnaireInspector.Comment = comment;
            await _apiContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id}/description")]
        public async Task<string> GetComment([FromRoute] string id)
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var questInsp= await _apiContext.QuestionnaireInspectors.FirstOrDefaultAsync(el => el.Inspector.UserAccount.UserName == currentUserName && el.QuestionnaireId == id);
            return questInsp.Comment;
        }
 

        // GET: api/Questionnaire/{id}
        [HttpGet("{id}")]
        public async Task<Questionnaire> Get([FromRoute]string id)
        {
            return await _apiContext.Questionaires.Include(el => el.Questions).FirstOrDefaultAsync(elem => elem.Id == id);
        }

        [HttpGet("open")]
        public OpenQuestion OpenQuestion()
        {
            return null;
        }

        [HttpGet("multiple")]
        public MultipleChoiceQuestion MultipleChoiceQuestion()
        {
            return null;
        }

        [HttpGet("draw")]
        public DrawQuestion DrawQuestion()
        {
            return null;
        }

        [HttpGet("measure")]
        public MeasureQuestion MeasureQuestion()
        {
            return null;
        }

        [HttpGet("picture")]
        public PictureQuestion PictureQuestion()
        {
            return null;
        }

        [HttpGet("scale")]
        public ScaleQuestion ScaleQuestion()
        {
            return null;
        }

        [HttpGet("table")]
        public TableQuestion TableQuestion()
        {
            return null;
        }

        [HttpGet("category")]
        public CategorieQuestion CategoryQuestion()
        {
            return null;
        }
    }
}