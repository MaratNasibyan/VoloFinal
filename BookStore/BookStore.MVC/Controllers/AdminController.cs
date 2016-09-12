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
namespace BookStore.MVC.Controllers
{
    public class AdminController : Controller
    {
        private BookDatabaseEntities db = new BookDatabaseEntities();

        // GET: Admin
        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var books = db.Books.Include(b => b.Author).Include(b => b.CountryPublished).ToList();

            return View(books.ToPagedList(pageNumber,pageSize));
          
        }

        public JsonResult GetRecords()
        {
            var books = db.Books.Include(b => b.Author).Include(b => b.CountryPublished).ToList();
            return new JsonResult { Data = books, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // GET: Admin/Details/5
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
                    book.ImagePatchs = new List<ImagePatch>() { new ImagePatch { ImageUrl = "No.jpg" } };

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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
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
                if(upload != null)
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
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
