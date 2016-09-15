using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult> Index()
        {
            var attributes = db.Attributes.Include(a => a.AttributeType);
            
            return View(await attributes.ToListAsync());
        }

        // GET: Attributes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entities.Attribute attribute = await db.Attributes.FindAsync(id);
            if (attribute == null)
            {
                return HttpNotFound();
            }
            return View(attribute);
        }

        // GET: Attributes/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.AttributeTypes, "Id", "Type");
            return View();
        }

        // POST: Attributes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] Entities.Attribute attribute)
        {
            if (ModelState.IsValid)
            {
                db.Attributes.Add(attribute);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.AttributeTypes, "Id", "Type", attribute.Id);
            return View(attribute);
        }

        // GET: Attributes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entities.Attribute attribute = await db.Attributes.FindAsync(id);
            if (attribute == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.AttributeTypes, "Id", "Type", attribute.Id);
            return View(attribute);
        }

        // POST: Attributes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] Entities.Attribute attribute)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attribute).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.AttributeTypes, "Id", "Type", attribute.Id);
            return View(attribute);
        }

        // GET: Attributes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entities.Attribute attribute = await db.Attributes.FindAsync(id);
            if (attribute == null)
            {
                return HttpNotFound();
            }
            return View(attribute);
        }

        // POST: Attributes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Entities.Attribute attribute = await db.Attributes.FindAsync(id);
            db.Attributes.Remove(attribute);
            await db.SaveChangesAsync();
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
