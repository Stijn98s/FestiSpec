using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

using FestiAPI.Persistence;
using FestiDB.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FestiAPI.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InspectorController : ControllerBase
    {
        private readonly ApiContext _context;

        public InspectorController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Inspector/me
        [HttpGet("me")]
        public async Task<Inspector> GetCurrentUser()
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await _context.Inspectors.FirstOrDefaultAsync(elem => elem.UserAccount.UserName == currentUserName);
        }

        // Put: api/Inspector/me
        [HttpPut("me")]
        public async Task<StatusCodeResult> Put([FromBody] Inspector value)
        {
            var inspector = await GetCurrentUser();
            if (inspector.Id != value.Id) return Unauthorized();
            inspector.HouseNumber = value.HouseNumber;
            inspector.BirthDate = value.BirthDate;
            inspector.FirstName = value.FirstName;
            inspector.LastName = value.LastName;
            inspector.PostalCode = value.PostalCode;
            inspector.Gender = value.Gender;
            inspector.Phone = value.Phone;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
