using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entities.AuthorViewModel
{
    //Author ViewModeli Implimenty
    public  class AuthorRelase
    {
        public static List<AuthorViewModel> GetAuthorsResult(IEnumerable<Author> Authors)
        {
            var result = new List<AuthorViewModel>();
            foreach(var item in Authors)
            {
                result.Add(new AuthorViewModel
                {
                    Id = item.Id,
                    FullName = item.FullName,
                    DateBirth = item.DateBirth

                });                                       
            }

            return result;
        }

        public static Author CreateAuthor(AuthorViewModel model)
        {
            Author author = new Author
            {
                Id = model.Id,
                FullName = model.FullName,
                DateBirth = model.DateBirth
            };

            return author;
        }

        public static Author EditAuthor(AuthorViewModel model)
        {
            Author author = new Author
            {
                Id = model.Id,
                FullName = model.FullName,
                DateBirth = model.DateBirth
            };

            return author;
        }

        public static AuthorViewModel EditAuthor(Author author)
        {
            var model = new AuthorViewModel
            {
                Id= author.Id,
                FullName = author.FullName,
                DateBirth = author.DateBirth
            };
            return model;
        }

        public static AuthorViewModel DetailsAuthor(Author author)
        {
            var model = new AuthorViewModel
            {
                Id = author.Id,
                FullName = author.FullName,
                DateBirth = author.DateBirth
            };
            return model;
        }
    }
}
