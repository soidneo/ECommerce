using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Clases
{
    public class DbHelper
    {
        public static Respuesta Guardar(ECommerceContext db)
        {
            try
            {
                db.SaveChanges();
                return new Respuesta { Succeeded = true, };
            }
            catch (Exception ex)
            {

                return new Respuesta { Succeeded = false, Message = ex.Message };
            }
        }

        public static int GetEstado(string descripcion, ECommerceContext db)
        {
            var estado = db.Estadoes.Where(e => e.Descripcion == descripcion).FirstOrDefault();
            if (estado == null)
            {
                estado = new Estado { Descripcion = descripcion, };
                db.Estadoes.Add(estado);
                db.SaveChanges();
            }
            return estado.EstadoID;
        }
    }
}