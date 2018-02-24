using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrDemo
{
    public class Naip_Test
    {
        static ENVDBHelper m_dbEnvDB = new ENVDBHelper();

        public static void AddID()
        {
            string strSqlTable = "select * from user_tables";
            string strSqlAdd = "alter table {0} add (ID number(8) default 0 not null)";

            List<string> lstTable = new List<string>();
            var reader = m_dbEnvDB.ExecuteDataTable(System.Data.CommandType.Text, strSqlTable);

            for (int i = 0; i < reader.Rows.Count; i++)
            {
                lstTable.Add(reader.Rows[i][0] + "");
            }

            for (int i = 0; i < 1; i++)
            {
                //增加ID
                string strTableName = lstTable[i];
                strSqlAdd = string.Format(strSqlAdd, strTableName);
                //m_dbEnvDB.ExecuteNonQuery(System.Data.CommandType.Text, strSqlAdd);

                //增加主键
                string strPrimaryKey = "alter table {0} add constraint PK_ID_{1} primary key(ID)";
                strPrimaryKey = string.Format(strPrimaryKey, strTableName, strTableName);
                m_dbEnvDB.ExecuteNonQuery(System.Data.CommandType.Text, strPrimaryKey);

                //增加SEQ
                string strSEQ = "create sequence {0}_SEQ minvalue 1 maxvalue 9999999999999 start with 1 increment by 1 cache 10";
                strSEQ = string.Format(strSEQ, strTableName);
                m_dbEnvDB.ExecuteNonQuery(System.Data.CommandType.Text, strSEQ);

                //增加Triggers
                string strTriggers = "create or replace trigger \"TRI_{0}_ID\" before insert on {1} for each row " +
                                     "begin select {2}_SEQ.nextval into :new.ID from sys.dual; end; ";
                strTriggers = string.Format(strTriggers, strTableName, strTableName, strTableName);
                m_dbEnvDB.ExecuteNonQuery(System.Data.CommandType.Text, strTriggers);
            }
        }


        public static void AddIS_NAIP()
        {
            ENVDBHelper.connectionString = string.Format("Data Source={0};User Id={1};PassWord={2};",
            "(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.241.185)(PORT=1521))(CONNECT_DATA=(SID=HUANJING)))",
            "huanjing",
            "huanjing");
            string strSqlTable = "select * from user_tables";
            string strSqlAdd = "alter table {0} add (ID number(1) default 0 not null)";

            List<string> lstTable = new List<string>();
            var reader = m_dbEnvDB.ExecuteDataTable(System.Data.CommandType.Text, strSqlTable);

            for (int i = 0; i < reader.Rows.Count; i++)
            {
                lstTable.Add(reader.Rows[i][0] + "");
            }

            for (int i = 0; i < 1; i++)
            {
                //增加ID
                string strTableName = lstTable[i];
                strSqlAdd = string.Format(strSqlAdd, strTableName);
                //m_dbEnvDB.ExecuteNonQuery(System.Data.CommandType.Text, strSqlAdd);

                //增加主键
                string strPrimaryKey = "alter table {0} add constraint PK_ID_{1} primary key(ID)";
                strPrimaryKey = string.Format(strPrimaryKey, strTableName, strTableName);
                m_dbEnvDB.ExecuteNonQuery(System.Data.CommandType.Text, strPrimaryKey);

                //增加SEQ
                string strSEQ = "create sequence {0}_SEQ minvalue 1 maxvalue 9999999999999 start with 1 increment by 1 cache 10";
                strSEQ = string.Format(strSEQ, strTableName);
                m_dbEnvDB.ExecuteNonQuery(System.Data.CommandType.Text, strSEQ);

                //增加Triggers
                string strTriggers = "create or replace trigger \"TRI_{0}_ID\" before insert on {1} for each row " +
                                     "begin select {2}_SEQ.nextval into :new.ID from sys.dual; end; ";
                strTriggers = string.Format(strTriggers, strTableName, strTableName, strTableName);
                m_dbEnvDB.ExecuteNonQuery(System.Data.CommandType.Text, strTriggers);
            }
        }
    }
}
