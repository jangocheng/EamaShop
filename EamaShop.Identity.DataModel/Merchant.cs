using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Identity.DataModel
{
    public class Merchant
    {
        public long Id { get; set; }
        public DateTime CreateTime { get; set; }

        public DateTime ModifiedTime { get; set; }

        public long CreatorUId { get; set; }

        public string Name { get; set; }
    }
}
