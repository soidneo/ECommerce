using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Clases
{
    public class CombosHelper : IDisposable
    {
        private static ECommerceContext db = new ECommerceContext();

        public static List<Departamento> GetDepartamentos()
        {
            var departamentos = db.Departamentos.ToList();
            departamentos.Add(new Departamento
            {
                DepartamentoID = 0,
                Nombre = "[Seleccione un departamento...]"
            });
            return departamentos.OrderBy(d => d.Nombre).ToList();
        }
        public static List<Ciudad> GetCiudades()
        {
            var ciudades = db.Ciudads.ToList();
            ciudades.Add(new Ciudad
            {
                CiudadID = 0,
                Nombre = "[Seleccione una ciudad...]"
            });
            return ciudades.OrderBy(c => c.Nombre).ToList();
        }

        public static List<Empresa> GetEmpresas()
        {
            var empresas = db.Empresas.ToList();
            empresas.Add(new Empresa
            {
                EmpresaID = 0,
                Nombre = "[Seleccione una empresa...]"
            });
            return empresas.OrderBy(e => e.Nombre).ToList();
        }
        public static List<Categoria> GetCategorias(int EmpresaID)
        {
            var categorias = db.Categorias.Where(c => c.EmpresaID == EmpresaID).ToList();
            categorias.Add(new Categoria
            {
                CategoriaID = 0,
                Descripcion = "[Seleccione una categoria...]"
            });
            return categorias.OrderBy(c => c.Descripcion).ToList();
        }

        public static List<Impuesto> GetImpuestos(int EmpresaID)
        {
            var impuestos = db.Impuestoes.Where(i => i.EmpresaID == EmpresaID).ToList();
            impuestos.Add(new Impuesto
            {
                ImpuestoID = 0,
                Descripcion = "[Seleccione un impuesto...]"
            });
            return impuestos.OrderBy(i => i.Descripcion).ToList();
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
