using System;
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
using FestiDB.Domain.Table;
using FestiMS.Models;
using Microsoft.Azure.Mobile.Server.Config;
using Newtonsoft.Json.Linq;

namespace FestiMS.Controllers.Reports
{
    [MobileAppController]
    public class TableController : ApiController
    {
        private readonly MobileServiceContext _context;

        public TableController(MobileServiceContext context)
        {
            _context = context;
        }

        public async Task<IHttpActionResult> GetAnswers(string id)
        {
            var question = await _context.TableQuestions.Where(el => el.Id == id).Include(elem => elem.Answers).Include(elem => elem.KeyColumn).Include(elem => elem.ValueColumn).FirstOrDefaultAsync();
            if (question == null)
            {
                return NotFound();
            }


            var answerIds = question.Answers.Select(el => el.Id);
            var answers = await _context.TableQuestionsAnswers.Where(elem => answerIds.Contains(elem.Id))
                .Include(elem => elem.AnswerEntries.Select(elem2 => elem2.Value))
                .Include(elem => elem.AnswerEntries.Select(elem2 => elem2.Key)).ToListAsync();
            var res = answers.SelectMany(el =>
            {
                return el.AnswerEntries.Select(el2 =>
                    new KeyValuePair<object, object>(el2.Key.GetValue(), el2.Value.GetValue()));
            }).ToList();
            var tableGraphViewModel = new TableAnswerViewModel
            {
                Key = question.KeyColumn.Header,
                Value = question.ValueColumn.Header,
                Entries = res,
                Question = question.Description
            };

            return Json(tableGraphViewModel);
        }
    }


}
