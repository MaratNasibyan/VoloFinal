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

namespace BookStore.MVC.Controllers
{
    [HandleError]
    public class AdminController : Controller
    {
        private BookDatabaseEntities db = new BookDatabaseEntities();

       // GET: Admin
        public ActionResult Index(string searchString, string sortOption, int page = 1)
        {
            int pageSize = 5;            
            var books = db.Books.ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                books = (db.Books.Include(b => b.Author).Include(b => b.CountryPublished).Where(n => n.Title.StartsWith(searchString) || n.Author.FullName.StartsWith(searchString))).ToList();
                //if (!books.Any())
                //{
                //    return Content("This book is not found");
                //}

            }
            BooksListModel model = new BooksListModel
            {
                BooksList = BookRelase.GetBookResult(books)
            };
                       
            switch(sortOption)
            {
                case "Title_ASC": model.BooksList = model.BooksList.OrderBy(n => n.Title).ToList(); break;
                case "Title_DESC":model.BooksList = model.BooksList.OrderByDescending(n => n.Title).ToList();break;                
                case "Price_ASC": model.BooksList= model.BooksList.OrderBy(n => n.Price).ToList();break;
                case "Price_DESC": model.BooksList = model.BooksList.OrderByDescending(n => n.Price).ToList(); break;
                case "Author_ASC":model.BooksList = model.BooksList.OrderBy(n => n.Author.FullName).ToList();break;
                case "Author_DESC":model.BooksList = model.BooksList.OrderByDescending(n => n.Author.FullName).ToList();break;
                case "PageCount_ASC":model.BooksList = model.BooksList.OrderBy(n => n.PagesCount).ToList();break;
                case "PageCount_DESC":model.BooksList = model.BooksList.OrderByDescending(n => n.PagesCount).ToList();break;
                case "Country_ASC":model.BooksList = model.BooksList.OrderBy(n => n.CountryPublished.CountryName).ToList();break;
                case "Country_DESC":model.BooksList = model.BooksList.OrderByDescending(n => n.CountryPublished.CountryName).ToList();break;
                default:model.BooksList = model.BooksList.OrderBy(n => n.Id).ToList();break;
            }
          
            return Request.IsAjaxRequest() ? (ActionResult)PartialView("IndexPartial",model.BooksList.ToPagedList(page, pageSize)) :
               View(model.BooksList.ToPagedList(page, pageSize));

        }


        // GET: Admin/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            Book book = await db.Books.FindAsync(id);

            try
            {
                if (id == null)
                {
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    return PartialView("IndexNotFound", id.ToString());
                }
                if (book == null)
                {
                    //return HttpNotFound();
                    return PartialView("IndexNotFound", id.ToString());
                }
            }
            catch
            {
                return Content("Error");
            }
            return View(book);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            ViewBag.AuthorsId = new SelectList(db.Authors, "Id", "FullName");
            ViewBag.CountryPublishedId = new SelectList(db.CountryPublisheds, "Id", "CountryName");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Price,Description,PagesCount,Picture,CountryPublishedId,AuthorsId")] Book book,HttpPostedFileBase upload)
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
                        // ModelState.AddModelError("photo", "Invalid type. Only the following types (jpg, jpeg, png) are supported.");

                    }

                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName);
                    string patch = Path.Combine(Server.MapPath("~/Images"), filename);
                    upload.SaveAs(patch);

                    book.ImagePatchs = new List<ImagePatch>() { new ImagePatch { ImageUrl = filename } };
                }
                else
                {
                    //string filenamenoimage = Path.Combine(Server.MapPath("~/Images/No.jpg"));
                    //System.IO.File.Copy()
                    book.ImagePatchs = new List<ImagePatch>() { new ImagePatch { ImageUrl = "No.jpg" } };
                }

                    db.Books.Add(book);
                    await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            ViewBag.AuthorsId = new SelectList(db.Authors, "Id", "FullName", book.AuthorsId);
            ViewBag.CountryPublishedId = new SelectList(db.CountryPublisheds, "Id", "CountryName", book.CountryPublishedId);
            return View(book);
        }

        // GET: Admin/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return PartialView("IndexNotFound", id.ToString());
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                //return HttpNotFound();
                return PartialView("IndexNotFound", id.ToString());
            }
            ViewBag.AuthorsId = new SelectList(db.Authors, "Id", "FullName", book.AuthorsId);
            ViewBag.CountryPublishedId = new SelectList(db.CountryPublisheds, "Id", "CountryName", book.CountryPublishedId);
            return View(book);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Price,Description,PagesCount,Picture,CountryPublishedId,AuthorsId")] Book book,HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                //book = await db.Books.FindAsync(book.Id); 
                ImagePatch oldpatch = db.ImagePatchs.Where(n => n.BooksId == book.Id).FirstOrDefault();
                string pat = oldpatch.ImageUrl;
                if (upload != null)
                {
                    var supportedTypes = new[] { "jpg", "jpeg", "png" };
                    var fileExt = System.IO.Path.GetExtension(upload.FileName).Substring(1);

                    if (!supportedTypes.Contains(fileExt))
                    {
                        return RedirectToAction("Index");
                        // ModelState.AddModelError("photo", "Invalid type. Only the following types (jpg, jpeg, png) are supported.");                        
                    }

                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName);
                    string patch = Path.Combine(Server.MapPath("~/Images"), filename);
                    upload.SaveAs(patch);
                    ImagePatch patchimg = db.ImagePatchs.Where(n => n.BooksId == book.Id).SingleOrDefault();
                    if(patchimg != null)
                    {
                        patchimg.ImageUrl = filename;


                        string patch1 = Path.Combine(Server.MapPath("~/Images"), pat);

                        if (System.IO.File.Exists(patch1))
                        {
                            System.IO.File.Delete(patch1);
                        }
                    }

                    //book.ImagePatchs = new List<ImagePatch>() { new ImagePatch { ImageUrl = upload.FileName } };

                }
                db.Entry(book).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorsId = new SelectList(db.Authors, "Id", "FullName", book.AuthorsId);
            ViewBag.CountryPublishedId = new SelectList(db.CountryPublisheds, "Id", "CountryName", book.CountryPublishedId);
            return View(book);
        }

        // GET: Admin/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return PartialView("IndexNotFound", id.ToString());

            }
            Book book = await db.Books.FindAsync(id);
          
            
            if (book == null)
            {
                //return HttpNotFound();
                return PartialView("IndexNotFound", id.ToString());
            }
            return View(book);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Book book = await db.Books.FindAsync(id);
            ImagePatch patch = db.ImagePatchs.Where(n => n.BooksId == id).FirstOrDefault();
            string patch1 = Path.Combine(Server.MapPath("~/Images"), patch.ImageUrl);

            if (patch1 != null && patch.ImageUrl != "No.jpg")
            {
                if (System.IO.File.Exists(patch1))
                {
                    System.IO.File.Delete(patch1);
                }
            }
            db.Books.Remove(book);
            db.ImagePatchs.Remove(patch);
            await db.SaveChangesAsync();
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
