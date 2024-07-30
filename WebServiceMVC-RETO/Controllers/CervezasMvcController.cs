using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServiceMVC_RETO.Models;

namespace WebServiceMVC_RETO.Controllers
{
	public class CervezasMvcController : Controller
	{
        private readonly AppDbContext _context;

        public CervezasMvcController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Cervezas.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price")] Cerveza cerveza)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cerveza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cerveza);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cerveza = await _context.Cervezas.FindAsync(id);
            if (cerveza == null)
            {
                return NotFound();
            }
            return View(cerveza);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price")] Cerveza cerveza)
        {
            if (id != cerveza.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cerveza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CervezaExists(cerveza.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cerveza);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cerveza = await _context.Cervezas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cerveza == null)
            {
                return NotFound();
            }

            return View(cerveza);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cerveza = await _context.Cervezas.FindAsync(id);
            _context.Cervezas.Remove(cerveza);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CervezaExists(int id)
        {
            return _context.Cervezas.Any(e => e.Id == id);
        }
    }
}
