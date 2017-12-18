using EamaShop.Ordering.Respository;
using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Ordering.Service
{
    public class ReceivingAddressValueObject : IEquatable<ReceivingAddressValueObject>
    {
        internal ReceivingAddressValueObject(ReceivingAddress address)
            : this(ArgumentUtilities.NotNull(address, nameof(address)).City,
                  address.Province,
                  address.Area, 
                  address.Street, 
                  address.HouseNumber,
                  address.Receiver,
                  address.ContactPhone, 
                  address.Country)
        {
        }

        public ReceivingAddressValueObject(string city,
            string province,
            string area,
            string street,
            string houseNumber,
            string receiver,
            string contactPhone,
            string country = "中国")
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

        public override bool Equals(object obj)
        {
            return Equals(obj as ReceivingAddressValueObject);
        }

        public bool Equals(ReceivingAddressValueObject other)
        {
            return other != null &&
                   Country == other.Country &&
                   City == other.City &&
                   Province == other.Province &&
                   Area == other.Area &&
                   Street == other.Street &&
                   HouseNumber == other.HouseNumber &&
                   Receiver == other.Receiver &&
                   ContactPhone == other.ContactPhone;
        }

        public override int GetHashCode()
        {
            var hashCode = 1256853605;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Country);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(City);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Province);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Area);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Street);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(HouseNumber);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Receiver);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ContactPhone);
            return hashCode;
        }
        /// <summary>
        /// 获取收货地址的字符串表达形式
        /// XX国XX省XX市XX区XX街XX号  XX 联系手机号码：XXXXXXXXXXX
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Country}{Province}{City}{Area}{Street}{HouseNumber}  {Receiver}  联系手机号码:{ContactPhone}";
        }

        public static bool operator ==(ReceivingAddressValueObject object1, ReceivingAddressValueObject object2)
        {
            return EqualityComparer<ReceivingAddressValueObject>.Default.Equals(object1, object2);
        }

        public static bool operator !=(ReceivingAddressValueObject object1, ReceivingAddressValueObject object2)
        {
            return !(object1 == object2);
        }
    }
}
