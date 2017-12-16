using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Identity.Common
{
    /// <summary>
    /// 用户角色
    /// <para></para>
    /// 此枚举使用了位域的支持，以满足同一个用户的多角色需求，该对象应该在用户领域使用，对于DTO对象，应该使用数组的形式表达多个角色
    /// </summary>
    /// <remarks>不要修改该枚举的常量值，或确保以2的n次方进行赋值</remarks>
    /// <example>
    /// <![CDATA[
    ///  Role|UserRole.Merchant //增加一个merchant角色，如果不存在，将会添加，否则忽略
    ///  Role&UserRole.Merchant // 尝试从现有的角色中获取merchant角色，如果获取不到返回0
    ///  (Role&UserRole.Merchant)!=0 //是否包含merchant角色
    ///  Role^UserRole.Merchant //新增或者删除merchant角色
    ///  Role&(~UserRole.Merchant)//删除一个角色
    /// ]]>
    /// 
    /// </example>
    [Flags]
    public enum UserRole
    {
        User = 1,
        Merchant = 2,
        Admin = 4
    }
}
