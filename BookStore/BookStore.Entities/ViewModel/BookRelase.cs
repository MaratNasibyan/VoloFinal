using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BookStore.Entities.ViewModel;
using System.Data.Entity;

namespace BookStore.Entities.Service
{
    public class BookRelase 
    {
        
        public static List<BookViewModel> GetBookResult(IEnumerable<Book> books)
        {
            //List<Book> books  = db.Books.Include(b=>b.Author).Include(b => b.CountryPublished).ToList();
            var result = new List<BookViewModel>();
            foreach (var item in books)
            {
                result.Add(new BookViewModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    PagesCount = Convert.ToInt32(item.PagesCount),
                    Description = item.Description,
                    Price = item.Price,
                    Author = item.Author,
                    CountryPublished = item.CountryPublished,
                    ImagePatchs = item.ImagePatchs.ToList()                    
                                                        
                });
            }
           return result;
        }

    }
}
