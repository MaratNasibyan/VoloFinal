using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Entities.Interface;
using BookStore.Entities.AuthorViewModel;

namespace BookStore.Entities.Repositories
{
    //ImagePath-i repositorin 
    public class ImagePathRepository : IRepository<ImagePatch>
    {
        private BookDatabaseEntities db;
        public ImagePathRepository(BookDatabaseEntities context)
        {
            this.db = context;
        }

        public void Create(ImagePatch image)
        {
            db.ImagePatchs.Add(image);
        }

        public void Delete(int id)
        {
            ImagePatch image = db.ImagePatchs.Find(id);

            if(image != null)
            {
                db.ImagePatchs.Remove(image);
            }
        }
              
        public async Task<ImagePatch> GetData(int? id)
        {
            return await db.ImagePatchs.FindAsync(id);
        }

        public IEnumerable<ImagePatch> GetList()
        {
            return db.ImagePatchs;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(ImagePatch image)
        {
            db.Entry(image).State = System.Data.Entity.EntityState.Modified;
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
