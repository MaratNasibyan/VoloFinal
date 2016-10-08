using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Entities.AuthorViewModel;
namespace BookStore.Entities.CountryViewModel
{
    public class CauntryRepository : IRepository<CountryPublished>
    {
        private BookDatabaseEntities db;
        public CauntryRepository()
        {
            db = new BookDatabaseEntities();
        }
        
        public void Create(CountryPublished cauntry)
        {
            db.CountryPublisheds.Add(cauntry);
        }

        public void Delete(int id)
        {
            CountryPublished cauntry = db.CountryPublisheds.Find(id);
            if(cauntry !=null)
            {
                db.CountryPublisheds.Remove(cauntry);
            }
        }
            

        public IEnumerable<CountryPublished> GetList()
        {
            return db.CountryPublisheds;
        }

        public async Task<CountryPublished> GetData(int? id)
        {
            return await db.CountryPublisheds.FindAsync(id);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(CountryPublished cauntry)
        {
            db.Entry(cauntry).State = System.Data.Entity.EntityState.Modified;
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
