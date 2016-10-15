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
using System.IO;
using PagedList;
using BookStore.MVC.Models;
using BookStore.Entities.ViewModel;
using BookStore.Entities.Service;
using System.Web.Helpers;
using BookStore.Entities.Repositories;
using BookStore.Entities.Unit_of_Work;

namespace BookStore.MVC.Controllers
{
  
    [Authorize]
    public class AdminController : Controller
    {       
        UnitofWork db;
        public AdminController()
        {
            db = new UnitofWork();
        }
        
        // GET: Admin
        public ActionResult Index(string searchString, string sortOption, int page=1)
        {
            try
            {
                int pageSize = 5;
                var books = db.Books.Find(searchString);
                if(!books.Any())
                {
                    return PartialView("BookNot", searchString);
                }

                BooksListModel model = new BooksListModel
                {
                    BooksList = BookRelase.GetBookResult(books)
                };

                switch (sortOption)
                {
                    case "Title_ASC": model.BooksList = model.BooksList.OrderBy(n => n.Title).ToList(); break;
                    case "Title_DESC": model.BooksList = model.BooksList.OrderByDescending(n => n.Title).ToList(); break;
                    case "Price_ASC": model.BooksList = model.BooksList.OrderBy(n => n.totalPrice).ToList(); break;
                    case "Price_DESC": model.BooksList = model.BooksList.OrderByDescending(n => n.totalPrice).ToList(); break;
                    case "Author_ASC": model.BooksList = model.BooksList.OrderBy(n => n.Author.FullName).ToList(); break;
                    case "Author_DESC": model.BooksList = model.BooksList.OrderByDescending(n => n.Author.FullName).ToList(); break;
                    case "PageCount_ASC": model.BooksList = model.BooksList.OrderBy(n => n.PagesCount).ToList(); break;
                    case "PageCount_DESC": model.BooksList = model.BooksList.OrderByDescending(n => n.PagesCount).ToList(); break;
                    case "Country_ASC": model.BooksList = model.BooksList.OrderBy(n => n.CountryPublished.CountryName).ToList(); break;
                    case "Country_DESC": model.BooksList = model.BooksList.OrderByDescending(n => n.CountryPublished.CountryName).ToList(); break;
                    default: model.BooksList = model.BooksList.OrderBy(n => n.Id).ToList(); break;
                }
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
        
        // GET: Admin/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            Book book;
            BookViewModel model;
            try
            {
                if (id == null)
                {                   
                    return PartialView("IndexNotFound", id.ToString());
                }
                book = await db.Books.GetData(id);
               
                if (book == null)
                {                    
                    return PartialView("IndexNotFound", id.ToString());
                }
                model = BookRelase.DetailsBook(book);
            }
            catch
            {
                return Content("Error");
            }
            return View(model);
        }

        // GET: Admin/Create
        [Authorize]
        public ActionResult Create()
        {
            //Atributi hamar naxatesvac vor yntri atribut ev hamapatasxan arjeqy atributi,vor beruma GetItems partialviewn

            int selectedIndex = 1;
            ViewBag.Attributes = new SelectList(db.Attributes.GetList(), "Id", "Name", selectedIndex);
            ViewBag.Values = new SelectList(db.Values.GetList().Where(n => n.AttributesId == selectedIndex), "Id", "ValueText");
            

            ViewBag.AuthorsId = new SelectList(db.Authors.GetList(), "Id", "FullName");
            ViewBag.CountryPublishedId = new SelectList(db.Countries.GetList(), "Id", "CountryName"); 
                     
            return View();
        }
        public ActionResult GetItems(int id)
        {
            return PartialView(db.Values.GetList().Where(n => n.AttributesId == id).ToList());
        }       

        //POST: Admin/Create

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]      
        public  ActionResult Create([Bind(Include = "Id,Title,Price,Description,PagesCount,Picture,ImagePatchs,CountryPublishedId,AuthorsId")] BookViewModel model,HttpPostedFileBase upload/*,int Attributes, int Values*/)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (upload != null)
                    {
                        var supportedTypes = new[] { "jpg", "jpeg", "png" };
                        var fileExt = System.IO.Path.GetExtension(upload.FileName).Substring(1);

                        if (!supportedTypes.Contains(fileExt))
                        {
                            return RedirectToAction("Index");                          
                        }
                        string filename = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName);
                        string patch = Path.Combine(Server.MapPath("~/Images"), filename);                      
                        WebImage img = new WebImage(upload.InputStream);
                        if (img.Width > 270)
                            img.Resize(260, 400);
                        img.Save(patch);
                        
