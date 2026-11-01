using System;
using System.Collections.Generic;

namespace POS_OCCODE.Models;

public partial class FacturasVentasDetalle
{
    public int IdDetalle { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public decimal Precio { get; set; }

    public int IdSucursal { get; set; }

    public int IdFactura { get; set; }

    public virtual ICollection<DevolucionesVentasDetalle> DevolucionesVentasDetalles { get; set; } = new List<DevolucionesVentasDetalle>();

    public virtual FacturasVentasEncabezado IdFacturaNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Sucursale IdSucursalNavigation { get; set; } = null!;
}
