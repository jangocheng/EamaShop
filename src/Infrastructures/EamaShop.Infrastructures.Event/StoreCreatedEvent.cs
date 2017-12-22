using System;
using System.Collections.Generic;

namespace EamaShop.Infrastructures.Events
{
    public class StoreCreatedEvent : IEventMetadata
    {
        public long StoreId { get; set; }

        public string StoreName { get; set; }

        public string StoreDescription { get; set; }

        public string StoreLogoUri { get; set; }

        public IEnumerable<string> StoreScopes { get; set; }

        public long UId { get; set; }
    }
}
