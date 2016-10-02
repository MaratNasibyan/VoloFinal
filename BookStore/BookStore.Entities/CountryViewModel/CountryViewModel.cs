using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace BookStore.Entities.CountryViewModel
{
    public class CountryViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The field Country must be filled")]
        [StringLength(50)]
        public string CountryName { get; set; }
        [Required(ErrorMessage = "The field IsoCode must be filled")]
        [StringLength(3)]
        public string IsoCode { get; set; }
        //[Required(ErrorMessage = "The field Phonecode must be filled")]
        //[StringLength(8)]
        public int PhoneCode { get; set; }
    }
}
