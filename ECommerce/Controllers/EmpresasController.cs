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
    public class EmpresasController : Controller
    {
        private ECommerceContext db = new ECommerceContext();

        // GET: Empresas
        public ActionResult Index()
        {
            var empresas = db.Empresas.Include(e => e.Ciudad).Include(e => e.Departamento);
            return View(empresas.ToList());
        }

        // GET: Empresas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Empresas.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // GET: Empresas/Create
        public ActionResult Create()
        {
            ViewBag.CiudadID = new SelectList(Helper.GetCiudades(), "CiudadID", "Nombre");
            ViewBag.DepartamentoID = new SelectList(Helper.GetDepartamentos(),
                "DepartamentoID", "Nombre");
            return View();
        }

        // POST: Empresas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Logos";
                if (empresa.LogoFile != null)
                {
                    pic = FilesHelper.SubirImagen(empresa.LogoFile, folder);
                    pic = string.Format("{0}/{1}",folder,pic);
                }
                empresa.Logo = pic;
                db.Empresas.Add(empresa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CiudadID = new SelectList(Helper.GetCiudades(), "CiudadID", "Nombre");
            ViewBag.DepartamentoID = new SelectList(Helper.GetDepartamentos(),
                "DepartamentoID", "Nombre");
            return View(empresa);
        }

        // GET: Empresas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Empresas.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            ViewBag.CiudadID = new SelectList(Helper.GetCiudades(), "CiudadID", "Nombre");
            ViewBag.DepartamentoID = new SelectList(Helper.GetDepartamentos(),
                "DepartamentoID", "Nombre");
            return View(empresa);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpresaID,Nombre,Telefono,Direccion,Logo,DepartamentoID,CiudadID")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CiudadID = new SelectList(Helper.GetCiudades(), "CiudadID", "Nombre");
            ViewBag.DepartamentoID = new SelectList(Helper.GetDepartamentos(),
                "DepartamentoID", "Nombre");
            return View(empresa);
        }

        // GET: Empresas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Empresas.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empresa empresa = db.Empresas.Find(id);
            db.Empresas.Remove(empresa);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetCiudadesDe(int departamentoId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var ciudadesDe = db.Ciudads.Where(c => c.DepartamentoID == departamentoId);
            return Json(ciudadesDe);
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
