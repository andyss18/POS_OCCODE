using System;
using System.Collections.Generic;

namespace POS_OCCODE.Models;

public partial class FacturasComprasEncabezado
{
    public int IdFactura { get; set; }

    public int IdProveedor { get; set; }

    public DateOnly Fecha { get; set; }

    public string? NumeroFel { get; set; }

    public string? SerieFel { get; set; }

    public virtual ICollection<ComprasDevolucionesEnc> ComprasDevolucionesEncs { get; set; } = new List<ComprasDevolucionesEnc>();

    public virtual ICollection<FacturasComprasDetalle> FacturasComprasDetalles { get; set; } = new List<FacturasComprasDetalle>();

    public virtual Proveedore IdProveedorNavigation { get; set; } = null!;
}
