using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FestiAPI.Persistence;
using FestiDB.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FestiAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ApiContext _apiContext;

        public EventController(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        // GET: api/Event
        [HttpGet]
        public async Task<ICollection<Event>> GetAll()
        {
            var currentUserName =  User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _apiContext.QuestionnaireInspectors.Where(el => el.Inspector.UserAccount.UserName == currentUserName).Select(el => el.Questionnaire.Event).ToListAsync();
            return user.ToList();
        }

        // GET: api/Event/{id}
        [HttpGet("{id}")]
        public async Task<Event> Get([FromRoute]string id)
        {
            return await _apiContext.Events.FirstOrDefaultAsync(elem => elem.Id == id);
        }

        // GET: api/Event/{id}/Questionnaires
        [HttpGet("{id}/Questionnaires")]
        public ICollection<Questionnaire> GetQuestionnaires([FromRoute]string id)
        {
            return _apiContext.Events.Where(elem => elem.Id == id).SelectMany(elem => elem.Questionnaires).ToList();
        }
    }
}