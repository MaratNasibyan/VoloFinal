using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Entities.AuthorViewModel;

namespace BookStore.Entities.Repositories
{
    public  class AttrRepository:IRepository<Entities.Attribute>
    {
        private BookDatabaseEntities db;

        public AttrRepository(BookDatabaseEntities context)
        {
            this.db = context;
        }

        public void Create(Attribute attribute)
        {
            db.Attributes.Add(attribute);
        }

        public void Delete(int id)
        {
            Attribute attr = db.Attributes.Find(id);
            if(attr != null)
            {
                db.Attributes.Remove(attr);
            }
        }     

        public async Task<Attribute> GetData(int? id)
        {
            return await db.Attributes.FindAsync(id);
        }

        public IEnumerable<Attribute> GetList()
        {
            return db.Attributes;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Attribute attr)
        {
            db.Entry(attr).State = System.Data.Entity.EntityState.Modified;

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
