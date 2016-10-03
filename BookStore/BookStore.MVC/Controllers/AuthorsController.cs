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
using BookStore.Entities.AuthorViewModel;

namespace BookStore.MVC.Controllers
{
    public class AuthorsController : Controller
    {
        private BookDatabaseEntities db = new BookDatabaseEntities();

        // GET: Authors
        [Authorize]
        public  ActionResult Index()
        {
            try
            {
                var authors = db.Authors.ToList();
                AuthorListModel model = new AuthorListModel
                {
                    AuthorsList =  AuthorRelase.GetAuthorsResult(authors)
                };
           
                return View(model.AuthorsList);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Authors/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            Author author;
            AuthorViewModel model;
               
            try
            {
                if (id == null)
                {
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    return PartialView("PartialNotFoundView", id.ToString());
                }
                author = await db.Authors.FindAsync(id);
                if (author == null)
                {
                    //return HttpNotFound();
                    return PartialView("PartialNotFoundView", id.ToString());
                }
                model = AuthorRelase.DetailsAuthor(author);
                return View(model);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FullName,DateBirth")] AuthorViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var author = AuthorRelase.CreateAuthor(model);
                    db.Authors.Add(author);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Authors/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    return PartialView("PartialNotFoundView", id.ToString());
                }
                Author author = await db.Authors.FindAsync(id);
                if (author == null)
                {
                    //return HttpNotFound();
                    return PartialView("PartialNotFoundView", id.ToString());

                }
                var model = AuthorRelase.EditAuthor(author);
                return View(model);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FullName,DateBirth")] AuthorViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var author = AuthorRelase.EditAuthor(model);
                    db.Entry(author).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Authors/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    return PartialView("PartialNotFoundView", id.ToString());

                }
                Author author = await db.Authors.FindAsync(id);
                if (author == null)
                {
                    //return HttpNotFound();
                    return PartialView("PartialNotFoundView", id.ToString());
                }
                return View(author);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Authors/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Author author = await db.Authors.FindAsync(id);
                db.Authors.Remove(author);
                await db.SaveChangesAsync();
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
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
