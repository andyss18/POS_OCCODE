using System;
using System.Collections.Generic;

namespace POS_OCCODE.Models;

public partial class ComprasDevolucionesDet
{
    public int IdDetalle { get; set; }

    public int Cantidad { get; set; }

    public decimal Precio { get; set; }

    public int IdDevolucion { get; set; }

    public int IdDetalleFactura { get; set; }

    public virtual FacturasComprasDetalle IdDetalleFacturaNavigation { get; set; } = null!;

    public virtual ComprasDevolucionesEnc IdDevolucionNavigation { get; set; } = null!;
}
