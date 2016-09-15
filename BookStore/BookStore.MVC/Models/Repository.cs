using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Entities;
using System.Data.Entity;

namespace BookStore.MVC.Models
{
    public class Repository
    {
        public static void Insert<TEntity>(TEntity entity) where TEntity : class
        {
            // Настройки контекста
            BookDatabaseEntities  context = new BookDatabaseEntities();
            context.Database.Log = (s => System.Diagnostics.Debug.WriteLine(s));

            context.Entry(entity).State = EntityState.Added;
            context.SaveChanges();
        }
    }
}