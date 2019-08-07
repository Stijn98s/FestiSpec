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
    public class AvailabilityController : ControllerBase
    {
        private readonly ApiContext _context;

        public AvailabilityController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Availability
        [HttpGet]
        public IEnumerable<Availability> Get()
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var today = DateTime.Today;
            var user =  _context.Inspectors.FirstOrDefault(elem => elem.UserAccount.UserName == currentUserName);
            var events = _context.Events.ToList().Where(model => model.StartDate.CompareTo(today) >= 0);
            var availabilities = _context.Availabilities.Where(elem => elem.Inspector.UserAccount.UserName == currentUserName && elem.Event.StartDate.CompareTo(today)>=0).Include(elem2 => elem2.Event).Include(elem3 => elem3.Inspector).ToList();

            return events.Select(inspectionEvent =>
            {
                var any = availabilities.FirstOrDefault(el => el.Event.Id == inspectionEvent.Id);
                if (any != null)
                {
                    return any;
                }
                return new Availability()
                {
                    Event = inspectionEvent,
                    Inspector = user
                };
            });
        }

        // GET: api/Availability/Create
        [HttpGet("Create")]
        public void Create()
        {
            _context.Events.AddRange(new List<Event>()
            {
                new Event()
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "Pinkpop",
                    Availabilities = new List<Availability>()
                    {
                        new Availability()
                        {
                            Id = Guid.NewGuid().ToString("N"),
                            Inspector = _context.Inspectors.First(elem => elem.FirstName == "Geordi")
                        }
                    }
                },
                new Event()
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "Paaspop",
                },
                new Event()
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "Hallo k3",
                }
            });

            _context.SaveChanges();
        }


        [HttpPost]
        public async Task<Availability> Create([FromBody]Availability availability)
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _context.Inspectors.FirstOrDefault(elem => elem.UserAccount.UserName == currentUserName);
            availability.Id = Guid.NewGuid().ToString("N");
            availability.Inspector = user;
            availability.Event = await _context.Events.FindAsync(availability.Event.Id);
            _context.Availabilities.Add(availability);
            await _context.SaveChangesAsync();
            return availability;
        }

        [HttpPut]
        public async Task<Availability> Edit([FromBody]Availability availabilityPosted)
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _context.Inspectors.FirstOrDefault(elem => elem.UserAccount.UserName == currentUserName);
            var availability = await _context.Availabilities.FindAsync(availabilityPosted.Id);
            availability.HasResponded = availabilityPosted.HasResponded;
            availability.IsAvailable = availabilityPosted.IsAvailable;
            availability.Inspector = user;
            availability.Event = await _context.Events.FindAsync(availabilityPosted.Event.Id);
            await _context.SaveChangesAsync();
            return availability;
        }
    }
}
