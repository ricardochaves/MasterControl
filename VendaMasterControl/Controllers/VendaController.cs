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
    public class VendaController : Controller
    {
        private NSAADMEntities db = new NSAADMEntities();

        //
        // GET: /Venda/

        public ViewResult Index()
        {
            var vendas = db.Vendas.Include("Cliente");
            return View(vendas.ToList());
        }

        //
        // GET: /Venda/Details/5

        public ViewResult Details(decimal id)
        {
            Venda venda = db.Vendas.Single(v => v.id == id);
            return View(venda);
        }

        //
        // GET: /Venda/Create

        public ActionResult Create()
        {
            ViewBag.id_cliente = new SelectList(db.Clientes, "id", "nome");
            return View();
        } 

        //
        // POST: /Venda/Create

        [HttpPost]
        public ActionResult Create(Venda venda)
        {
            if (ModelState.IsValid)
            {
                db.Vendas.AddObject(venda);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.id_cliente = new SelectList(db.Clientes, "id", "nome", venda.id_cliente);
            return View(venda);
        }
        
        //
        // GET: /Venda/Edit/5
 
        public ActionResult Edit(decimal id)
        {
            Venda venda = db.Vendas.Single(v => v.id == id);
            ViewBag.id_cliente = new SelectList(db.Clientes, "id", "nome", venda.id_cliente);
            return View(venda);
        }

        //
        // POST: /Venda/Edit/5

        [HttpPost]
        public ActionResult Edit(Venda venda)
        {
            if (ModelState.IsValid)
            {
                db.Vendas.Attach(venda);
                db.ObjectStateManager.ChangeObjectState(venda, EntityState.Modified);

                ///*--------------------------------------*/
                //Session["Venda"] = venda;
                //return RedirectToAction("Create", "VendaProduto");
                ///*--------------------------------------*/

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_cliente = new SelectList(db.Clientes, "id", "nome", venda.id_cliente);
            return View(venda);
        }

        //
        // GET: /Venda/Delete/5
 
        public ActionResult Delete(decimal id)
        {
            Venda venda = db.Vendas.Single(v => v.id == id);
            return View(venda);
        }

        //
        // POST: /Venda/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(decimal id)
        {            
            Venda venda = db.Vendas.Single(v => v.id == id);
            db.Vendas.DeleteObject(venda);
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