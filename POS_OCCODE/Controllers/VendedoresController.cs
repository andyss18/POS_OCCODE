using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS_OCCODE.Models;
using System.Threading.Tasks;
using System.Linq;

namespace POS_OCCODE.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendedoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vendedores = await _context.Vendedores.ToListAsync();
            return View(vendedores);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVendedor,NombreVendedor,Direccion,Telefono")] Vendedore vendedor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendedor);
                await _context.SaveChangesAsync();
                TempData["MensajeExito"] = "Vendedor registrado exitosamente.";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IdVendedor,NombreVendedor,Direccion,Telefono")] Vendedore vendedor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendedor);
                    await _context.SaveChangesAsync();
                    TempData["MensajeExito"] = "Datos del vendedor actualizados.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendedoreExists(vendedor.IdVendedor)) return NotFound();
                    else throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int idVendedor)
        {
            var vendedor = await _context.Vendedores.FindAsync(idVendedor);
            if (vendedor != null)
            {
                _context.Vendedores.Remove(vendedor);
                await _context.SaveChangesAsync();
                TempData["MensajeExito"] = "Vendedor eliminado correctamente.";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool VendedoreExists(int id)
        {
            return _context.Vendedores.Any(e => e.IdVendedor == id);
        }
    }
}