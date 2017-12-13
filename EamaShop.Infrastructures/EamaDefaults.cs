using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class EamaDefaults
    {
        /// <summary>
        /// 默认的签名密码 保证字节数在126以上就行了
        /// </summary>
        public const string JwtBearerSignKey = "bb6cabb9-58b4-4d93-9a99-bba213d652d7";
        /// <summary>
        /// 授权的对象，授权的是所有的 Api
        /// </summary>
        public const string Audience = "Api";
    }
}
