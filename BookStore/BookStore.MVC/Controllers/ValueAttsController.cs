using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Entities;

namespace BookStore.MVC.Controllers
{
    public class ValueAttsController : Controller
    {
        private BookDatabaseEntities db = new BookDatabaseEntities();

        // GET: ValueAtts
        public ActionResult Index()
        {
            var valueAtts = db.ValueAtts.Include(v => v.Attribute);
            return View(valueAtts.ToList());
        }

        // GET: ValueAtts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValueAtt valueAtt = db.ValueAtts.Find(id);
            if (valueAtt == null)
            {
                return HttpNotFound();
            }
            return View(valueAtt);
        }

        // GET: ValueAtts/Create
        public ActionResult Create(int? id)
        {
            ViewBag.AttributesId = new SelectList(db.Attributes, "Id", "Name");
            return View();
        }
       
        // POST: ValueAtts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cheked,ValueText,ValueDate,ValueInt,AttributesId")] ValueAtt valueAtt)
        {
            if (ModelState.IsValid)
            {
                db.ValueAtts.Add(valueAtt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AttributesId = new SelectList(db.Attributes, "Id", "Name", valueAtt.AttributesId);
            return View(valueAtt);
        }

        // GET: ValueAtts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValueAtt valueAtt = db.ValueAtts.Find(id);
            if (valueAtt == null)
            {
                return HttpNotFound();
            }
            ViewBag.AttributesId = new SelectList(db.Attributes, "Id", "Name", valueAtt.AttributesId);
            return View(valueAtt);
        }

        // POST: ValueAtts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cheked,ValueText,ValueDate,ValueInt,AttributesId")] ValueAtt valueAtt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(valueAtt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AttributesId = new SelectList(db.Attributes, "Id", "Name", valueAtt.AttributesId);
            return View(valueAtt);
        }

        // GET: ValueAtts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValueAtt valueAtt = db.ValueAtts.Find(id);
            if (valueAtt == null)
            {
                return HttpNotFound();
            }
            return View(valueAtt);
        }

        // POST: ValueAtts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ValueAtt valueAtt = db.ValueAtts.Find(id);
            db.ValueAtts.Remove(valueAtt);
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
