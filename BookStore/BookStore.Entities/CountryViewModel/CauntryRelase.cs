using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entities.CountryViewModel
{
    public class CauntryRelase
    {
        public static List<CountryViewModel> GetCountryResult(IEnumerable<CountryPublished> Countryies)
        {
            var result = new List<CountryViewModel>();
            foreach(var item in Countryies)
            {
                result.Add(new CountryViewModel
                {
                    Id = item.Id,
                    CountryName = item.CountryName,
                    IsoCode = item.IsoCode,
                    PhoneCode = item.PhoneCode
                });
            }

            return result;
        }

        public static CountryViewModel DetailsCountry(CountryPublished country)
        {
            var model = new CountryViewModel
            {
                Id = country.Id,
                CountryName = country.CountryName,
                IsoCode = country.IsoCode,
                PhoneCode = country.PhoneCode
            };
            return model;
        }

        public static CountryPublished CreateCountry(CountryViewModel model)
        {
            CountryPublished country = new CountryPublished
            {
                Id = model.Id,
                CountryName = model.CountryName,
                PhoneCode = model.PhoneCode,
                IsoCode = model.IsoCode,
                
            };

            return country;
        }

        public static CountryPublished EditCountry(CountryViewModel model)
        {
            CountryPublished country = new CountryPublished
            {
                Id = model.Id,
                CountryName = model.CountryName,
                PhoneCode = model.PhoneCode,
                IsoCode = model.IsoCode,

            };

            return country;
        }

        public static CountryViewModel EditCountry(CountryPublished country)
        {
            var model = new CountryViewModel
            {
                Id = country.Id,
                CountryName = country.CountryName,
                PhoneCode = country.PhoneCode,
                IsoCode = country.IsoCode,
            };

            return model;
        }
    }
}
