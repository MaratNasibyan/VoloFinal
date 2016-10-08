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
using BookStore.Entities.CountryViewModel;
using BookStore.Entities.AuthorViewModel;
namespace BookStore.MVC.Controllers
{
    public class CountryPublishedsController : Controller
    {
        //private BookDatabaseEntities db = new BookDatabaseEntities();
        IRepository<CountryPublished> db;
           
        public CountryPublishedsController()
        {
            db = new CauntryRepository();
        }
        // GET: CountryPublisheds
        [Authorize]
        public ActionResult Index()
        {
            try
            {
                //var countries = db.CountryPublisheds.ToList();
                var countries = db.GetList();
                CauntryListModel model = new CauntryListModel
                {
                    CountryList = CauntryRelase.GetCountryResult(countries)
                };
                return View(model.CountryList);
                //return View(await db.CountryPublisheds.ToListAsync());
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: CountryPublisheds/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            CountryViewModel model;
            try
            {
                if (id == null)
                {
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    return PartialView("PartialNotFound", id.ToString());
                }
                //CountryPublished countryPublished = await db.CountryPublisheds.FindAsync(id);
                CountryPublished countryPublished = await db.GetData(id);
                if (countryPublished == null)
                {
                    //return HttpNotFound();
                    return PartialView("PartialNotFound", id.ToString());

                }
                model = CauntryRelase.DetailsCountry(countryPublished);

                return View(model);
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
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CountryName,IsoCode,PhoneCode")] CountryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var country = CauntryRelase.CreateCountry(model);
                    db.Create(country);
                    db.Save();
                    //db.CountryPublisheds.Add(country);
                    //await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: CountryPublisheds/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    return PartialView("PartialNotFound", id.ToString());
                }
                //CountryPublished countryPublished = await db.CountryPublisheds.FindAsync(id);
                CountryPublished countryPublished = await db.GetData(id);
                if (countryPublished == null)
                {
                    //return HttpNotFound();
                    return PartialView("PartialNotFound", id.ToString());

                }
                var model = CauntryRelase.EditCountry(countryPublished);
                return View(model);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // POST: CountryPublisheds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CountryName,IsoCode,PhoneCode")] CountryViewModel mod)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var country = CauntryRelase.EditCountry(mod);
                    db.Update(country);
                    db.Save();
                    //db.Entry(country).State = EntityState.Modified;
                    //await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(mod);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: CountryPublisheds/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    return PartialView("PartialNotFound", id.ToString());

                }
                //CountryPublished countryPublished = await db.CountryPublisheds.FindAsync(id);
                CountryPublished countryPublished = await db.GetData(id);
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
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                //CountryPublished countryPublished = await db.CountryPublisheds.FindAsync(id);
                //db.CountryPublisheds.Remove(countryPublished);
                //await db.SaveChangesAsync();
                CountryPublished countryPublished = await db.GetData(id);
                db.Delete(id);
                db.Save();
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
