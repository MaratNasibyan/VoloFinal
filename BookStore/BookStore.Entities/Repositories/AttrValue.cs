using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Entities.AuthorViewModel;

namespace BookStore.Entities.Repositories
{
    public class AttrValueRepository:IRepository<ValueAtt>
    {
        private BookDatabaseEntities db;

        public AttrValueRepository(BookDatabaseEntities context)
        {
            this.db = context;
        }

        public void Create(ValueAtt value)
        {
            db.ValueAtts.Add(value);
        }

        public void Delete(int id)
        {
            ValueAtt value = db.ValueAtts.Find(id);

            if(value != null)
            {
                db.ValueAtts.Remove(value);
            }
        }

        public async Task<ValueAtt> GetData(int? id)
        {
            return await db.ValueAtts.FindAsync(id);
        }

        public IEnumerable<ValueAtt> GetList()
        {
            return db.ValueAtts;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(ValueAtt value)
        {
            db.Entry(value).State = System.Data.Entity.EntityState.Modified;
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
