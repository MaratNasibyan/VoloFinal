﻿using System;
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
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateBirth { get; set; }
    }
}
