using FestiApp.Util.ViewModels.Answers;
using FestiDB.Domain.Answers;
using FestiMS.Models;
using Microsoft.Azure.Mobile.Server.Config;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace FestiMS.Controllers.Reports
{
    [MobileAppController]
    public class PictureController : ApiController
    {
        private readonly MobileServiceContext _context;

        public PictureController(MobileServiceContext context)
        {
            _context = context;
        }

        public async Task<IHttpActionResult> GetAnswers(string id)
        {
            var question = await _context.PictureQuestions.Where(elem => elem.Id == id).Include(elem => elem.Answers).FirstOrDefaultAsync();

            if (question == null) return NotFound();
            var answers = question.Answers.Cast<PictureQuestionAnswer>().Select(elem => elem.ImageUrl).ToList();
            return Json(new PictureAnswerViewModel()
            {
                Question = question.Description,
                UrlList = answers
            });
        }
    }
}
