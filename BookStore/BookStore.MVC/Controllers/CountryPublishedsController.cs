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
    public class CountryPublishedsController : Controller
    {
        private BookDatabaseEntities db = new BookDatabaseEntities();

        // GET: CountryPublisheds
        public async Task<ActionResult> Index()
        {
            return View(await db.CountryPublisheds.ToListAsync());
        }

        // GET: CountryPublisheds/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return PartialView("PartialNotFound", id.ToString());
            }
            CountryPublished countryPublished = await db.CountryPublisheds.FindAsync(id);
            if (countryPublished == null)
            {
                //return HttpNotFound();
                return PartialView("PartialNotFound", id.ToString());

            }
            return View(countryPublished);
        }

        // GET: CountryPublisheds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CountryPublisheds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CountryName,IsoCode,PhoneCode")] CountryPublished countryPublished)
        {
            if (ModelState.IsValid)
            {
                db.CountryPublisheds.Add(countryPublished);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(countryPublished);
        }

        // GET: CountryPublisheds/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return PartialView("PartialNotFound", id.ToString());

            }
            CountryPublished countryPublished = await db.CountryPublisheds.FindAsync(id);
            if (countryPublished == null)
            {
                //return HttpNotFound();
                return PartialView("PartialNotFound", id.ToString());

            }
            return View(countryPublished);
        }

        // POST: CountryPublisheds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CountryName,IsoCode,PhoneCode")] CountryPublished countryPublished)
        {
            if (ModelState.IsValid)
            {
                db.Entry(countryPublished).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(countryPublished);
        }

        // GET: CountryPublisheds/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return PartialView("PartialNotFound", id.ToString());

            }
            CountryPublished countryPublished = await db.CountryPublisheds.FindAsync(id);
            if (countryPublished == null)
            {
                //return HttpNotFound();
                return PartialView("PartialNotFound", id.ToString());

            }
            return View(countryPublished);
        }

        // POST: CountryPublisheds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CountryPublished countryPublished = await db.CountryPublisheds.FindAsync(id);
            db.CountryPublisheds.Remove(countryPublished);
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
