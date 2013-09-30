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
    public class ProdutoController : Controller
    {
        private NSAADMEntities db = new NSAADMEntities();

        //
        // GET: /Produto/

        public ViewResult Index()
        {
            return View(db.Produtoes.ToList());
        }

        //
        // GET: /Produto/Details/5

        public ViewResult Details(decimal id)
        {
            Produto produto = db.Produtoes.Single(p => p.id == id);
            return View(produto);
        }

        //
        // GET: /Produto/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Produto/Create

        [HttpPost]
        public ActionResult Create(Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Produtoes.AddObject(produto);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(produto);
        }
        
        //
        // GET: /Produto/Edit/5
 
        public ActionResult Edit(decimal id)
        {
            Produto produto = db.Produtoes.Single(p => p.id == id);
            return View(produto);
        }

        //
        // POST: /Produto/Edit/5

        [HttpPost]
        public ActionResult Edit(Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Produtoes.Attach(produto);
                db.ObjectStateManager.ChangeObjectState(produto, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(produto);
        }

        //
        // GET: /Produto/Delete/5
 
        public ActionResult Delete(decimal id)
        {
            Produto produto = db.Produtoes.Single(p => p.id == id);
            return View(produto);
        }

        //
        // POST: /Produto/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(decimal id)
        {            
            Produto produto = db.Produtoes.Single(p => p.id == id);
            db.Produtoes.DeleteObject(produto);
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