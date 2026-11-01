using System;
using System.Collections.Generic;

namespace POS_OCCODE.Models;

public partial class DevolucionesVentasDetalle
{
    public int IdDetalle { get; set; }

    public int Cantidad { get; set; }

    public decimal Precio { get; set; }

    public int IdDevolucion { get; set; }

    public int IdDetalleFactura { get; set; }

    public virtual FacturasVentasDetalle IdDetalleFacturaNavigation { get; set; } = null!;

    public virtual DevolucionesVentasEncabezado IdDevolucionNavigation { get; set; } = null!;
}
