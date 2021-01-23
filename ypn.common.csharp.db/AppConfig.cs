/************************************************************************
*Copyright (c) 2021   All Rights Reserved .
*CLR 版本    ：4.0.30319.42000
*机器名称    ：PC-20201201KGNJ
*公司名称    : 
*命名空间    ：ypn.common.csharp.db
*文件名称    ：AppConfig.cs
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
    class AppConfig
    {
        //
        /// <summary>
        /// 数据库密码
        /// </summary>
        public const string DATABASEPASSWORD = "123";
        //public const string DATABASEPASSWORD = "";//置空后便于分析数据
        /// <summary>
        /// AES加密所需要的钥匙（注：必须为16位）
        /// </summary>
        public const string AESKEY = "1111222233334444";
        /// <summary>
        /// AES加密所需要的偏移量（注：必须为16位）
        /// </summary>
        public const string AESIV = "1111222233334444";
        /// <summary>
        /// 系统自动更新频率，单位：分钟
        /// https://github.com/ravibpatel/AutoUpdater.NET
        /// </summary>
        public const int AUTOUPDATEINTERVAL = 30;
        /// <summary>
        /// 系统更新日志URL
        /// </summary>
        public const string AUTOUPDATELOG = "http://127.0.0.1/UpdateLog.html";
        /// <summary>
        /// 系统更新日志英文URL
        /// </summary>
        public const string AUTOUPDATELOG_ENUS = "http://127.0.0.1/UpdateLog_EN-US.html";
        /// <summary>
        /// 系统更新服务URL
        /// </summary>
        public const string AUTOUPDATESERVICE = "http://127.0.0.1/AutoupdateService.xml";
        /// <summary>
        /// 模块更新服务URL
        /// </summary>
        public const string AUTOUPDATEMODULESERVICE= "http://127.0.0.1/AutoupdateModuleService.xml";
        /// <summary>
        /// 软件有效期，单位：天
        /// </summary>
        public const int VALIDTIME = 5;
        /// <summary>
        /// 数据定时同步开关
        /// </summary>
        public const bool TIMESYNCENABLED = true;
        /// <summary>
        /// 数据定时同步频率，单位：毫秒
        /// </summary>
        public const int TIMESYSNCINTERVAL = 1800000;//30分钟 = 1800000
        /// <summary>
        /// 通用表格下载地址
        /// </summary>
        public const string DOWNLOADTEMPLATEURL = "http://127.0.0.1/import.Out.Common.xlsx";
        /// <summary>
        ///每次下载产品的数量
        /// </summary>
        public const int DOWNLOADPRODUCTQUANTITY = 500 ;
    }
}
