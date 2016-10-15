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
                    totalPrice = item.CountryPublished.PhoneCode + item.Price,
                    //AttributeBook = item.AttributeBooks.ToList()                                                         

            });
           }
           return result;
        }

        //Coment arac parametrery naxatesvac eyin atributy create anelu hamar
        public static Book CreateBook(BookViewModel model/*,int attrId,int valueId*/)
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
                CountryPublished = model.CountryPublished,
                //Attributi ev Value-i tablner
                //AttributeBooks = new List<AttributeBook> { new AttributeBook { BooksId=model.Id,AttributesId=attrId} },            
                //BookValues = new List<BookValue> { new BookValue { BooksId = model.Id, ValueId = valueId } }
                                          
            };
            return book;
        }
        //Post-i hamar
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

        //Get-i hamar
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
