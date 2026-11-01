using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POS_OCCODE.Models;
using System.Threading.Tasks;
using System.Linq;

namespace POS_OCCODE.Controllers
{
    public class ExistenciasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExistenciasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Incluimos las navegaciones para poder mostrar los nombres en lugar de solo los IDs
            var existencias = await _context.ExistenciasBodegas
                .Include(e => e.IdProductoNavigation)
                .Include(e => e.IdSucursalNavigation)
                .ToListAsync();

            // Cargamos los datos para los select/dropdowns del modal de Crear
            ViewBag.IdProducto = new SelectList(_context.Productos, "IdProducto", "Nombre");
            ViewBag.IdSucursal = new SelectList(_context.Sucursales, "IdSucursal", "NombreSucursal");

            return View(existencias);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSucursal,IdProducto,Existencias,Minimo,Maximo")] ExistenciasBodega existencia)
        {
            if (ModelState.IsValid)
            {
                // Verificar si ya existe este producto en esta sucursal para evitar error de llave duplicada
                var existeRegistro = await _context.ExistenciasBodegas
                    .AnyAsync(e => e.IdSucursal == existencia.IdSucursal && e.IdProducto == existencia.IdProducto);

                if (existeRegistro)
                {
                    TempData["MensajeError"] = "El producto ya tiene un registro de existencias en esta sucursal.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Add(existencia);
                await _context.SaveChangesAsync();
                TempData["MensajeExito"] = "Existencias registradas exitosamente.";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IdSucursal,IdProducto,Existencias,Minimo,Maximo")] ExistenciasBodega existencia)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(existencia);
                    await _context.SaveChangesAsync();
                    TempData["MensajeExito"] = "Inventario actualizado correctamente.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExistenciaExists(existencia.IdSucursal, existencia.IdProducto)) return NotFound();
                    else throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int idSucursal, int idProducto)
        {
            var existencia = await _context.ExistenciasBodegas
                .FirstOrDefaultAsync(e => e.IdSucursal == idSucursal && e.IdProducto == idProducto);

            if (existencia != null)
            {
                _context.ExistenciasBodegas.Remove(existencia);
                await _context.SaveChangesAsync();
                TempData["MensajeExito"] = "Registro de inventario eliminado.";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ExistenciaExists(int idSucursal, int idProducto)
        {
            return _context.ExistenciasBodegas.Any(e => e.IdSucursal == idSucursal && e.IdProducto == idProducto);
        }
    }
}