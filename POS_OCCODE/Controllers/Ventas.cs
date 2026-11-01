using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POS_OCCODE.Models;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace POS_OCCODE.Controllers
{
    public class VentasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var facturas = await _context.FacturasVentasEncabezados
                .Include(f => f.IdClienteNavigation)
                .OrderByDescending(f => f.Fecha)
                .ThenByDescending(f => f.IdFactura)
                .ToListAsync();

            return View(facturas);
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var factura = await _context.FacturasVentasEncabezados
                .Include(f => f.IdClienteNavigation)
                .Include(f => f.FacturasVentasDetalles)
                    .ThenInclude(d => d.IdProductoNavigation)
                .Include(f => f.FacturasVentasDetalles)
                    .ThenInclude(d => d.IdSucursalNavigation)
                .FirstOrDefaultAsync(m => m.IdFactura == id);

            if (factura == null) return NotFound();

            return View(factura);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            ViewBag.IdCliente = new SelectList(_context.Clientes, "IdCliente", "Nombre");
            ViewBag.IdProducto = new SelectList(_context.Productos, "IdProducto", "Nombre");
            ViewBag.IdSucursal = new SelectList(_context.Sucursales, "IdSucursal", "NombreSucursal");

            return View();
        }

        // POST: Ventas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FacturasVentasEncabezado factura)
        {
            if (factura.FacturasVentasDetalles != null && factura.FacturasVentasDetalles.Any())
            {
                _context.Add(factura);
                await _context.SaveChangesAsync();

                TempData["MensajeExito"] = "Venta facturada correctamente.";
                return RedirectToAction(nameof(Index));
            }

            TempData["MensajeError"] = "Debe agregar productos a la factura.";

            ViewBag.IdCliente = new SelectList(_context.Clientes, "IdCliente", "Nombre", factura.IdCliente);
            ViewBag.IdProducto = new SelectList(_context.Productos, "IdProducto", "Nombre");
            ViewBag.IdSucursal = new SelectList(_context.Sucursales, "IdSucursal", "NombreSucursal");

            return View(factura);
        }
    }
}