using System;
using System.Collections.Generic;

namespace POS_OCCODE.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? CodigoPostal { get; set; }

    public string? Telefono { get; set; }

    public string? Nit { get; set; }

    public virtual ICollection<DevolucionesVentasEncabezado> DevolucionesVentasEncabezados { get; set; } = new List<DevolucionesVentasEncabezado>();

    public virtual ICollection<EntradasSalidasEncabezado> EntradasSalidasEncabezados { get; set; } = new List<EntradasSalidasEncabezado>();

    public virtual ICollection<FacturasVentasEncabezado> FacturasVentasEncabezados { get; set; } = new List<FacturasVentasEncabezado>();
}
