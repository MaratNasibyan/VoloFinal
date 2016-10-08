using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Entities;

namespace BookStore.Entities.AuthorViewModel
{
    public  class AuthorRepository:IRepository<Author>
    {
        private BookDatabaseEntities db;

        public AuthorRepository()
        {
            db = new BookDatabaseEntities();
        }

        public IEnumerable<Author> GetList()
        {
            return db.Authors;
        }

        public async Task<Author> GetData(int? id)
        {
            return await  db.Authors.FindAsync(id);
        }

        public void Create(Author author)
        {
            db.Authors.Add(author);
        }

        public void Update(Author author)
        {
            db.Entry(author).State = System.Data.Entity.EntityState.Modified; 
        }

        public void Delete(int id)
        {
            Author author = db.Authors.Find(id);

            if(author !=null)
            {
                db.Authors.Remove(author);
            }
        }

        public void Save()
        {
            db.SaveChanges();
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
    }
}

