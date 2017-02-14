using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Clases
{
    public class MovimientosHelper : IDisposable
    {
        private static ECommerceContext db = new ECommerceContext();



        public void Dispose()
        {
            db.Dispose();
        }

        public static Respuesta NuevaVenta(NuevaVentaVista vista, string userName)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var user = db.Usuarios.Where(u => u.UserName == userName).FirstOrDefault();
                    var venta = new Venta
                    {
                        EmpresaID = user.EmpresaID,
                        ClienteID = vista.ClienteID,
                        Fecha = vista.Fecha,
                        Comentarios = vista.Comentarios,
                        EstadoID = DbHelper.GetEstado("Creada", db),
                    };
                    db.Ventas.Add(venta);
                    db.SaveChanges();

                    var detalles = db.VentaDetallesTmps.Where(v => v.UserName == userName).ToList();
                    foreach (var detalle in detalles)
                    {
                        var ventaDetalles = new VentaDetalle
                        {
                            VentaID = venta.VentaID,
                            descripcion = detalle.Descripcion,
                            Precio = detalle.Precio,
                            ProductoID = detalle.ProductoID,
                            Cantidad = detalle.Cantidad,
                            Tasa = detalle.Tasa,
                        };
                        db.VentaDetalles.Add(ventaDetalles);
                        db.VentaDetallesTmps.Remove(detalle);
                    }
                    db.SaveChanges();
                    transaction.Commit();
                    return new Respuesta { Succeeded = true, };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new Respuesta { Succeeded = false, Message = ex.Message, };
                }
            }
        }
    }
}