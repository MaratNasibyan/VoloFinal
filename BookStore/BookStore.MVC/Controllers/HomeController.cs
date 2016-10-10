using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Entities.ViewModel;
using BookStore.Entities.Service;
using BookStore.Entities;
using BookStore.Entities.Unit_of_Work;
namespace BookStore.MVC.Controllers
{
    public class HomeController : Controller
    {        
        UnitofWork db;
        public HomeController()
        {
            db = new UnitofWork();
        }
        public ActionResult Index()
        {            
                var books = db.Books.GetList();
                BooksListModel model = new BooksListModel
                {
                    BooksList = BookRelase.GetBookResult(books)
                };
             Random rnd = new Random();
             return View(model.BooksList.OrderBy(n=>rnd.Next()).Take(5));
        }
     
    }
}