                        model.ImagePatchs = new List<ImagePatch>() { new ImagePatch { ImageUrl = filename } };
                    }
                    else
                    {
                        model.ImagePatchs = new List<ImagePatch>() { new ImagePatch { ImageUrl = "No.jpg" } };
                    }
                    var v = BookRelase.CreateBook(model/*,Attributes,Values*/);
                
                    db.Books.Create(v);
                    db.Books.Save();                  
                    return RedirectToAction("Index");
                }
                int selectedIndex = 1;
                SelectList attribute = new SelectList(db.Attributes.GetList(), "Id", "Name", selectedIndex);
                ViewBag.Attributes = attribute;

                SelectList values = new SelectList(db.Values.GetList().Where(n => n.AttributesId == selectedIndex), "Id", "ValueText");
                ViewBag.Values = values;
                ViewBag.AuthorsId = new SelectList(db.Authors.GetList(), "Id", "FullName", model.AuthorsId);
                ViewBag.CountryPublishedId = new SelectList(db.Countries.GetList(), "Id", "CountryName", model.CountryPublishedId);
              
                return View(model);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/Edit/5
        [Authorize]       
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {               
                    return PartialView("IndexNotFound", id.ToString());
                }
                Book book = await db.Books.GetData(id);
                if (book == null)
                {                  
                   return PartialView("IndexNotFound", id.ToString());
                }
                var modelBook = BookRelase.EditBook(book);
                ViewBag.AuthorsId = new SelectList(db.Authors.GetList(), "Id", "FullName", book.AuthorsId);
                ViewBag.CountryPublishedId = new SelectList(db.Countries.GetList(), "Id", "CountryName", book.CountryPublishedId);             

                return View(modelBook);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
      
        // POST: Admin/Edit/5      
        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]       
        public ActionResult Edit([Bind(Include = "Id,Title,Price,Description,PagesCount,Picture,ImagePatchs,CountryPublishedId,AuthorsId")] BookViewModel model,HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {                   
                    ImagePatch oldpatch = db.Images.GetList().Where(n => n.BooksId == model.Id).FirstOrDefault();
                    string pat = oldpatch.ImageUrl;
                    if (upload != null)
                    {
                        var supportedTypes = new[] { "jpg", "jpeg", "png" };
                        var fileExt = System.IO.Path.GetExtension(upload.FileName).Substring(1);

                        if (!supportedTypes.Contains(fileExt))
                        {
                            return RedirectToAction("Index");                           
                        }
                        string filename = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName);
                        string patch = Path.Combine(Server.MapPath("~/Images"), filename);
                        upload.SaveAs(patch);
               
                        ImagePatch patchimg = db.Images.GetList().Where(n => n.BooksId == model.Id).FirstOrDefault();
                        if (patchimg != null)
                        {
                            patchimg.ImageUrl = filename;

                            string patch1 = Path.Combine(Server.MapPath("~/Images"), pat);

                            if (System.IO.File.Exists(patch1))
                            {
                                System.IO.File.Delete(patch1);
                            }
                        }              

                    }                                       
                    Book b = BookRelase.EditBook(model);                  
                    db.Books.Update(b);
                    db.Books.Save();
                    return RedirectToAction("Index");
                }
                ViewBag.AuthorsId = new SelectList(db.Authors.GetList(), "Id", "FullName", model.AuthorsId);
                ViewBag.CountryPublishedId = new SelectList(db.Countries.GetList(), "Id", "CountryName", model.CountryPublishedId);

                //Atributneri hamar er naxatesvac

                //int selectedIndex = 1;
                //SelectList attribute = new SelectList(db.Attributes.GetList(), "Id", "Name", selectedIndex);
                //ViewBag.Attributes = attribute;

                //SelectList values = new SelectList(db.Values.GetList().Where(n => n.AttributesId == selectedIndex), "Id", "ValueText");
                //ViewBag.Values = values;

                return View(model);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {                   
                    return PartialView("IndexNotFound", id.ToString());
                }                              
                Book book = await db.Books.GetData(id);                
                if (book == null)
                {                
                    return PartialView("IndexNotFound", id.ToString());
                }
                return View(book);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BookDatabaseEntities e = new BookDatabaseEntities(); 
            try
            {             
                Book book = await db.Books.GetData(id);
                ImagePatch patch = db.Images.GetList().Where(n => n.BooksId == id).FirstOrDefault();
                string patch1 = Path.Combine(Server.MapPath("~/Images"), patch.ImageUrl);
                if (patch1 != null && patch.ImageUrl != "No.jpg")
                {
                    if (System.IO.File.Exists(patch1))
                    {
                        System.IO.File.Delete(patch1);
                    }
                }
                int k = Convert.ToInt32(patch.BooksId);
         
                db.Books.Delete(id);
                db.Images.Delete(k);
                db.Books.Save();             
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
                db.Books.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
