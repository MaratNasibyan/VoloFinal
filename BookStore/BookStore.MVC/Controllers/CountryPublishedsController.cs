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
            try
            {
                return View(await db.CountryPublisheds.ToListAsync());
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: CountryPublisheds/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
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
            catch
            {
                return RedirectToAction("Index");
            }
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
            try
            {
                if (ModelState.IsValid)
                {
                    db.CountryPublisheds.Add(countryPublished);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                return View(countryPublished);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: CountryPublisheds/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            try
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
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // POST: CountryPublisheds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CountryName,IsoCode,PhoneCode")] CountryPublished countryPublished)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(countryPublished).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(countryPublished);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: CountryPublisheds/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
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
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // POST: CountryPublisheds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                CountryPublished countryPublished = await db.CountryPublisheds.FindAsync(id);
                db.CountryPublisheds.Remove(countryPublished);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
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
