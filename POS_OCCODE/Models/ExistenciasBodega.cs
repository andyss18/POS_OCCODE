using System;
using System.Collections.Generic;

namespace POS_OCCODE.Models;

public partial class ExistenciasBodega
{
    public int IdSucursal { get; set; }

    public int IdProducto { get; set; }

    public int Existencias { get; set; }

    public int Minimo { get; set; }

    public int Maximo { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Sucursale IdSucursalNavigation { get; set; } = null!;
}
