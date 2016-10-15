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
    public class AttributesController : Controller
    {
        private BookDatabaseEntities db = new BookDatabaseEntities();

        // GET: Attributes
        public ActionResult Index()
        {
            var attributes = db.Attributes.Include(a => a.AttributeType);
            return View(attributes.ToList());
        }

        [HttpGet]
        public ActionResult Get()
        {
            return View();
        }

       // sa atributi arjeq avelacnelu hamara
        [HttpPost]
        public ActionResult Get(int? id, string ValueText)
        {
            if (ValueText != "")
            {
                //var v1 = db.ValueAtts.Where(n => n.AttributesId == id).FirstOrDefault();
                ValueAtt v1 = new ValueAtt { AttributesId = id, ValueText = ValueText };
                db.ValueAtts.Add(v1);
                db.SaveChanges();
                return RedirectToAction("Index", "Attributes");
            }
            
            return View();
        }

        // GET: Attributes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entities.Attribute attribute = db.Attributes.Find(id);
            if (attribute == null)
            {
                return HttpNotFound();
            }
            return View(attribute);
        }

        // GET: Attributes/Create
        public ActionResult Create()
        {
            ViewBag.AttributeTypeId = new SelectList(db.AttributeTypes, "Id", "Type");
            return View();
        }

        // POST: Attributes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,AttributeTypeId")] Entities.Attribute attribute)
        {
            if (ModelState.IsValid)
            {
                db.Attributes.Add(attribute);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AttributeTypeId = new SelectList(db.AttributeTypes, "Id", "Type", attribute.AttributeTypeId);
            return View(attribute);
        }

        // GET: Attributes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entities.Attribute attribute = db.Attributes.Find(id);
            if (attribute == null)
            {
                return HttpNotFound();
            }
            ViewBag.AttributeTypeId = new SelectList(db.AttributeTypes, "Id", "Type", attribute.AttributeTypeId);
            return View(attribute);
        }

        // POST: Attributes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,AttributeTypeId")] Entities.Attribute attribute)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attribute).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AttributeTypeId = new SelectList(db.AttributeTypes, "Id", "Type", attribute.AttributeTypeId);
            return View(attribute);
        }

        // GET: Attributes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entities.Attribute attribute = db.Attributes.Find(id);
            if (attribute == null)
            {
                return HttpNotFound();
            }
            return View(attribute);
        }

        // POST: Attributes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Entities.Attribute attribute = db.Attributes.Find(id);
            db.Attributes.Remove(attribute);
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
