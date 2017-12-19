using System;

namespace EamaShop.Infrastructures.Enums
{
    /// <summary>
    /// 易玛- 用户角色枚举
    /// <para></para>
    /// 此枚举使用了位域的支持，以满足同一个用户的多角色需求
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
        /// <summary>
        /// 该用户的角色为普通用户，这是所有用户角色必须具备的一种角色
        /// </summary>
        User = 1,//2的0次方
        /// <summary>
        /// 该用户是商户，或者具有门店的用户
        /// </summary>
        Merchant = 2,// 2的1次方
        /// <summary>
        /// 该用户是超级管理员，可以登陆和访问ERP
        /// </summary>
        Admin = 4,// 2的2次方
        /// <summary>
        /// 该用户是VIP
        /// </summary>
        Vip = 8// 2的三次方
    }
}
