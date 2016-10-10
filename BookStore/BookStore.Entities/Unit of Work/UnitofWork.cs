using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Entities.Repositories;
using BookStore.Entities.AuthorViewModel;
using BookStore.Entities.CountryViewModel;
namespace BookStore.Entities.Unit_of_Work
{
    //Unit Of Work
    public class UnitofWork
    {
        private BookDatabaseEntities db = new BookDatabaseEntities();

        private BookRepository bookRepository;
        private AuthorRepository authorRepository;
        private CauntryRepository countryRepository;
        private ImagePathRepository imagePathRepository;

        public AuthorRepository Authors
        {
            get
            {
                if (authorRepository == null)
                    authorRepository = new AuthorRepository(db);
                    return authorRepository;
            }
                
        }

        public CauntryRepository Countries
        {
            get
            {
                if (countryRepository == null)
                    countryRepository = new CauntryRepository(db);
                return countryRepository;
            }
        }

        public ImagePathRepository Images
        {
            get
            {
                if (imagePathRepository == null)
                    imagePathRepository = new ImagePathRepository(db);
                return imagePathRepository;
            }
        }

        public BookRepository Books
        {
            get
            {
                if (bookRepository == null)
                    bookRepository = new BookRepository(db);
                return bookRepository;
            }
        }

    }
}
