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
    public class ScaleController : ApiController
    {
        private readonly MobileServiceContext _context;

        public ScaleController(MobileServiceContext context)
        {
            _context = context;
        }

        public async Task<IHttpActionResult> GetAnswers(string Id)
        {
            var question = await _context.ScaleQuestions.Where(elem => elem.Id == Id).Include(elem => elem.Answers).FirstOrDefaultAsync();


            var options = new List<int>(Enumerable.Range(question.Min, (question.Max - question.Min) + 1));
            var dict = new Dictionary<int, int>();
            var min = question.Min;
            var max = question.Max;
            foreach (var opt in options)
            {
                dict.Add(opt, question.Answers.Count(ans =>
                 {
                     var Answer = ans as ScaleQuestionAnswer;
                     return Answer.Value == opt;
                 }));
            }

            return Json(new ScaleAnswerViewModel()
            {
                Max = max,
                Min = min,
                Question = question.Description,
                Options = dict
            });

        }
    }
}
