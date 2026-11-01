using System;
using System.Collections.Generic;

namespace POS_OCCODE.Models;

public partial class ComprasDevolucionesEnc
{
    public int IdDevolucion { get; set; }

    public int IdProveedor { get; set; }

    public DateOnly Fecha { get; set; }

    public string? NotaCreditoFel { get; set; }

    public string? SerieCreditoFel { get; set; }

    public int IdFactura { get; set; }

    public virtual ICollection<ComprasDevolucionesDet> ComprasDevolucionesDets { get; set; } = new List<ComprasDevolucionesDet>();

    public virtual FacturasComprasEncabezado IdFacturaNavigation { get; set; } = null!;

    public virtual Proveedore IdProveedorNavigation { get; set; } = null!;
}
