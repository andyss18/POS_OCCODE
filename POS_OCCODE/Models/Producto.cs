    using System;
    using System.Collections.Generic;

    namespace POS_OCCODE.Models;

    public partial class Producto
    {
        public int IdProducto { get; set; }

        public string Nombre { get; set; } = null!;

        public decimal PrecioCompra { get; set; }

        public decimal PrecioVenta { get; set; }

        public virtual ICollection<EntradasSalidasDetalle> EntradasSalidasDetalles { get; set; } = new List<EntradasSalidasDetalle>();

        public virtual ICollection<ExistenciasBodega> ExistenciasBodegas { get; set; } = new List<ExistenciasBodega>();

        public virtual ICollection<FacturasComprasDetalle> FacturasComprasDetalles { get; set; } = new List<FacturasComprasDetalle>();

        public virtual ICollection<FacturasVentasDetalle> FacturasVentasDetalles { get; set; } = new List<FacturasVentasDetalle>();
    }
