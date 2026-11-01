using System;
using System.Collections.Generic;

namespace POS_OCCODE.Models;

public partial class Proveedore
{
    public int IdProveedor { get; set; }

    public string NombreProveedor { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? CodigoPostal { get; set; }

    public string? Telefono { get; set; }

    public string? NitProveedor { get; set; }

    public virtual ICollection<ComprasDevolucionesEnc> ComprasDevolucionesEncs { get; set; } = new List<ComprasDevolucionesEnc>();

    public virtual ICollection<EntradasSalidasEncabezado> EntradasSalidasEncabezados { get; set; } = new List<EntradasSalidasEncabezado>();

    public virtual ICollection<FacturasComprasEncabezado> FacturasComprasEncabezados { get; set; } = new List<FacturasComprasEncabezado>();
}
