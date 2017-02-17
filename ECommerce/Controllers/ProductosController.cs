using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;
using ECommerce.Clases;

namespace ECommerce.Controllers
{
    [Authorize(Roles = "User")]
    public class ProductosController : Controller
    {
        private ECommerceContext db = new ECommerceContext();

        // GET: Productos
        public ActionResult Index()
        {
            var user = db.Usuarios.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var productoes = db.Productoes
                .Include(p => p.Categoria)
                .Include(p => p.Impuesto)
                .Where(p => p.EmpresaID == user.EmpresaID);
            return View(productoes.ToList());
        }

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productoes.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            var user = db.Usuarios.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewBag.CategoriaID = new SelectList(CombosHelper.GetCategorias(user.EmpresaID), "CategoriaID", "Descripcion");
            ViewBag.ImpuestoID = new SelectList(CombosHelper.GetImpuestos(user.EmpresaID), "ImpuestoID", "Descripcion");
            var producto = new Producto
            { EmpresaID = user.EmpresaID, };
            return View(producto);
        }

        // POST: Productos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Productoes.Add(producto);
                var respuesta = DbHelper.Guardar(db);
                if (respuesta.Succeeded == false)
                {
                    ModelState.AddModelError(string.Empty, respuesta.Message);
                    var user2 = db.Usuarios.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                    ViewBag.CategoriaID = new SelectList(CombosHelper.GetCategorias(user2.EmpresaID), "CategoriaID", "Descripcion", producto.CategoriaID);
                    ViewBag.ImpuestoID = new SelectList(CombosHelper.GetImpuestos(user2.EmpresaID), "ImpuestoID", "Descripcion", producto.ImpuestoID);
                    return View(producto);
                }
                if (producto.ImageFile != null)
                {
                    var folder = "~/Content/Images";
                    var fileName = string.Format("{0}.jpg", producto.ProductoID);

                    if (FilesHelper.SubirImagen(producto.ImageFile, folder,
                        fileName))
                    {

                        var pic = string.Format("{0}/{1}", folder, fileName);
                        producto.Image = pic;
                        db.Entry(producto).State = EntityState.Modified;
                        respuesta = DbHelper.Guardar(db);
                        if (respuesta.Succeeded == false)
                        {
                            ModelState.AddModelError(string.Empty, respuesta.Message);
                            var user2 = db.Usuarios.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                            ViewBag.CategoriaID = new SelectList(CombosHelper.GetCategorias(user2.EmpresaID), "CategoriaID", "Descripcion", producto.CategoriaID);
                            ViewBag.ImpuestoID = new SelectList(CombosHelper.GetImpuestos(user2.EmpresaID), "ImpuestoID", "Descripcion", producto.ImpuestoID);
                            return View(producto);
                        }

                    }

                }
                return RedirectToAction("Index");
            }

            var user = db.Usuarios.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewBag.CategoriaID = new SelectList(CombosHelper.GetCategorias(user.EmpresaID), "CategoriaID", "Descripcion", producto.CategoriaID);
            ViewBag.ImpuestoID = new SelectList(CombosHelper.GetImpuestos(user.EmpresaID), "ImpuestoID", "Descripcion", producto.ImpuestoID);
            
            return View(producto);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var producto = db.Productoes.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaID = new SelectList(CombosHelper.GetCategorias(producto.EmpresaID), "CategoriaID", "Descripcion", producto.CategoriaID);
            ViewBag.ImpuestoID = new SelectList(CombosHelper.GetImpuestos(producto.EmpresaID), "ImpuestoID", "Descripcion", producto.ImpuestoID);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Producto producto)
        {
            var respuesta = new Respuesta();
            if (ModelState.IsValid)
            {
                if (producto.ImageFile != null)
                {
                    var folder = "~/Content/Images";
                    var fileName = string.Format("{0}.jpg", producto.ProductoID);
                    if (FilesHelper.SubirImagen(producto.ImageFile, folder, fileName))
                    {
                        producto.Image = string.Format("{0}/{1}", folder, fileName);
                    }

                }
                db.Entry(producto).State = EntityState.Modified;
                respuesta = DbHelper.Guardar(db);
                if (respuesta.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, respuesta.Message);
            ViewBag.CategoriaID = new SelectList(CombosHelper.GetCategorias(producto.EmpresaID), "CategoriaID", "Descripcion", producto.CategoriaID);
            ViewBag.ImpuestoID = new SelectList(CombosHelper.GetImpuestos(producto.EmpresaID), "ImpuestoID", "Descripcion", producto.ImpuestoID);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productoes.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Productoes.Find(id);
            db.Productoes.Remove(producto);
            var respuesta = DbHelper.Guardar(db);
            if (respuesta.Succeeded == false)
            {
                ModelState.AddModelError(string.Empty, respuesta.Message);
                return View(producto);
            }

            return RedirectToAction("Index");
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
