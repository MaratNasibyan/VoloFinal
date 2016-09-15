using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.MVC.Models
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateBirth { get; set; }
    }
}