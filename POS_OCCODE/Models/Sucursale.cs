using System;
using System.Collections.Generic;

namespace POS_OCCODE.Models;

public partial class Sucursale
{
    public int IdSucursal { get; set; }

    public string NombreSucursal { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public int? IdEncargado { get; set; }

    public virtual ICollection<EntradasSalidasDetalle> EntradasSalidasDetalles { get; set; } = new List<EntradasSalidasDetalle>();

    public virtual ICollection<ExistenciasBodega> ExistenciasBodegas { get; set; } = new List<ExistenciasBodega>();

    public virtual ICollection<FacturasComprasDetalle> FacturasComprasDetalles { get; set; } = new List<FacturasComprasDetalle>();

    public virtual ICollection<FacturasVentasDetalle> FacturasVentasDetalles { get; set; } = new List<FacturasVentasDetalle>();
}
