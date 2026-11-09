using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reunionsystem2API.Data;
using ReunionsystemAPI.Models.Entities;

namespace Reunionsystem2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SalasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/salas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sala>>> GetSalas()
        {
            return await _context.Salas.ToListAsync();
        }
    }
}
