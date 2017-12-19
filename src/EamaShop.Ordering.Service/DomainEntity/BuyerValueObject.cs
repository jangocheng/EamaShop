using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Ordering.Service
{
    public class BuyerValueObject : IEquatable<BuyerValueObject>
    {
        public BuyerValueObject(long uId, string nickName, string account, string email)
        {
            UId = uId;
            NickName = nickName ?? throw new ArgumentNullException(nameof(nickName));
            Account = account ?? throw new ArgumentNullException(nameof(account));
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        public long UId { get; }

        public string NickName { get; }

        public string Account { get; }

        public string Email { get; }

        public override bool Equals(object obj)
        {
            return Equals(obj as BuyerValueObject);
        }

        public bool Equals(BuyerValueObject other)
        {
            return other != null &&
                   UId == other.UId &&
                   NickName == other.NickName &&
                   Account == other.Account &&
                   Email == other.Email;
        }

        public override int GetHashCode()
        {
            var hashCode = -1205800379;
            hashCode = hashCode * -1521134295 + UId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NickName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Account);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            return hashCode;
        }

        public static bool operator ==(BuyerValueObject object1, BuyerValueObject object2)
        {
            return EqualityComparer<BuyerValueObject>.Default.Equals(object1, object2);
        }

        public static bool operator !=(BuyerValueObject object1, BuyerValueObject object2)
        {
            return !(object1 == object2);
        }
    }
}
