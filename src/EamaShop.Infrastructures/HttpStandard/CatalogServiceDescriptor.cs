using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Infrastructures.HttpStandard
{
    public class CatalogServiceDescriptor : MicroserviceDescriptor
    {
        public override string Name => "catalog";
        public override string NormalizedName => "商品微服务";
        public override Uri Host { get; } = new Uri("http://localhost:58752/");
    }
}
