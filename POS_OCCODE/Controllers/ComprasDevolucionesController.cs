using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POS_OCCODE.Models;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace POS_OCCODE.Controllers
{
    public class ComprasDevolucionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComprasDevolucionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ComprasDevoluciones
        public async Task<IActionResult> Index()
        {
            var devoluciones = await _context.ComprasDevolucionesEncs
                .Include(c => c.IdProveedorNavigation)
                .Include(c => c.IdFacturaNavigation)
                .OrderByDescending(c => c.Fecha)
                .ThenByDescending(c => c.IdDevolucion)
                .ToListAsync();

            return View(devoluciones);
        }

        // GET: ComprasDevoluciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var devolucion = await _context.ComprasDevolucionesEncs
                .Include(c => c.IdProveedorNavigation)
                .Include(c => c.IdFacturaNavigation)
                .Include(c => c.ComprasDevolucionesDets)
                    .ThenInclude(det => det.IdDetalleFacturaNavigation)
                        .ThenInclude(df => df.IdProductoNavigation) // Trae el nombre del producto devuelto al proveedor
                .FirstOrDefaultAsync(m => m.IdDevolucion == id);

            if (devolucion == null) return NotFound();

            return View(devolucion);
        }

        // GET: ComprasDevoluciones/Create
        public IActionResult Create()
        {
            ViewBag.IdProveedor = new SelectList(_context.Proveedores, "IdProveedor", "NombreProveedor");
            ViewBag.IdFactura = new SelectList(_context.FacturasComprasEncabezados, "IdFactura", "IdFactura");

            // Proyectamos el detalle de compras para que el usuario vea: "Factura Compra #1 - Producto X"
            var detallesCompra = _context.FacturasComprasDetalles
                .Include(d => d.IdProductoNavigation)
                .Select(d => new {
                    IdDetalle = d.IdDetalle,
                    Descripcion = $"Compra #{d.IdFactura} - {d.IdProductoNavigation.Nombre} (Q{d.Precio})"
                }).ToList();

            ViewBag.IdDetalleFactura = new SelectList(detallesCompra, "IdDetalle", "Descripcion");

            return View();
        }

        // POST: ComprasDevoluciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ComprasDevolucionesEnc devolucion)
        {
            if (devolucion.ComprasDevolucionesDets != null && devolucion.ComprasDevolucionesDets.Any())
            {
                _context.Add(devolucion);
                await _context.SaveChangesAsync();

                TempData["MensajeExito"] = "Devolución a proveedor registrada correctamente.";
                return RedirectToAction(nameof(Index));
            }

            TempData["MensajeError"] = "Debe agregar artículos a devolver al proveedor.";

            ViewBag.IdProveedor = new SelectList(_context.Proveedores, "IdProveedor", "NombreProveedor", devolucion.IdProveedor);
            ViewBag.IdFactura = new SelectList(_context.FacturasComprasEncabezados, "IdFactura", "IdFactura", devolucion.IdFactura);

            return View(devolucion);
        }
    }
}