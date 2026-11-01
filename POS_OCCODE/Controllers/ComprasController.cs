using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POS_OCCODE.Models;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace POS_OCCODE.Controllers
{
    public class ComprasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComprasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Compras
        public async Task<IActionResult> Index()
        {
            var compras = await _context.FacturasComprasEncabezados
                .Include(c => c.IdProveedorNavigation)
                .OrderByDescending(c => c.Fecha)
                .ThenByDescending(c => c.IdFactura)
                .ToListAsync();

            return View(compras);
        }

        // GET: Compras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var compra = await _context.FacturasComprasEncabezados
                .Include(c => c.IdProveedorNavigation)
                .Include(c => c.FacturasComprasDetalles)
                    .ThenInclude(d => d.IdProductoNavigation)
                .Include(c => c.FacturasComprasDetalles)
                    .ThenInclude(d => d.IdSucursalNavigation)
                .FirstOrDefaultAsync(m => m.IdFactura == id);

            if (compra == null) return NotFound();

            return View(compra);
        }

        // GET: Compras/Create
        public IActionResult Create()
        {
            ViewBag.IdProveedor = new SelectList(_context.Proveedores, "IdProveedor", "NombreProveedor");
            ViewBag.IdProducto = new SelectList(_context.Productos, "IdProducto", "Nombre");
            ViewBag.IdSucursal = new SelectList(_context.Sucursales, "IdSucursal", "NombreSucursal");

            return View();
        }

        // POST: Compras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FacturasComprasEncabezado facturaCompra)
        {
            if (facturaCompra.FacturasComprasDetalles != null && facturaCompra.FacturasComprasDetalles.Any())
            {
                _context.Add(facturaCompra);
                await _context.SaveChangesAsync();

                TempData["MensajeExito"] = "Compra registrada correctamente.";
                return RedirectToAction(nameof(Index));
            }

            TempData["MensajeError"] = "Debe agregar productos a la factura de compra.";

            ViewBag.IdProveedor = new SelectList(_context.Proveedores, "IdProveedor", "NombreProveedor", facturaCompra.IdProveedor);
            ViewBag.IdProducto = new SelectList(_context.Productos, "IdProducto", "Nombre");
            ViewBag.IdSucursal = new SelectList(_context.Sucursales, "IdSucursal", "NombreSucursal");

            return View(facturaCompra);
        }
    }
}