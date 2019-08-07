using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using FestiApp.Util.ViewModels.Answers;
using FestiDB.Domain;
using FestiDB.Domain.Answers;
using FestiMS.Models;
using Microsoft.Azure.Mobile.Server.Config;

namespace FestiMS.Controllers.Reports
{
    [MobileAppController]
    public class MeasureController : ApiController
    {
        private readonly MobileServiceContext _context;

        public MeasureController(MobileServiceContext context)
        {
            _context = context;
        }

        public async Task<IHttpActionResult> GetAnswers(string Id)
        {
            var question = await _context.MeasureQuestions.Where(elem => elem.Id == Id).Include(elem => elem.Answers).FirstOrDefaultAsync();

                var dict = new List<double>();

                var answers = question.Answers.Cast<MeasureQuestionAnswer>().Select(elem => elem.Value);
                if (answers.Count() != 0)
                {
                    dict.Add(answers.Min());
                    dict.Add(answers.Average());
                    dict.Add(answers.Max());
                }

                return Json(new MeasureAnswerViewModel()
                {
                    Question = question.Description,
                    Options = dict.ToList()
                });
        }
    }
}
