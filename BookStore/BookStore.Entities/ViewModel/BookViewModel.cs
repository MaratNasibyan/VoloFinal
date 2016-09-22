using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Entities.ViewModel
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int PagesCount { get; set; }
      

        public CountryPublished  CountryPublished { get; set; }
        public Author Author  { get; set; }
        public List<ImagePatch> ImagePatchs { get; set; }
    }
}
