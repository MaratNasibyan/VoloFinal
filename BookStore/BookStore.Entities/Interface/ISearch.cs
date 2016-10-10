using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entities.Interface
{
    //ISearch-y ogtagorcvum e BookControllerum ,Author ev Country-i mej search chem ogtagorcum sa arandzin greci Book-i hamar!!!
    public interface ISearch<T> where T:class
    {
        IEnumerable<T> Find(string searchString);
    }
}
