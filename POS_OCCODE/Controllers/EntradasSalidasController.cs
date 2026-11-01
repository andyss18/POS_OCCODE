using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POS_OCCODE.Models;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace POS_OCCODE.Controllers
{
    public class EntradasSalidasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EntradasSalidasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EntradasSalidas
        public async Task<IActionResult> Index()
        {
            var historial = await _context.EntradasSalidasEncabezados
                .Include(e => e.IdProveedorNavigation)
                .Include(e => e.IdclienteNavigation)
                .OrderByDescending(e => e.Fecha)
                .ThenByDescending(e => e.IdEntradaSalida)
                .ToListAsync();

            return View(historial);
        }

        // GET: EntradasSalidas/Create
        public IActionResult Create()
        {
            ViewBag.IdProveedor = new SelectList(_context.Proveedores, "IdProveedor", "NombreProveedor");
            ViewBag.Idcliente = new SelectList(_context.Clientes, "IdCliente", "Nombre");
            ViewBag.IdProducto = new SelectList(_context.Productos, "IdProducto", "Nombre");
            ViewBag.IdSucursal = new SelectList(_context.Sucursales, "IdSucursal", "NombreSucursal");

            return View();
        }
        // GET: EntradasSalidas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccion = await _context.EntradasSalidasEncabezados
                .Include(e => e.IdProveedorNavigation)
                .Include(e => e.IdclienteNavigation)
                .Include(e => e.EntradasSalidasDetalles)
                    .ThenInclude(d => d.IdProductoNavigation) // Trae el nombre del producto
                .Include(e => e.EntradasSalidasDetalles)
                    .ThenInclude(d => d.IdSucursalNavigation) // Trae el nombre de la sucursal
                .FirstOrDefaultAsync(m => m.IdEntradaSalida == id);

            if (transaccion == null)
            {
                return NotFound();
            }

            return View(transaccion);
        }

        // POST: EntradasSalidas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EntradasSalidasEncabezado encabezado)
        {
            // Se asume que la vista enviará la colección EntradasSalidasDetalles correctamente mapeada
            if (encabezado.EntradasSalidasDetalles != null && encabezado.EntradasSalidasDetalles.Any())
            {
                _context.Add(encabezado);
                await _context.SaveChangesAsync(); // Los triggers en BD harán el resto

                TempData["MensajeExito"] = "Transacción registrada correctamente.";
                return RedirectToAction(nameof(Index));
            }

            TempData["MensajeError"] = "Debe agregar al menos un producto al detalle.";

            ViewBag.IdProveedor = new SelectList(_context.Proveedores, "IdProveedor", "NombreProveedor", encabezado.IdProveedor);
            ViewBag.Idcliente = new SelectList(_context.Clientes, "IdCliente", "Nombre", encabezado.Idcliente);
            ViewBag.IdProducto = new SelectList(_context.Productos, "IdProducto", "Nombre");
            ViewBag.IdSucursal = new SelectList(_context.Sucursales, "IdSucursal", "NombreSucursal");

            return View(encabezado);
        }
    }
}