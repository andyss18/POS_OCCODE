using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS_OCCODE.Models;
using System.Threading.Tasks;
using System.Linq;

namespace POS_OCCODE.Controllers
{
    public class SucursalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SucursalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sucursales = await _context.Sucursales.ToListAsync();
            return View(sucursales);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSucursal,NombreSucursal,Direccion,Telefono,IdEncargado")] Sucursale sucursal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sucursal);
                await _context.SaveChangesAsync();
                TempData["MensajeExito"] = "Sucursal registrada exitosamente.";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IdSucursal,NombreSucursal,Direccion,Telefono,IdEncargado")] Sucursale sucursal)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sucursal);
                    await _context.SaveChangesAsync();
                    TempData["MensajeExito"] = "Datos de la sucursal actualizados.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SucursaleExists(sucursal.IdSucursal)) return NotFound();
                    else throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int idSucursal)
        {
            var sucursal = await _context.Sucursales.FindAsync(idSucursal);
            if (sucursal != null)
            {
                _context.Sucursales.Remove(sucursal);
                await _context.SaveChangesAsync();
                TempData["MensajeExito"] = "Sucursal eliminada correctamente.";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SucursaleExists(int id)
        {
            return _context.Sucursales.Any(e => e.IdSucursal == id);
        }
    }
}