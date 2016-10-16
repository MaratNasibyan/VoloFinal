using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BookStore.Entities.ViewModel
{
    public class BookViewModel
    {
        //Book View Model
        public int Id { get; set; }
        [Display(Name ="Image")]
        public string Image { get; set; }
        public decimal totalPrice { get; set; }   
        [Required(ErrorMessage = "The field Title must be filled")]
        [StringLength(50)]
        [DisplayName("Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The price Title must be filled")]
        [DisplayName("Price")]
        public decimal Price { get; set; }
       
        public string Description { get; set; }
        [DisplayName("Pages")]
        public int PagesCount { get; set; }
        public DateTime DateCreated { get; set; }
        public byte[] Picture { get; set; }


        [DisplayName("Country")]
        [Required(ErrorMessage = "The field Country must be filled")]
        public int CountryPublishedId { get; set; }

        [Required(ErrorMessage = "The field Author must be filled")]
        [DisplayName("Author")]
        public int AuthorsId { get; set; }

        //Atributneri hamar eyi naxatesel bayc chkaraca many to many kapi mijocov et konkret book-i arjeq tam , kam stanam!
        //public int Attribute { get; set; }
        //public int AttributeValue { get; set; }

        public CountryPublished  CountryPublished { get; set; }
                
        public Author Author  { get; set; }
        [DisplayName("Picture")]  
        public List<ImagePatch> ImagePatchs { get; set; }
        public List<AttributeBook> AttributeBook { get; set; } 
        
        public List<BookValue> BookValue { get; set; }    
        
            
    }      
      
}
