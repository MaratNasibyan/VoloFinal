using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Entities.AuthorViewModel;
using System.Data.Entity;
using BookStore.Entities.Interface;
namespace BookStore.Entities.Repositories
{
    //Search-y bookum ogtagorcelu hamar Impliment em anum nayem ISearch interface-y
    public class BookRepository : IRepository<Book>,ISearch<Book>
    {
        //Book Repositoryi nkaragrutyun
        private BookDatabaseEntities db;

        public BookRepository(BookDatabaseEntities context)
        {
            this.db = context;
        }

        public void Create(Book book)
        {
            db.Books.Add(book);
        }

        public void Delete(int id)
        {
            Book book = db.Books.Find(id);
            if(book!=null)
            {
                db.Books.Remove(book);
            }
        }
               
        public async Task<Book> GetData(int? id)
        {
            return await db.Books.FindAsync(id);
        }

        public IEnumerable<Book> GetList()
        {
            return db.Books;
        }
      
        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Book book)
        {
            db.Entry(book).State = System.Data.Entity.EntityState.Modified; 
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Book> Find(string searchString)
        {
            var books = db.Books.AsQueryable();
            if(searchString != null)
            {
               books = (db.Books.Include(b => b.Author).Include(b => b.CountryPublished).Where(n => n.Title.Contains(searchString) || n.Author.FullName.Contains(searchString)));
            }

            return books;
        }
    }
}
