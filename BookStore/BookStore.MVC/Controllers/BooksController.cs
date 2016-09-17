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
namespace BookStore.MVC.Controllers
{
    public class BooksController : Controller
    {
        private BookDatabaseEntities db = new BookDatabaseEntities();

        // GET: Books
      
              
        public ActionResult Index(string searchString,  int page = 1)
        {
            int pageSize = 5;
            //int pageNumber = 1;

            var books = db.Books.ToList();
            
            if (!string.IsNullOrEmpty(searchString))
            {
                books = (db.Books.Include(b => b.Author).Include(b => b.CountryPublished).Where(n=>n.Title.StartsWith(searchString) || n.Author.FullName.StartsWith(searchString))).ToList();       
                if(books == null)
                {
                    return RedirectToAction("View");
                }         
            }
       
            return Request.IsAjaxRequest() ? (ActionResult)PartialView("Data1", books.ToPagedList(page, pageSize)) :
                View(books.ToPagedList(page, pageSize));
        
        }
    

        //public ActionResult Data(string Search)
        //{
        //    var books = db.Books.Include(b => b.Author).Include(b => b.CountryPublished).ToList();
        //    if (Search != null)
        //    {
        //        books.Where(n => n.Title.StartsWith(Search)).ToList();
        //    }

        //    return PartialView("Data",books);
        //}


        // GET: Books/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
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
