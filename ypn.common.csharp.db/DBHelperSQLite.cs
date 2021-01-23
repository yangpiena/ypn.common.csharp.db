/************************************************************************
*Copyright (c) 2021   All Rights Reserved .
*CLR 版本    ：4.0.30319.42000
*机器名称    ：PC-20201201KGNJ
*公司名称    : 
*命名空间    ：ypn.common.csharp.db
*文件名称    ：SQLiteHelper.cs
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace ypn.common.csharp.db
{
    public class SQLiteHelper
    {
        private static readonly string f_connStr1 = "Data Source=|DataDirectory|\\xx.db;Version=3;FailIfMissing=True;Password=" + AppConfig.DATABASEPASSWORD;
        private static readonly string f_connStr2 = "Data Source=|DataDirectory|\\xxyb.db;Version=3;FailIfMissing=True;Password=" + AppConfig.DATABASEPASSWORD;
        public static readonly string f_connStr3 = f_connStr2;
        private static SQLiteConnection f_Conn1 = null; // 数据库连接
        private static SQLiteConnection f_Conn2 = null;
        private static SQLiteConnection f_Conn3 = null;

        #region 基础方法

        /// <summary>
        /// 新建数据库文件
        /// YPN 2019-10-25 Create
        /// </summary>
        /// <param name="i_dbName">数据库文件名称</param>
        /// <returns>新建成功，返回true，否则返回false</returns>
        public static bool CreateDB(string i_dbName)
        {
            try
            {
                SQLiteConnection.CreateFile(i_dbName);
                Console.WriteLine("新建数据库文件...ok");
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("新建数据库文件" + i_dbName + "失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 初始化数据库
        /// YPN 2019-10-27 Create
        /// </summary>
        /// <returns></returns>
        public static bool InitDataBase()

        {
            bool v_db1;
            bool v_db2;

            try
            {
                SQLiteConnection v_conn1 = new SQLiteConnection("Data Source=|DataDirectory|\\xx.db;Version=3");
                v_conn1.Open();
                v_conn1.ChangePassword(AppConfig.DATABASEPASSWORD);

                v_db1 = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("系统数据文件xx.db异常：" + e.Message);
                v_db1 = false;
            }
            try
            {
                SQLiteConnection v_conn2 = new SQLiteConnection("Data Source=|DataDirectory|\\xxyb.db;Version=3");
                v_conn2.Open();
                v_conn2.ChangePassword(AppConfig.DATABASEPASSWORD);
                v_db2 = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("系统数据文件xxyb.db异常：" + e.Message);
                v_db2 = false;
            }
            return v_db1 && v_db2;
        }

        /// <summary>
        /// 得到数据库连接方法
        /// YPN 2019-08-02 Create
        /// </summary>
        /// <param name="i_DBNo">数据库编号</param>
        /// <returns>数据库连接</returns>
        private static void GetConn(int i_DBNo)
        {
            try
            {
                if (i_DBNo == 1)
                {
                    if (f_Conn1 == null)
                        f_Conn1 = new SQLiteConnection(f_connStr1);
                }
                else if (i_DBNo == 2)
                {
                    if (Global.Language == "en-US")
                    {
                        if (f_Conn3 == null)
                            f_Conn3 = new SQLiteConnection(f_connStr3);
                    }
                    else
                    {
                        if (f_Conn2 == null)
                            f_Conn2 = new SQLiteConnection(f_connStr2);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 获得并打开数据库连接方法
        /// YPN 2019-08-02 Create
        /// </summary>
        /// <param name="i_DBNo">数据库编号</param>
        /// <returns></returns>
        private static void OpenConn(int i_DBNo)
        {
            try
            {
                if (i_DBNo == 1)
                {
                    GetConn(i_DBNo);
                    if (f_Conn1.State == ConnectionState.Open)
                        return;
                    if (f_Conn1.State != ConnectionState.Closed)
                        f_Conn1.Close();
                    f_Conn1.Open();
                }
                else if (i_DBNo == 2)
                {
                    if (Global.Language == "en-US")
                    {
                        GetConn(i_DBNo);
                        if (f_Conn3.State == ConnectionState.Open)
                            return;
                        if (f_Conn3.State != ConnectionState.Closed)
                            f_Conn3.Close();
                        f_Conn3.Open();
                    }
                    else
                    {
                        GetConn(i_DBNo);
                        if (f_Conn2.State == ConnectionState.Open)
                            return;
                        if (f_Conn2.State != ConnectionState.Closed)
                            f_Conn2.Close();
                        f_Conn2.Open();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 创建sql执行命令
        /// YPN 2019-08-02 Create
        /// </summary>
        /// <param name="i_DBNo">数据库编号</param>
        /// <returns></returns>
        private static SQLiteCommand CreateCmd(int i_DBNo)
        {
            if (i_DBNo == 1)
            {
                return f_Conn1.CreateCommand();
            }
            else if (i_DBNo == 2)
            {
                if (Global.Language == "en-US")
                {
                    return f_Conn3.CreateCommand();
                }
                else
                {
                    return f_Conn2.CreateCommand();
                }
            }
            return f_Conn1.CreateCommand();
        }

        /// <summary>
        /// 关闭数据库连接方法
        /// YPN 2019-08-02 Create
        /// <param name="i_DBNo">数据库编号</param>
        /// </summary>
        public static void CloseConn(int i_DBNo)
        {
            if (i_DBNo == 1)
            {
                if (f_Conn1 != null && f_Conn1.State != ConnectionState.Closed)
                    f_Conn1.Close();
            }
            else if (i_DBNo == 2)
            {
                if (Global.Language == "en-US")
                {
                    if (f_Conn3 != null && f_Conn3.State != ConnectionState.Closed)
                        f_Conn3.Close();
                }
                else
                {
                    if (f_Conn2 != null && f_Conn2.State != ConnectionState.Closed)
                        f_Conn2.Close();
                }
            }
        }

        #endregion 基础方法

        #region 数据库操作方法

        /// <summary>
        /// 执行 UPDATE、INSERT 或 DELETE 语句，并返回受影响的行数
        /// YPN 2019-08-02 Create
        /// </summary>
        /// <param name="i_cmdText"></param>
        /// <param name="i_paramates"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string i_cmdText, params SQLiteParameter[] i_paramates)
        {
            OpenConn(1);
            using (SQLiteCommand v_cmd = CreateCmd(1))
            {
                v_cmd.CommandText = i_cmdText;
                v_cmd.CommandTimeout = 3000;
                if (i_paramates != null)
                {
                    v_cmd.Parameters.AddRange(i_paramates);
                }
                int v_Result = v_cmd.ExecuteNonQuery();
                return v_Result;
            }
        }

        /// <summary>
        /// 批量执行 UPDATE、INSERT 或 DELETE 语句
        /// YPN 2019-10-30 Create
        /// </summary>
        /// <param name="i_sqlList"></param>
        public static void ExecuteNonQueryBatch(List<KeyValuePair<string, SQLiteParameter[]>> i_sqlList)
        {
            OpenConn(1);
            using (SQLiteTransaction v_tran = f_Conn1.BeginTransaction())
            {
                using (SQLiteCommand v_cmd = CreateCmd(1))
                {
                    try
                    {
                        foreach (var v_item in i_sqlList)
                        {
                            v_cmd.CommandText = v_item.Key;
                            if (v_item.Value != null)
                            {
                                v_cmd.Parameters.AddRange(v_item.Value);
                            }
                            v_cmd.ExecuteNonQuery();
                        }
                        v_tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        v_tran.Rollback();
                        //LoggerHelper.LogError(ex, null, "ExecuteNonQueryBatch");
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// 批量执行 UPDATE、INSERT 或 DELETE 语句
        /// YPN 2019-10-30 Create
        /// </summary>
        /// <param name="i_sqlList"></param>
        public static int ExecuteBatch(List<KeyValuePair<string, SQLiteParameter[]>> i_sqlList)
        {
            int v_Result = 0;
            OpenConn(1);
            using (SQLiteTransaction v_tran = f_Conn1.BeginTransaction())
            {
                using (SQLiteCommand v_cmd = CreateCmd(1))
                {
                    try
                    {
                        foreach (var v_item in i_sqlList)
                        {
                            v_cmd.CommandText = v_item.Key;
                            if (v_item.Value != null)
                            {
                                v_cmd.Parameters.AddRange(v_item.Value);
                            }
                            v_Result += v_cmd.ExecuteNonQuery();
                        }
                        v_tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        v_tran.Rollback();
                        //LoggerHelper.LogError(ex, null, "ExecuteNonQueryBatch");
                        throw;
                    }
                }
            }
            return v_Result;
        }

        /// <summary>
        /// 执行 SELECT 语句，并返回查询所返回的结果集中第一行的第一列。 忽略其他列或行。
        /// YPN 2019-08-02 Create
        /// </summary>
        /// <param name="i_DBNo">数据库编号</param>
        /// <param name="i_CmdText"></param>
        /// <param name="i_Parameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(int i_DBNo, string i_CmdText, params SQLiteParameter[] i_Parameters)
        {
            OpenConn(i_DBNo);
            using (SQLiteCommand v_cmd = CreateCmd(i_DBNo))
            {
                v_cmd.CommandText = i_CmdText;
                v_cmd.CommandTimeout = 3000;
                v_cmd.Parameters.AddRange(i_Parameters);
                return v_cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// 执行 SELECT 语句，返回一个 DataTable（表格）
        /// YPN 2019-08-02 Create
        /// </summary>
        /// <param name="i_DBNo">数据库编号</param>
        /// <param name="i_CmdText"></param>
        /// <param name="i_Parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(int i_DBNo, string i_CmdText, params SQLiteParameter[] i_Parameters)
        {
            OpenConn(i_DBNo);
            using (SQLiteCommand v_cmd = CreateCmd(i_DBNo))
            {
                v_cmd.CommandText = i_CmdText;
                v_cmd.CommandTimeout = 3000;
                if (i_Parameters != null)
                {
                    v_cmd.Parameters.AddRange(i_Parameters);
                }
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(v_cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        /// <summary>
        /// 执行 SELECT 语句，返回一个 DataTable（表格）
        /// （查询较慢，仅限异步查询、缓存数据时使用）
        /// YPN 2019-08-07 Create
        /// </summary>
        /// <param name="i_DBNo">数据库编号</param>
        /// <param name="i_CmdText"></param>
        /// <param name="i_Parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTableAsync(int i_DBNo, string i_CmdText, params SQLiteParameter[] i_Parameters)
        {
            using (SQLiteConnection v_conn = new SQLiteConnection(i_DBNo == 1 ? f_connStr1 : Global.Language == "en-US" ? f_connStr3 : f_connStr2))
            {
                v_conn.Open();
                using (SQLiteCommand v_cmd = v_conn.CreateCommand())
                {
                    v_cmd.CommandText = i_CmdText;
                    v_cmd.CommandTimeout = 3000;
                    v_cmd.Parameters.AddRange(i_Parameters);
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(v_cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        /// <summary>
        /// 执行 SELECT 语句，返回一个 DataSet（包含多个表格）
        /// YPN 2019-08-02 Create
        /// </summary>
        /// <param name="i_DBNo">数据库编号</param>
        /// <param name="i_CmdText"></param>
        /// <param name="i_Parameters"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(int i_DBNo, string i_CmdText, params SQLiteParameter[] i_Parameters)
        {
            OpenConn(i_DBNo);
            using (SQLiteCommand v_cmd = CreateCmd(i_DBNo))
            {
                v_cmd.CommandText = i_CmdText;
                v_cmd.CommandTimeout = 3000;
                v_cmd.Parameters.AddRange(i_Parameters);
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(v_cmd))
                {
                    DataSet dt = new DataSet();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        /// <summary>
        /// 执行 SELECT 语句，返回一个 OleDbDataReader，即一行记录
        /// YPN 2019-08-02 Create
        /// </summary>
        /// <param name="i_DBNo">数据库编号</param>
        /// <param name="i_CmdText"></param>
        /// <param name="i_Parameters"></param>
        /// <returns></returns>
        public static SQLiteDataReader ExecuteDataReader(int i_DBNo, string i_CmdText, params SQLiteParameter[] i_Parameters)
        {
            OpenConn(i_DBNo);
            using (SQLiteCommand v_cmd = CreateCmd(i_DBNo))
            {
                v_cmd.CommandText = i_CmdText;
                v_cmd.CommandTimeout = 3000;
                if (i_Parameters!=null)
                {
                    v_cmd.Parameters.AddRange(i_Parameters);

                }
                return v_cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }

        /// <summary>
        /// 返回SQL适用的NULL
        /// YPN 2019-08-02 Create
        /// </summary>
        /// <param name="i_Object">要转换的对象</param>
        /// <returns></returns>
        public static object GetSQLNull(object i_Object)
        {
            if (i_Object == null)
            {
                return DBNull.Value;
            }
            return i_Object;
        }

        /// <summary>
        /// 检查表是否存在
        /// </summary>
        /// <param name="i_TableName"></param>
        /// <returns></returns>
        public static bool CheckExistsTable(string i_TableName)
        {
            string v_SQL = " SELECT COUNT(*) FROM SQLITE_MASTER WHERE TYPE = 'table' AND NAME =@NAME";
            SQLiteParameter[] v_SQLiteParameter =
            {
                new SQLiteParameter("@NAME",i_TableName)
            };
            object v_Object = SQLiteHelper.ExecuteScalar(1, v_SQL, v_SQLiteParameter);

            int v_Result = Convert.ToInt32(v_Object);
            if (v_Result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="i_CmdText">创建表、删除表的sql</param>
        /// <returns></returns>
        public static int OperateTable(string i_CmdText)
        {
            OpenConn(1);
            using (SQLiteCommand v_cmd = CreateCmd(1))
            {
                v_cmd.CommandText = i_CmdText;
                v_cmd.CommandTimeout = 3000;
                int v_Result = v_cmd.ExecuteNonQuery();
                return v_Result;
            }
        }

        #endregion 数据库操作方法

        #region 用户多线程操作数据库

        public static object TaskExecuteScalar(int i_DBNo, string i_CmdText, params SQLiteParameter[] i_Parameters)
        {
            var constring = "Data Source=|DataDirectory|\\xx.db;Version=3;FailIfMissing=True;Password=" + AppConfig.DATABASEPASSWORD;
            if (i_DBNo != 1)
            {
                constring = "Data Source=|DataDirectory|\\xxyb.db;Version=3;FailIfMissing=True;Password=" + AppConfig.DATABASEPASSWORD;
            }
            using (var conn = new SQLiteConnection(constring))
            {
                conn.Open();
                using (SQLiteCommand v_cmd = conn.CreateCommand())
                {
                    v_cmd.CommandText = i_CmdText;
                    v_cmd.CommandTimeout = 3000;
                    v_cmd.Parameters.AddRange(i_Parameters);
                    return v_cmd.ExecuteScalar();
                }
            }
        }

        public static int TaskExecuteNonQuery(string i_cmdText, params SQLiteParameter[] i_paramates)
        {
            using (var conn = new SQLiteConnection("Data Source=|DataDirectory|\\xx.db;Version=3;FailIfMissing=True;Password=" + AppConfig.DATABASEPASSWORD))
            {
                conn.Open();
                using (SQLiteCommand v_cmd = conn.CreateCommand())
                {
                    v_cmd.CommandText = i_cmdText;
                    v_cmd.CommandTimeout = 3000;
                    v_cmd.Parameters.AddRange(i_paramates);
                    int v_Result = v_cmd.ExecuteNonQuery();
                    return v_Result;
                }
            }
        }

        public static DataTable TaskExecuteDataTable(int i_DBNo, string i_CmdText, params SQLiteParameter[] i_Parameters)
        {
            var constring = "Data Source=|DataDirectory|\\xx.db;Version=3;FailIfMissing=True;Password=" + AppConfig.DATABASEPASSWORD;
            if (i_DBNo != 1)
            {
                constring = "Data Source=|DataDirectory|\\xxyb.db;Version=3;FailIfMissing=True;Password=" + AppConfig.DATABASEPASSWORD;
            }
            using (var conn = new SQLiteConnection(constring))
            {
                conn.Open();
                using (SQLiteCommand v_cmd = conn.CreateCommand())
                {
                    v_cmd.CommandText = i_CmdText;
                    v_cmd.CommandTimeout = 3000;
                    v_cmd.Parameters.AddRange(i_Parameters);
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(v_cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public static int TaskOperateTable(string i_CmdText)
        {
            using (var conn = new SQLiteConnection("Data Source=|DataDirectory|\\xx.db;Version=3;FailIfMissing=True;Password=" + AppConfig.DATABASEPASSWORD))
            {
                conn.Open();
                using (SQLiteCommand v_cmd = conn.CreateCommand())
                {
                    v_cmd.CommandText = i_CmdText;
                    v_cmd.CommandTimeout = 3000;
                    int v_Result = v_cmd.ExecuteNonQuery();
                    return v_Result;
                }
            }
        }

        #endregion 用户多线程操作数据库
    }
}