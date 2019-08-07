using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using FestiApp.Util.ViewModels;
using FestiApp.Util.ViewModels.Answers;
using FestiDB.Domain;
using FestiDB.Domain.Answers;
using FestiMS.Models;
using Microsoft.Azure.Mobile.Server.Config;

namespace FestiMS.Controllers.Reports
{
    [MobileAppController]
    public class OpenController : ApiController
    {
        private readonly MobileServiceContext _context;

        public OpenController(MobileServiceContext context)
        {
            _context = context;
        }

        public async Task<IHttpActionResult> GetAnswers(string id)
        {
            var question = await _context.OpenQuestions.Where(elem => elem.Id == id).Include(elem => elem.Answers).FirstOrDefaultAsync();
            if(question == null) return NotFound();
            var answers = question.Answers.Cast<OpenQuestionAnswer>().Select(elem => elem.Answer).ToList();
            return Json(new OpenAnswerViewModel()
            {
                Question = question.Description,
                AnswerList = answers
            });
        }
    }
}
