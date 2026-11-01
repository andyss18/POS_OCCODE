using System;
using System.Collections.Generic;

namespace POS_OCCODE.Models;

public partial class DevolucionesVentasEncabezado
{
    public int IdDevolucion { get; set; }

    public int IdCliente { get; set; }

    public DateOnly Fecha { get; set; }

    public string? NotaCreditoFel { get; set; }

    public string? SerieCreditoFel { get; set; }

    public int IdFactura { get; set; }

    public virtual ICollection<DevolucionesVentasDetalle> DevolucionesVentasDetalles { get; set; } = new List<DevolucionesVentasDetalle>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual FacturasVentasEncabezado IdFacturaNavigation { get; set; } = null!;
}
