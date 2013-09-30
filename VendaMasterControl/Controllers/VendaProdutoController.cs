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
    public class VendaProdutoController : Controller
    {
        private NSAADMEntities db = new NSAADMEntities();

        //
        // GET: /VendaProduto/

        public ViewResult Index()
        {
            var vendaprodutoes = db.VendaProdutoes.Include("Produto").Include("Venda");
            return View(vendaprodutoes.ToList());
        }

        //
        // GET: /VendaProduto/Details/5

        public ViewResult Details(decimal id)
        {
            VendaProduto vendaproduto = db.VendaProdutoes.Single(v => v.id == id);
            return View(vendaproduto);
        }

        //
        // GET: /VendaProduto/Create

        public ActionResult Create()
        {
            ViewBag.id_Produto = new SelectList(db.Produtoes, "id", "Descr");
            ViewBag.id_Venda = new SelectList(db.Vendas, "id", "id_ML");
            return View();
        } 

        //
        // POST: /VendaProduto/Create

        [HttpPost]
        public ActionResult Create(VendaProduto vendaproduto)
        {
            if (ModelState.IsValid)
            {
                db.VendaProdutoes.AddObject(vendaproduto);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.id_Produto = new SelectList(db.Produtoes, "id", "Descr", vendaproduto.id_Produto);
            ViewBag.id_Venda = new SelectList(db.Vendas, "id", "id_ML", vendaproduto.id_Venda);
            return View(vendaproduto);
        }
        
        //
        // GET: /VendaProduto/Edit/5
 
        public ActionResult Edit(decimal id)
        {
            VendaProduto vendaproduto = db.VendaProdutoes.Single(v => v.id == id);
            ViewBag.id_Produto = new SelectList(db.Produtoes, "id", "Descr", vendaproduto.id_Produto);
            ViewBag.id_Venda = new SelectList(db.Vendas, "id", "id_ML", vendaproduto.id_Venda);
            return View(vendaproduto);
        }

        //
        // POST: /VendaProduto/Edit/5

        [HttpPost]
        public ActionResult Edit(VendaProduto vendaproduto)
        {
            if (ModelState.IsValid)
            {
                db.VendaProdutoes.Attach(vendaproduto);
                db.ObjectStateManager.ChangeObjectState(vendaproduto, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Produto = new SelectList(db.Produtoes, "id", "Descr", vendaproduto.id_Produto);
            ViewBag.id_Venda = new SelectList(db.Vendas, "id", "id_ML", vendaproduto.id_Venda);
            return View(vendaproduto);
        }

        //
        // GET: /VendaProduto/Delete/5
 
        public ActionResult Delete(decimal id)
        {
            VendaProduto vendaproduto = db.VendaProdutoes.Single(v => v.id == id);
            return View(vendaproduto);
        }

        //
        // POST: /VendaProduto/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(decimal id)
        {            
            VendaProduto vendaproduto = db.VendaProdutoes.Single(v => v.id == id);
            db.VendaProdutoes.DeleteObject(vendaproduto);
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