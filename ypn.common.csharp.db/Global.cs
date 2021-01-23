/************************************************************************
*Copyright (c) 2021   All Rights Reserved .
*CLR 版本    ：4.0.30319.42000
*机器名称    ：PC-20201201KGNJ
*公司名称    : 
*命名空间    ：ypn.common.csharp.db
*文件名称    ：Global.cs
*版 本 号    : 2021|V1.0.0.0 
*=================================
*创 建 者    ：@ YANGPIENA
*创建日期    ：2021/01/23 18:52:46 
*电子邮箱    ：yangpiena@163.com
*个人主站    ：http://ynn5ru.coding-pages.com
*功能描述    ：
*使用说明    ：
*=================================
*修改日期    ：2021/01/23 18:52:46 
*修 改 者    ：Administrator
*修改描述    ：
*版 本 号    : 2021|V1.0.0.0 
***********************************************************************/

namespace ypn.common.csharp.db
{
    internal static class Global
    {
        /// <summary>
        /// 缓存过期时间（小时）
        /// </summary>
        public static readonly double CacheDeadline = 1;
        /// <summary>
        /// 系统当前语言
        /// </summary>
        public static string Language { get; set; } = "zh-CN";
        /// <summary>
        /// 是否手动操作
        /// </summary>
        public static bool IsManualOperate { get; set; } = false;
        /// <summary>
        /// 单位制
        /// </summary>
        public static string UNITSYSTEM { get; set; } = "";
    }
}
