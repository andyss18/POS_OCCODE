using System;
using System.Collections.Generic;

namespace POS_OCCODE.Models;

public partial class EntradasSalidasEncabezado
{
    public int IdEntradaSalida { get; set; }

    public string TipoEntradaSalida { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public int? IdProveedor { get; set; }

    public int? Idcliente { get; set; }

    public string? Motivo { get; set; }

    public virtual ICollection<EntradasSalidasDetalle> EntradasSalidasDetalles { get; set; } = new List<EntradasSalidasDetalle>();

    public virtual Proveedore? IdProveedorNavigation { get; set; }

    public virtual Cliente? IdclienteNavigation { get; set; }
}
