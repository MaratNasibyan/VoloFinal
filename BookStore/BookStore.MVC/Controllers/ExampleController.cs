using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Entities;
using PagedList.Mvc;
using PagedList;

namespace BookStore.MVC.Controllers
{
    public class ExampleController : Controller
    {
        private BookDatabaseEntities db = new BookDatabaseEntities();

        //GET: Example
        public ActionResult Index(int? page)
        {
            var books = db.Books.Include(b => b.Author).Include(b => b.CountryPublished);
            //return Request.IsAjaxRequest() ? (ActionResult)PartialView("OrdersData", books.ToPagedList(page, pageSize)) :
            //   View(books.ToPagedList(page, pageSize));
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(books);
        }

        [HttpPost]
        public ActionResult Index(string id)
        {
            int page = 1;
            int pageSize = 5;

            // id - имя клиента, заказы которого необходимо выводить на странице.
            var books = db.Books.Include(b => b.Author).Include(b => b.CountryPublished).Where(n=>n.Title.StartsWith(id));

            return View("Index",books);
        }

        public ActionResult OrdersData(string id)
        {
            var data = db.Books.Include(b => b.Author).Include(b => b.CountryPublished);
            if (!string.IsNullOrEmpty(id))
            {
                // выполняем выборку по свойству Customer если значение id не пустое и не равное "All"
                data = data.Where(n => n.Title.StartsWith(id));
            }
            return PartialView(data);
        }


        // GET: Example/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Example/Create
        public ActionResult Create()
        {
            ViewBag.AuthorsId = new SelectList(db.Authors, "Id", "FullName");
            ViewBag.CountryPublishedId = new SelectList(db.CountryPublisheds, "Id", "CountryName");
            return View();
        }

        // POST: Example/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Price,Description,PagesCount,Picture,CountryPublishedId,AuthorsId")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorsId = new SelectList(db.Authors, "Id", "FullName", book.AuthorsId);
            ViewBag.CountryPublishedId = new SelectList(db.CountryPublisheds, "Id", "CountryName", book.CountryPublishedId);
            return View(book);
        }

        // GET: Example/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorsId = new SelectList(db.Authors, "Id", "FullName", book.AuthorsId);
            ViewBag.CountryPublishedId = new SelectList(db.CountryPublisheds, "Id", "CountryName", book.CountryPublishedId);
            return View(book);
        }

        // POST: Example/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Price,Description,PagesCount,Picture,CountryPublishedId,AuthorsId")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorsId = new SelectList(db.Authors, "Id", "FullName", book.AuthorsId);
            ViewBag.CountryPublishedId = new SelectList(db.CountryPublisheds, "Id", "CountryName", book.CountryPublishedId);
            return View(book);
        }

        // GET: Example/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Example/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
