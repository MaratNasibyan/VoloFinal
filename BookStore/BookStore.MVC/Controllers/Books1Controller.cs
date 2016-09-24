using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Entities;
using PagedList;
using BookStore.Entities.Service;
using BookStore.Entities.ViewModel;
namespace BookStore.MVC.Controllers
{
    public class Books1Controller : Controller
    {
        private BookDatabaseEntities db = new BookDatabaseEntities();

        // GET: Books1
        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var books = db.Books.Include(b => b.Author).Include(b => b.CountryPublished).ToList();

            var list = new BookStore.Entities.ViewModel.BooksListModel();
            BooksListModel arr = new BooksListModel()
            {
                BooksList = BookRelase.GetBookResult(books)
            };
          

            return View(arr.BooksList.ToPagedList(pageNumber,pageSize));
        }

        // GET: Books1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return PartialView("ErrorPartial");
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books1/Create
        public ActionResult Create()
        {
            ViewBag.AuthorsId = new SelectList(db.Authors, "Id", "FullName");
            ViewBag.CountryPublishedId = new SelectList(db.CountryPublisheds, "Id", "CountryName");
            return View();
        }

        // POST: Books1/Create
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

        // GET: Books1/Edit/5
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

        // POST: Books1/Edit/5
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

        // GET: Books1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return PartialView("ErrorPartial")/*new HttpStatusCodeResult(HttpStatusCode.BadRequest)*/;
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                //return HttpNotFound();
                return PartialView("ErrorPartial");
            }
            return View(book);
        }

        // POST: Books1/Delete/5
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
