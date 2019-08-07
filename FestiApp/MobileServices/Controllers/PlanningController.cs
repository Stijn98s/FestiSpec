using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using FestiApp.Util.Util;
using FestiAppViewModels;
using FestiDB.Domain;
using FestiMS.Models;
using Microsoft.Azure.Mobile.Server.Config;

namespace FestiMS.Controllers
{
    [MobileAppController]
    public class PlanningController : ApiController
    {
        private readonly MobileServiceContext _context;
        private readonly GeodanHelperService _routeClient;

        public PlanningController(MobileServiceContext context, GeodanHelperService routeClient)
        {
            _context = context;
            _routeClient = routeClient;
        }

        [ActionName("plan")]
        [HttpPost]
        public async Task Post(InspectorPlanningPoco inspectorPlanReq)
        {
            var questionnaireInspector =
                await _context.QuestionnaireInspectors.FindAsync(inspectorPlanReq.QuestionnaireId,
                    inspectorPlanReq.InspectorId);
            if (questionnaireInspector != null)
            {
                throw new ObjectNotFoundException("Entered id(s) do not match any known record or the inspector has already been assigned");
            }
            _context.QuestionnaireInspectors.Add(new QuestionnaireInspector()
                {InspectorId = inspectorPlanReq.InspectorId, QuestionnaireId = inspectorPlanReq.QuestionnaireId});

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        [ActionName("unplan")]
        [HttpPost]
        public async Task Unplan(InspectorPlanningPoco inspectorPlanReq)
        {
            var questionnaireInspector =
                await _context.QuestionnaireInspectors.FindAsync(inspectorPlanReq.QuestionnaireId,
                    inspectorPlanReq.InspectorId);

            if (questionnaireInspector == null)
            {
                throw new ObjectNotFoundException(
                    "Entered id(s) do not match any known record or the inspector has not been planned for this questionnaire");
            }
            _context.QuestionnaireInspectors.Remove(questionnaireInspector);
            await _context.SaveChangesAsync();
        }

        [HttpGet]
        public async Task<IEnumerable<InspectorDistanceViewModel>> Get(string id)
        {
            var eventIn = await _context.Events.Where(events => events.Id == id)
                .Include(elem => elem.Availabilities.Select(elem2 => elem2.Inspector)).FirstOrDefaultAsync();
            if (eventIn == null) throw new ObjectNotFoundException();
            var inspectors = eventIn.Availabilities.Where(el => el.IsAvailable).Select(el => el.Inspector);
            var tasks = inspectors.Select(async el =>
            {
                DistanceViewModel distance = null;
                try
                {
                    distance = await _routeClient.GetDistanceBetweenTwoLocatioins(el.Locx.ToString(),
                        el.Locy.ToString(CultureInfo.InvariantCulture), eventIn.GeodanAdresX, eventIn.GeodanAdresY);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                var model = new InspectorDistanceViewModel
                {
                    Inspector = el,
                    Distance = distance
                };
                return model;
            });

            var inspectorsa = await Task.WhenAll(tasks.ToArray());
            return inspectorsa.Where(el => el.Inspector != null);
        }

        [HttpGet]
        public async Task<List<Event>> GetEvents()
        {
            var events = await _context.Events.ToListAsync();
            return events.Where(x => x.EndDate >= DateTime.Now.AddDays(-1)).OrderBy(el => el.EndDate).Take(4).ToList();
        }
        public class InspectorPlanningPoco
        {
            public string QuestionnaireId { get; set; }

            public string InspectorId { get; set; }
        }
    }
}