

using Contact.Domain.Exceptions;
using System.Collections.Generic;
using DomainBase;
using System;

namespace Contact.Domain.ValueObjects
{
    public class Address: ValueObject
    {
        public string HouseNameNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string CountryIsoAlpha3 { get; set; }

        public Address(string houseNameNumber, string street, string city, string postcode, string countryIsoAlpha3)
        {
            if (string.IsNullOrEmpty(countryIsoAlpha3) || countryIsoAlpha3.Length !=3) throw new ArgumentException($"the country ISO Alpah must be a valid code");
            HouseNameNumber = houseNameNumber;
            Street = street;
            City = city;
            Postcode = postcode;
            CountryIsoAlpha3 = countryIsoAlpha3;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return HouseNameNumber;
            yield return Street;
            yield return City;
            yield return Postcode;
            yield return CountryIsoAlpha3;
        }
    }
}
