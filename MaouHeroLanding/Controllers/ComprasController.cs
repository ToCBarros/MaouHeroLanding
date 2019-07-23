using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MaouHeroLanding.Models;

namespace MaouHeroLanding.Controllers
{
    [Authorize]
    public class ComprasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Compras
        [Authorize(Roles = "cliente")]
        public ActionResult Index(int? id)
        {
            ViewBag.id = id;
            if (id == null)
            {
                return RedirectToAction("Index", "Encomendas");
            }
            var compras = db.Compras.Include(c => c.Artigo).Include(c => c.Encomenda).Where(c=>c.EncomendaFK==id);
            System.Web.HttpContext.Current.Session["encomenda"] = id;
            return View(compras.ToList());
        }

        // GET: Compras/Details/5
        [Authorize(Roles = "cliente")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compras compras = db.Compras.Find(id);
            if (compras == null)
            {
                return HttpNotFound();
            }
            return View(compras);
        }

        // GET: Compras/Create
        [Authorize(Roles = "cliente")]
        public ActionResult Create()
        {
            ViewBag.ArtigoFK = new SelectList(db.Artigos, "ID", "Nome");
            ViewBag.EncomendaFK = new SelectList(db.Encomendas, "ID", "Local_entrega");
            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "cliente")]
        public ActionResult Create([Bind(Include = "id,preco,EncomendaFK,ArtigoFK")] Compras compras)
        {
            
            if (ModelState.IsValid)
            {
                int encomenda = Convert.ToInt32(System.Web.HttpContext.Current.Session["encomenda"]);
                compras.EncomendaFK = encomenda;
                db.Compras.Add(compras);
                db.SaveChanges();
                return RedirectToAction("Index",new { id=encomenda});
            }

            ViewBag.ArtigoFK = new SelectList(db.Artigos, "ID", "Nome", compras.ArtigoFK);
            ViewBag.EncomendaFK = new SelectList(db.Encomendas, "ID", "Local_entrega", compras.EncomendaFK);
            return View(compras);
        }

        // GET: Compras/Edit/5
        [Authorize(Roles = "cliente")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compras compras = db.Compras.Find(id);
            if (compras == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtigoFK = new SelectList(db.Artigos, "ID", "Nome", compras.ArtigoFK);
            ViewBag.EncomendaFK = new SelectList(db.Encomendas, "ID", "Local_entrega", compras.EncomendaFK);
            return View(compras);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "cliente")]
        public ActionResult Edit([Bind(Include = "id,preco,EncomendaFK,ArtigoFK")] Compras compras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Compras",new { id= Convert.ToInt32(System.Web.HttpContext.Current.Session["encomenda"]) });
            }
            ViewBag.ArtigoFK = new SelectList(db.Artigos, "ID", "Nome", compras.ArtigoFK);
            ViewBag.EncomendaFK = new SelectList(db.Encomendas, "ID", "Local_entrega", compras.EncomendaFK);
            return View(compras);
        }

        // GET: Compras/Delete/5
        [Authorize(Roles = "cliente")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compras compras = db.Compras.Find(id);
            if (compras == null)
            {
                return HttpNotFound();
            }
            return View(compras);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "cliente")]
        public ActionResult DeleteConfirmed(int id)
        {
            Compras compras = db.Compras.Find(id);
            db.Compras.Remove(compras);
            db.SaveChanges();
            return RedirectToAction("Index", "Compras", new { id = Convert.ToInt32(System.Web.HttpContext.Current.Session["encomenda"]) });
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
