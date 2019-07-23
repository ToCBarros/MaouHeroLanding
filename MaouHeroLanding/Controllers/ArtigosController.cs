using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MaouHeroLanding.Models;

namespace MaouHeroLanding.Controllers
{
    //[Authorize]
    public class ArtigosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Artigos
       
        public ActionResult Index()
        {
            return View(db.Artigos.ToList());
        }

        // GET: Artigos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artigos artigos = db.Artigos.Find(id);
            if (artigos == null)
            {
                return HttpNotFound();
            }
            return View(artigos);
        }

        [Authorize(Roles = "gestor")]
        // GET: Artigos/Create
        public ActionResult Create()
        {
            Session["id"] = -1;
            Session["ac"] = "Artigos/Create";
            return View();
        }

        // POST: Artigos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "gestor")]
        public ActionResult Create([Bind(Include = "ID,Nome,Tipo,Preco,Data_Entrada,imagem,Descricao,Produtor")] Artigos artigos, HttpPostedFileBase fotografias)
        {
            if (Session["ac"] != "Artigos/Create")
                return RedirectToAction("Index","Artigos");
            string caminho = "";
            bool haFoto = false;

            // há ficheiro?
            if (fotografias == null)
            {
                
            }
            else
            {
                // há ficheiro
                // será correto?
                if (fotografias.ContentType == "image/jpeg" ||
                   fotografias.ContentType == "image/png")
                {
                    // estamos perante uma foto correta
                    string extensao = Path.GetExtension(fotografias.FileName).ToLower();
                    Guid g;
                    g = Guid.NewGuid();
                    // nome do ficheiro
                    string nome = g.ToString() + extensao;
                    // onde guardar o ficheiro
                    caminho = Path.Combine(Server.MapPath("~/imagens"), nome);
                    // atribuir ao agente o nome do ficheiro
                    artigos.imagem = nome;
                    // assinalar q há foto
                    haFoto = true;
                }
            }
            if (ModelState.IsValid)
            {
                db.Artigos.Add(artigos);
                db.SaveChanges();
                if (haFoto) fotografias.SaveAs(caminho);
                return RedirectToAction("Index");
            }

            return View(artigos);
        }

        [Authorize(Roles = "gestor")]
        // GET: Artigos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artigos artigos = db.Artigos.Find(id);
            if (artigos == null)
            {
                return HttpNotFound();
            }
            Session["id"] = id;
            Session["ac"] = "Artigos/Edit";
            return View(artigos);
        }

        // POST: Artigos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "gestor")]
        public ActionResult Edit([Bind(Include = "ID,Nome,Tipo,Preco,Data_Entrada,imagem,Descricao,Produtor")] Artigos artigos)
        {
            if (Session["ac"] != "Artigos/Edit" || (int)Session["id"]!= artigos.ID)
                return RedirectToAction("Index", "Artigos");
            if (ModelState.IsValid)
            {
                db.Entry(artigos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artigos);
        }

        [Authorize(Roles = "gestor")]
        // GET: Artigos/Delete/5
        public ActionResult Delete(int? id)
        {
            Session["id"] = id;
            Session["ac"] = "Artigos/Delete";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artigos artigos = db.Artigos.Find(id);
            if (artigos == null)
            {
                return HttpNotFound();
            }
            return View(artigos);
        }

        // POST: Artigos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "gestor")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["ac"] != "Artigos/Delete" || (int)Session["id"] != id)
                return RedirectToAction("Index", "Artigos");
            Artigos artigos = db.Artigos.Find(id);
            db.Artigos.Remove(artigos);
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
