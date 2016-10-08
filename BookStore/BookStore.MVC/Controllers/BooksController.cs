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
using PagedList;
using BookStore.Entities.ViewModel;
using BookStore.Entities.Service;

namespace BookStore.MVC.Controllers
{
    public class BooksController : Controller
    {
        private BookDatabaseEntities db = new BookDatabaseEntities();

        // GET: Books
      
              
        public ActionResult Index(string searchString,  int page = 1)
        {
            try
            {
                var books = db.Books.AsQueryable();
                int pageSize = 10;
                if (!string.IsNullOrEmpty(searchString))
                {
                    books = (db.Books.Include(b => b.Author).Include(b => b.CountryPublished).Where(n => n.Title.Contains(searchString) || n.Author.FullName.Contains(searchString)));
                    if (!books.Any())
                    {
                        return PartialView("SearchViewNotFound", searchString);
                    }
                }
                BooksListModel model = new BooksListModel
                {
                    BooksList = BookRelase.GetBookResult(books)
                };

                if (page > model.BooksList.ToPagedList(page, pageSize).PageCount)
                {
                    return RedirectToAction("Index");
                }
                return Request.IsAjaxRequest() ? (ActionResult)PartialView("IndexPartial", model.BooksList.ToPagedList(page, pageSize)) :
                    View(model.BooksList.ToPagedList(page, pageSize));
            }
            catch
            {
               return RedirectToAction("Index");
            }
        }
    

        // GET: Books/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            BookViewModel model;      
            try
            {
                Book book = await db.Books.FindAsync(id);
                if (id == null)
                {
                    //return View(new HttpStatusCodeResult(HttpStatusCode.BadRequest));
                    return PartialView("ViewPartial", id.ToString());
                }
                if (book == null)
                {
                    //return HttpNotFound();
                    return PartialView("ViewPartial", id.ToString());
                }
                model = BookRelase.DetailsBook(book);
                return View(model);
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
