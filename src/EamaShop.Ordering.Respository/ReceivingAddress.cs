using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Ordering.Respository
{
    /// <summary>
    /// own's <see cref="Order"/>
    /// </summary>
    public sealed class ReceivingAddress
    {
        public string Country { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string Area { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public string Receiver { get; set; }

        public string ContactPhone { get; set; }
    }
}
