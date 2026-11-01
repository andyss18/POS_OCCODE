using System;
using System.Collections.Generic;

namespace POS_OCCODE.Models;

public partial class FacturasVentasEncabezado
{
    public int IdFactura { get; set; }

    public int IdCliente { get; set; }

    public DateOnly Fecha { get; set; }

    public string? NumeroFel { get; set; }

    public string? SerieFel { get; set; }

    public virtual ICollection<DevolucionesVentasEncabezado> DevolucionesVentasEncabezados { get; set; } = new List<DevolucionesVentasEncabezado>();

    public virtual ICollection<FacturasVentasDetalle> FacturasVentasDetalles { get; set; } = new List<FacturasVentasDetalle>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;
}
