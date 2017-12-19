using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Infrastructures.HttpStandard
{
    public class MerchantServiceDescriptor : MicroserviceDescriptor
    {
        public override string Name => "merchant";
        public override string NormalizedName => "商户店铺微服务";
        public override Uri Host { get; } = new Uri("http://localhost:59423/");
    }
}
