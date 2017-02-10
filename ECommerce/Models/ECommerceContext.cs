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
    }
}
