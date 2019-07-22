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
    public class EncomendasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Encomendas
        [Authorize(Roles = "cliente,funcionario")]
        public ActionResult Index()
        {
            var encomendas = db.Encomendas.Include(e => e.Cliente);
            return View(encomendas.ToList());
        }

        // GET: Encomendas/Details/5
        [Authorize(Roles = "cliente,funcionario")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encomendas encomendas = db.Encomendas.Find(id);
            if (encomendas == null)
            {
                return HttpNotFound();
            }
            return View(encomendas);
        }

        // GET: Encomendas/Create
        [Authorize(Roles = "cliente")]
        public ActionResult Create()
        {
            ViewBag.ClienteFK = new SelectList(db.Clientes, "ID", "Nome");
            return View();
        }

        // POST: Encomendas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "cliente")]
        public ActionResult Create([Bind(Include = "ID,Local_entrega,Preco,Estado,ClienteFK")] Encomendas encomendas)
        {
            IList<Clientes> clienteslist = db.Clientes.ToList();
            foreach(Clientes c in clienteslist)
            {
                if (c.Username == User.Identity.Name)
                {
                    encomendas.ClienteFK = c.ID;
                }
            }

            if (ModelState.IsValid)
            {
                
                db.Encomendas.Add(encomendas);
                db.SaveChanges();
                return RedirectToAction("Create","Compras");
            }

            ViewBag.ClienteFK = new SelectList(db.Clientes, "ID", "Nome", encomendas.ClienteFK);
            return View(encomendas);
        }

        // GET: Encomendas/Edit/5
        [Authorize(Roles = "funcionario")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encomendas encomendas = db.Encomendas.Find(id);
            if (encomendas == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteFK = new SelectList(db.Clientes, "ID", "Nome", encomendas.ClienteFK);
            return View(encomendas);
        }

        // POST: Encomendas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "funcionario")]
        public ActionResult Edit([Bind(Include = "ID,Local_entrega,Preco,Estado,ClienteFK")] Encomendas encomendas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(encomendas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteFK = new SelectList(db.Clientes, "ID", "Nome", encomendas.ClienteFK);
            return View(encomendas);
        }

        // GET: Encomendas/Delete/5
        [Authorize(Roles = "cliente")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encomendas encomendas = db.Encomendas.Find(id);
            if (encomendas == null)
            {
                return HttpNotFound();
            }
            return View(encomendas);
        }

        // POST: Encomendas/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "cliente")]
        public ActionResult DeleteConfirmed(int id)
        {
            Encomendas encomendas = db.Encomendas.Find(id);
            db.Encomendas.Remove(encomendas);
            db.SaveChanges();
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
