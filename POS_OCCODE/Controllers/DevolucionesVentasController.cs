using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POS_OCCODE.Models;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace POS_OCCODE.Controllers
{
    public class DevolucionesVentasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DevolucionesVentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DevolucionesVentas
        public async Task<IActionResult> Index()
        {
            var devoluciones = await _context.DevolucionesVentasEncabezados
                .Include(d => d.IdClienteNavigation)
                .Include(d => d.IdFacturaNavigation)
                .OrderByDescending(d => d.Fecha)
                .ThenByDescending(d => d.IdDevolucion)
                .ToListAsync();

            return View(devoluciones);
        }

        // GET: DevolucionesVentas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var devolucion = await _context.DevolucionesVentasEncabezados
                .Include(d => d.IdClienteNavigation)
                .Include(d => d.IdFacturaNavigation)
                .Include(d => d.DevolucionesVentasDetalles)
                    .ThenInclude(det => det.IdDetalleFacturaNavigation)
                        .ThenInclude(df => df.IdProductoNavigation) // Para traer el nombre del producto devuelto
                .FirstOrDefaultAsync(m => m.IdDevolucion == id);

            if (devolucion == null) return NotFound();

            return View(devolucion);
        }

        // GET: DevolucionesVentas/Create
        public IActionResult Create()
        {
            ViewBag.IdCliente = new SelectList(_context.Clientes, "IdCliente", "Nombre");
            ViewBag.IdFactura = new SelectList(_context.FacturasVentasEncabezados, "IdFactura", "IdFactura");

            // Proyectamos el detalle para que el usuario vea: "Factura #1 - Producto X"
            var detallesFactura = _context.FacturasVentasDetalles
                .Include(d => d.IdProductoNavigation)
                .Select(d => new {
                    IdDetalle = d.IdDetalle,
                    Descripcion = $"Factura #{d.IdFactura} - {d.IdProductoNavigation.Nombre} (Q{d.Precio})"
                }).ToList();

            ViewBag.IdDetalleFactura = new SelectList(detallesFactura, "IdDetalle", "Descripcion");

            return View();
        }

        // POST: DevolucionesVentas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DevolucionesVentasEncabezado devolucion)
        {
            if (devolucion.DevolucionesVentasDetalles != null && devolucion.DevolucionesVentasDetalles.Any())
            {
                _context.Add(devolucion);
                await _context.SaveChangesAsync();

                TempData["MensajeExito"] = "Devolución sobre venta registrada correctamente.";
                return RedirectToAction(nameof(Index));
            }

            TempData["MensajeError"] = "Debe agregar artículos a devolver.";

            ViewBag.IdCliente = new SelectList(_context.Clientes, "IdCliente", "Nombre", devolucion.IdCliente);
            ViewBag.IdFactura = new SelectList(_context.FacturasVentasEncabezados, "IdFactura", "IdFactura", devolucion.IdFactura);

            return View(devolucion);
        }
    }
}