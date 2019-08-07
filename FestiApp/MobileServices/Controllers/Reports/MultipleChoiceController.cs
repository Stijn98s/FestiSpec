using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using FestiApp.Util.ViewModels;
using FestiApp.Util.ViewModels.Answers;
using FestiDB.Domain;
using FestiDB.Domain.Answers;
using FestiMS.Models;
using Microsoft.Azure.Mobile.Server.Config;

namespace FestiMS.Controllers.Reports
{
    [MobileAppController]
    public class MultipleChoiceController : ApiController
    {
        private readonly MobileServiceContext _context;

        public MultipleChoiceController(MobileServiceContext context)
        {
            _context = context;
        }

        public async Task<IHttpActionResult> GetAnswers(string Id)
        {
            var question = await _context.MultipleChoiceQuestions.Where(elem => elem.Id == Id).Include(elem => elem.Options)
                .Include(elem => elem.Answers).FirstOrDefaultAsync();


            var options = question.Options;
            var dict = new Dictionary<string, int>();

            foreach (var opt in options)
            {
                dict.Add(opt.Value, question.Answers.Count(ans =>
                {
                    var Answer = ans as MultipleChoiceQuestionAnswer;
                    return Answer.ChosenOption.Id == opt.Id;
                }));
            }

            return Json(new MultipleChoiceAnswerViewModel()
            {
                Question = question.Description,
                Options = dict
            });
        }
    }
}
