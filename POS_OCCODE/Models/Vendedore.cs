using System;
using System.Collections.Generic;

namespace POS_OCCODE.Models;

public partial class Vendedore
{
    public int IdVendedor { get; set; }

    public string NombreVendedor { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }
}
