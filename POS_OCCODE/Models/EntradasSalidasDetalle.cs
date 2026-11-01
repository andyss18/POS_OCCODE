using System;
using System.Collections.Generic;

namespace POS_OCCODE.Models;

public partial class EntradasSalidasDetalle
{
    public int IdDetalle { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public decimal Precio { get; set; }

    public int IdSucursal { get; set; }

    public int IdEntradaSalida { get; set; }

    public virtual EntradasSalidasEncabezado IdEntradaSalidaNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Sucursale IdSucursalNavigation { get; set; } = null!;
}
