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

        public DbSet<Ciudad> Ciudades { get; set; }

        public DbSet<Empresa> Empresas { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Impuesto> Impuestos { get; set; }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Bodega> Bodegas { get; set; }

        public DbSet<Inventario> Inventarios { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Estado> Estados { get; set; }

        public DbSet<Venta> Ventas { get; set; }

        public DbSet<VentaDetalle> VentaDetalles { get; set; }

        public DbSet<VentaDetalleTmp> VentaDetalleTmps { get; set; }

        public DbSet<EmpresaCliente> EmpresaClientes { get; set; }

        public DbSet<Receta> Recetas { get; set; }

        public DbSet<RecetaDetalle> RecetaDetalles { get; set; }

        public DbSet<RecetaDetalleTmp> RecetaDetalleTmps { get; set; }

        public DbSet<Unidad> Unidades { get; set; }

        public DbSet<FormaPago> FormaPagos { get; set; }

        public DbSet<ECommerce.Models.Compra> Compras { get; set; }

        public DbSet<ECommerce.Models.CompraDetalle> CompraDetalles { get; set; }

        public DbSet<ECommerce.Models.CompraDetalleTmp> CompraDetalleTmps { get; set; }

        public DbSet<ECommerce.Models.Proveedor> Proveedors { get; set; }
    }
}
