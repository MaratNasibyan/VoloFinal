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
        //Book ViewModeli Impliment-y
        public static List<BookViewModel> GetBookResult(IEnumerable<Book> books)
        {    
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
                    CountryPublishedId = item.CountryPublishedId,
                    AuthorsId = item.AuthorsId,
                    Author = item.Author,
                    CountryPublished = item.CountryPublished,
                    ImagePatchs = item.ImagePatchs.ToList(),
                    totalPrice = item.CountryPublished.PhoneCode+item.Price                   
                                                        
                });
            }
           return result;
        }

        public static Book CreateBook(BookViewModel model)
        {
            Book book = new Book
            {
                Id = model.Id,
                Title = model.Title,
                PagesCount = model.PagesCount,
                Description = model.Description,
                Price = model.Price,
                ImagePatchs = model.ImagePatchs.ToList(),
                AuthorsId = model.AuthorsId,
                CountryPublishedId = model.CountryPublishedId,
                Picture = model.Picture,
                Author = model.Author,
                CountryPublished = model.CountryPublished
            };
            return book;
        }

        public static Book EditBook(BookViewModel model)
        {
            Book book = new Book
            {
                Id = model.Id,
                Title = model.Title,
                PagesCount = model.PagesCount,
                Description = model.Description,
                Price = model.Price,   
                AuthorsId = model.AuthorsId,
                CountryPublishedId = model.CountryPublishedId,
                Picture = model.Picture, 
                ImagePatchs = model.ImagePatchs
                

            };
            return book;
        }

        public static BookViewModel EditBook(Book item)
        {
            var model = new BookViewModel
            {
                Id = item.Id,
                Title = item.Title,
                PagesCount = Convert.ToInt32(item.PagesCount),
                Description = item.Description,
                Price = item.Price,
                CountryPublishedId = item.CountryPublishedId,
                AuthorsId = item.AuthorsId,
                Author = item.Author,
                CountryPublished = item.CountryPublished,
                ImagePatchs = item.ImagePatchs.ToList()
            };
            return model;
        }

        public static BookViewModel DetailsBook(Book item)
        {
            var model = new BookViewModel
            {
                Id = item.Id,
                Title = item.Title,
                PagesCount = Convert.ToInt32(item.PagesCount),
                Description = item.Description,
                Price = item.Price,
                CountryPublishedId = item.CountryPublishedId,
                AuthorsId = item.AuthorsId,
                Author = item.Author,
                CountryPublished = item.CountryPublished,
                ImagePatchs = item.ImagePatchs.ToList()
            };
            return model;
        }

    }
}
