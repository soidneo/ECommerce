﻿using System;
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
    public class ImpuestosController : Controller
    {
        private ECommerceContext db = new ECommerceContext();

        // GET: Impuestoes
        public ActionResult Index()
        {
            var user = db.Usuarios.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var impuestos = db.Impuestoes.Where(t => t.EmpresaID == user.EmpresaID);
            return View(impuestos.ToList());
        }

        // GET: Impuestoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Impuesto impuesto = db.Impuestoes.Find(id);
            if (impuesto == null)
            {
                return HttpNotFound();
            }
            return View(impuesto);
        }

        // GET: Impuestoes/Create
        public ActionResult Create()
        {
            var user = db.Usuarios.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var impuesto = new Impuesto { EmpresaID = user.EmpresaID, };
            return View(impuesto);  
        }

        // POST: Impuestoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Impuesto impuesto)
        {
            if (ModelState.IsValid)
            {
                db.Impuestoes.Add(impuesto);
                var respuesta = DbHelper.Guardar(db);
                if (respuesta.Succeeded == false)
                {
                    ModelState.AddModelError(string.Empty, respuesta.Message);
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            return View(impuesto);
        }

        // GET: Impuestoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Impuesto impuesto = db.Impuestoes.Find(id);
            if (impuesto == null)
            {
                return HttpNotFound();
            }
            return View(impuesto);
        }

        // POST: Impuestoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Impuesto impuesto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(impuesto).State = EntityState.Modified;
                var respuesta = DbHelper.Guardar(db);
                if (respuesta.Succeeded == false)
                {
                    ModelState.AddModelError(string.Empty, respuesta.Message);
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            return View(impuesto);
        }

        // GET: Impuestoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Impuesto impuesto = db.Impuestoes.Find(id);
            if (impuesto == null)
            {
                return HttpNotFound();
            }
            return View(impuesto);
        }

        // POST: Impuestoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Impuesto impuesto = db.Impuestoes.Find(id);
            db.Impuestoes.Remove(impuesto);
            var respuesta = DbHelper.Guardar(db);
            if (respuesta.Succeeded == false)
            {
                ModelState.AddModelError(string.Empty, respuesta.Message);
                return RedirectToAction("Index");
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
