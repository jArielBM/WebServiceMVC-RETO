using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServiceMVC_RETO.Models;

namespace WebServiceMVC_RETO.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CervezasController : ControllerBase
	{
        private readonly AppDbContext _context;

        public CervezasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cerveza>>> GetCervezas()
        {
            return await _context.Cervezas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cerveza>> GetCerveza(int id)
        {
            var cerveza = await _context.Cervezas.FindAsync(id);

            if (cerveza == null)
            {
                return NotFound();
            }

            return cerveza;
        }

        [HttpPost]
        public async Task<ActionResult<Cerveza>> PostCerveza(Cerveza cerveza)
        {
            _context.Cervezas.Add(cerveza);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCerveza", new { id = cerveza.Id }, cerveza);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCerveza(int id, Cerveza cerveza)
        {
            if (id != cerveza.Id)
            {
                return BadRequest();
            }

            _context.Entry(cerveza).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCerveza(int id)
        {
            var cerveza = await _context.Cervezas.FindAsync(id);
            if (cerveza == null)
            {
                return NotFound();
            }

            _context.Cervezas.Remove(cerveza);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
