using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class ECommerceContext: DbContext
    {
        public ECommerceContext():base("DefaultConnection")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
        public DbSet<Departamento> Departamentos { get; set; }

        public System.Data.Entity.DbSet<ECommerce.Models.Ciudad> Ciudads { get; set; }

        public System.Data.Entity.DbSet<ECommerce.Models.Empresa> Empresas { get; set; }

        public System.Data.Entity.DbSet<ECommerce.Models.Usuario> Usuarios { get; set; }

        public System.Data.Entity.DbSet<ECommerce.Models.Categoria> Categorias { get; set; }

        public System.Data.Entity.DbSet<ECommerce.Models.Impuesto> Impuestoes { get; set; }

        public System.Data.Entity.DbSet<ECommerce.Models.Producto> Productoes { get; set; }

        public System.Data.Entity.DbSet<ECommerce.Models.Bodega> Bodegas { get; set; }

        public System.Data.Entity.DbSet<ECommerce.Models.Inventario> Inventarios { get; set; }

        public System.Data.Entity.DbSet<ECommerce.Models.Cliente> Clientes { get; set; }

        public System.Data.Entity.DbSet<ECommerce.Models.Estado> Estadoes { get; set; }

        public System.Data.Entity.DbSet<ECommerce.Models.Venta> Ventas { get; set; }

        public System.Data.Entity.DbSet<ECommerce.Models.VentaDetalle> VentaDetalles { get; set; }

        public System.Data.Entity.DbSet<ECommerce.Models.VentaDetalleTmp> VentaDetalleTmps { get; set; }
    }
}
