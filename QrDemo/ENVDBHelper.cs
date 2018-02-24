using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace QrDemo
{
    public class ENVDBHelper
    {
        /// 连接字符串
        public static string connectionString = string.Format("Data Source={0};User Id={1};PassWord={2};",
            "(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.241.185)(PORT=1521))(CONNECT_DATA=(SID=HUANJING)))",
            "ENV_NAIP",
            "ENV_NAIP");

        //public static void InitConnectionString()
        //{
        //    connectionString = string.Format("Data Source={0};User Id={1};PassWord={2};", Globals.GlobalData.SysConfig.DataBase.ListConnection[1].DataBaseName, Globals.GlobalData.SysConfig.DataBase.ListConnection[1].UserName, Globals.GlobalData.SysConfig.DataBase.ListConnection[1].PassWord);
        //}

        public OracleCommand PrepareCommand(OracleConnection conn, CommandType cmdType, string cmdText, params OracleParameter[] paras)
        {
            //Open the connection if required
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            OracleCommand cmd = new OracleCommand(cmdText, conn);
            cmd.CommandType = cmdType;
            if (paras != null)
            {
                foreach (OracleParameter para in paras)
                {
                    if (para != null)
                    {
                        cmd.Parameters.Add(para);
                    }
                }
            }
            return cmd;
        }

        public OracleDataReader ExecuteReader(CommandType cmdType, string cmdText, params OracleParameter[] paras)
        {
            OracleConnection conn = new OracleConnection(connectionString);
            try
            {
                OracleCommand cmd = PrepareCommand(conn, cmdType, cmdText, paras);
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                //Globals.GlobalOperation.AddLog(ex.Message, Common.Log.LogLevel.Error);
                conn.Close();
                return null;
            }
        }

        public DataTable ExecuteDataTable(CommandType cmdType, string cmdText, params OracleParameter[] paras)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    OracleCommand cmd = PrepareCommand(conn, cmdType, cmdText, paras);
                    OracleDataAdapter dataAdapter = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);
                    return dt;
                }
                catch (Exception ex)
                {
                    //Globals.GlobalOperation.AddLog(ex.Message, Common.Log.LogLevel.Error);
                    return null;
                }
            }
        }

        public int ExecuteNonQuery(CommandType cmdType, string cmdText, params OracleParameter[] paras)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    OracleCommand cmd = PrepareCommand(conn, cmdType, cmdText, paras);
                    return cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    //Globals.GlobalOperation.AddLog(ex.Message, Common.Log.LogLevel.Error);
                    return -1;
                }
            }
        }

        public object ExecuteScalar(CommandType cmdType, string cmdText, params OracleParameter[] paras)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    OracleCommand cmd = PrepareCommand(conn, cmdType, cmdText, paras);
                    return cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    //Globals.GlobalOperation.AddLog(ex.Message, Common.Log.LogLevel.Error);
                    return null;
                }
            }
        }
    }
}