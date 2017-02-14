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
    public class ClientesController : Controller
    {
        private ECommerceContext db = new ECommerceContext();

        // GET: Clientes
        public ActionResult Index()
        {
            var user = db.Usuarios.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            var clientes = db.Clientes.Where(c => c.EmpresaID == user.EmpresaID)
                .Include(c => c.Ciudad)
                .Include(c => c.Departamento);
            return View(clientes.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            ViewBag.CiudadID = new SelectList(CombosHelper.GetCiudades(), "CiudadID", "Nombre");
            ViewBag.DepartamentoID = new SelectList(CombosHelper.GetDepartamentos(), "DepartamentoID", "Nombre");
            var user = db.Usuarios.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            var cliente = new Cliente { EmpresaID = user.EmpresaID, };
            return View(cliente);
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
                UsuariosHelper.CreateUserAsp(cliente.UserName,"Cliente");
                return RedirectToAction("Index");
            }

            ViewBag.CiudadID = new SelectList(CombosHelper.GetCiudades(), "CiudadID", "Nombre", cliente.CiudadID);
            ViewBag.DepartamentoID = new SelectList(CombosHelper.GetDepartamentos(), "DepartamentoID", "Nombre", cliente.DepartamentoID);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.CiudadID = new SelectList(CombosHelper.GetCiudades(), "CiudadID", "Nombre", cliente.CiudadID);
            ViewBag.DepartamentoID = new SelectList(CombosHelper.GetDepartamentos(), "DepartamentoID", "Nombre", cliente.DepartamentoID);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CiudadID = new SelectList(CombosHelper.GetCiudades(), "CiudadID", "Nombre", cliente.CiudadID);
            ViewBag.DepartamentoID = new SelectList(CombosHelper.GetDepartamentos(), "DepartamentoID", "Nombre", cliente.DepartamentoID);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
            db.SaveChanges();
            UsuariosHelper.DeleteUser(cliente.UserName);
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
