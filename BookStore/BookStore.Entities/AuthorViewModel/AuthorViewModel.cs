using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace BookStore.Entities.AuthorViewModel
{
    //Authori ViewModel-y
    public class AuthorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Author name")]
        //[DisplayName("Author")]
        public string FullName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateBirth { get; set; }
    }
}
