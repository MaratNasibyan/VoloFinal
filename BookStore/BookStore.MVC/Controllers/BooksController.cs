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
            int pageSize = 5;
           
            var books = db.Books.ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                books = (db.Books.Include(b => b.Author).Include(b => b.CountryPublished).Where(n=>n.Title.Contains(searchString) || n.Author.FullName.Contains(searchString))).ToList();       
                if(!books.Any())
                {
                    return PartialView("ViewPartial",searchString);
                }
             }

            BooksListModel model = new BooksListModel
            {
                BooksList = BookRelase.GetBookResult(books)
            };

            if (page > model.BooksList.ToPagedList(page, pageSize).PageCount)
            {
                page = 1;
            }

            return Request.IsAjaxRequest() ? (ActionResult)PartialView("IndexPartial", model.BooksList.ToPagedList(page, pageSize)) :
                View(model.BooksList.ToPagedList(page, pageSize));
        
        }
    


        // GET: Books/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                //return View(new HttpStatusCodeResult(HttpStatusCode.BadRequest));
                return PartialView("ViewPartial", id.ToString());
            }

            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                //return HttpNotFound();
                return PartialView("ViewPartial", id.ToString());

            }
            return View(book);
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
