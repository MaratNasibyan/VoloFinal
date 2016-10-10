using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.MVC.Models
{
    public class CheckBoxViewModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public bool Checked { get; set; }
    }
}