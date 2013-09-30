using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VendaMasterControl.Models;

namespace VendaMasterControl.Controllers
{ 
    public class ClienteController : Controller
    {
        private NSAADMEntities db = new NSAADMEntities();

        //
        // GET: /Cliente/

        public ViewResult Index()
        {
            return View(db.Clientes.ToList());
        }

        //
        // GET: /Cliente/Details/5

        public ViewResult Details(decimal id)
        {
            Cliente cliente = db.Clientes.Single(c => c.id == id);
            return View(cliente);
        }

        //
        // GET: /Cliente/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Cliente/Create

        [HttpPost]
        public ActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Clientes.AddObject(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(cliente);
        }
        
        //
        // GET: /Cliente/Edit/5
 
        public ActionResult Edit(decimal id)
        {
            Cliente cliente = db.Clientes.Single(c => c.id == id);
            return View(cliente);
        }

        //
        // POST: /Cliente/Edit/5

        [HttpPost]
        public ActionResult Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Clientes.Attach(cliente);
                db.ObjectStateManager.ChangeObjectState(cliente, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        //
        // GET: /Cliente/Delete/5
 
        public ActionResult Delete(decimal id)
        {
            Cliente cliente = db.Clientes.Single(c => c.id == id);
            return View(cliente);
        }

        //
        // POST: /Cliente/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(decimal id)
        {            
            Cliente cliente = db.Clientes.Single(c => c.id == id);
            db.Clientes.DeleteObject(cliente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}