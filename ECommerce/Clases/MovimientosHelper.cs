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

        public static Respuesta NuevaVenta(NuevaOrdenVista vista, string userName)
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

                    var detalles = db.VentaDetalleTmps.Where(v => v.UserName == userName).ToList();
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
                        db.VentaDetalleTmps.Remove(detalle);
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

        public static Respuesta NuevaCompra(NuevaCompraVista vista, string userName)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var user = db.Usuarios.Where(u => u.UserName == userName).FirstOrDefault();
                    var compra = new Compra
                    {
                        EmpresaID = user.EmpresaID,
                        ProveedorID = vista.ProveedorID,
                        BodegaID = vista.BodegaID,
                        FormaPagoID = vista.FormaPagoID,
                        Fecha = vista.Fecha,
                        Comentarios = vista.Comentarios,
                        EstadoID = DbHelper.GetEstado("Creada", db),
                    };
                    db.Compras.Add(compra);
                    db.SaveChanges();

                    var detalles = db.CompraDetalleTmps.Where(v => v.UserName == userName).ToList();
                    foreach (var detalle in detalles)
                    {
                        var compraDetalles = new CompraDetalle
                        {
                            CompraID = compra.CompraID,
                            descripcion = detalle.Descripcion,
                            Precio = detalle.Precio,
                            ProductoID = detalle.ProductoID,
                            Cantidad = detalle.Cantidad,
                            Tasa = detalle.Tasa,
                        };
                        db.CompraDetalles.Add(compraDetalles);
                        db.CompraDetalleTmps.Remove(detalle);
                    }
                    db.SaveChanges();
                    foreach (var detalle in detalles)
                    {
                        var invProducto = db.Inventarios
                            .Where(p => p.ProductoID == detalle.ProductoID 
                            && p.BodegaID == compra.BodegaID).FirstOrDefault();
                        double stockActual = 0;
                        if (invProducto != null)
                        {
                            stockActual = invProducto.stock;
                        }
                        var inventario = new Inventario
                        {
                            BodegaID = compra.BodegaID,
                            ProductoID = detalle.ProductoID,
                            stock = stockActual + detalle.Cantidad,
                     };
                        db.Inventarios.Add(inventario);
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

        public static Respuesta NuevaReceta(NuevaRecetaVista vista, string userName)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var user = db.Usuarios.Where(u => u.UserName == userName).FirstOrDefault();
                    var receta = new Receta
                    {
                        EmpresaID = user.EmpresaID,
                        Descripcion = vista.Descripcion,
                        Comentarios = vista.Comentarios,
                    };
                    db.Recetas.Add(receta);
                    db.SaveChanges();

                    var detalles = db.RecetaDetalleTmps.Where(v => v.UserName == userName).ToList();
                    foreach (var detalle in detalles)
                    {
                        var recetaDetalles = new RecetaDetalle
                        {
                            RecetaID = receta.RecetaID,
                            ProductoID = detalle.ProductoID,
                            Cantidad = detalle.Cantidad,
                            Precio = detalle.Precio,
                        };
                        db.RecetaDetalles.Add(recetaDetalles);
                        db.RecetaDetalleTmps.Remove(detalle);
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