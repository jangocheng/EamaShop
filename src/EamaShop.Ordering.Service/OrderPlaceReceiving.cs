using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Ordering.Service
{
    public class OrderPlaceReceiving
    {
        public OrderPlaceReceiving(string country, string city, string province, string area, string street, string houseNumber, string receiver, string contactPhone)
        {
            Country = country ?? throw new ArgumentNullException(nameof(country));
            City = city ?? throw new ArgumentNullException(nameof(city));
            Province = province ?? throw new ArgumentNullException(nameof(province));
            Area = area ?? throw new ArgumentNullException(nameof(area));
            Street = street ?? throw new ArgumentNullException(nameof(street));
            HouseNumber = houseNumber ?? throw new ArgumentNullException(nameof(houseNumber));
            Receiver = receiver ?? throw new ArgumentNullException(nameof(receiver));
            ContactPhone = contactPhone ?? throw new ArgumentNullException(nameof(contactPhone));
        }

        public string Country { get; }

        public string City { get; }

        public string Province { get; }

        public string Area { get; }

        public string Street { get; }

        public string HouseNumber { get; }

        public string Receiver { get; }

        public string ContactPhone { get; }
    }
}
