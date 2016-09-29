using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Entities.ViewModel;
using BookStore.Entities.Service;
using BookStore.Entities;

namespace BookStore.MVC.Controllers
{
    public class HomeController : Controller
    {
        private BookDatabaseEntities db = new BookDatabaseEntities();
        public ActionResult Index()
        {            
                var books = db.Books.ToList();
                BooksListModel model = new BooksListModel
                {
                    BooksList = BookRelase.GetBookResult(books)
                };
             Random rnd = new Random();
             return View(model.BooksList.OrderBy(n=>rnd.Next()).Take(5));
        }
     
    }
}