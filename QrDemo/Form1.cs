using CommonUtils.FileOperate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QrDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Naip_Test.AddID();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Import(@"D:\workdir\EnvDB\trunk\doc\数据模块\22222222222\机场进离场点2018-01.xlsx");
        }

        private List<string> Import(string strFilePath)
        {
            List<string> lstResult = new List<string>();
            NPOIExcelHelper excelHelper = new NPOIExcelHelper(strFilePath);
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("机场名称", typeof(string));
            dtNew.Columns.Add("进场_传统", typeof(string));
            dtNew.Columns.Add("进场_PBN", typeof(string));
            dtNew.Columns.Add("离场_传统", typeof(string));
            dtNew.Columns.Add("离场_PBN", typeof(string));
            dtNew.Columns.Add("管理局", typeof(string));
            DataSet ds = NPOIExcelHelper.ImportDataSet(strFilePath,true);
            for (int t = 0; t < ds.Tables.Count; t++)
            {
                DataTable dt = ds.Tables[t];
                string name = dt.TableName;
                for (int index = 1; index < dt.Rows.Count; index++)
                {
                    DataRow dr = dt.Rows[index];
                    if (string.IsNullOrWhiteSpace(dr[0] + "") && string.IsNullOrWhiteSpace(dr[2] + "") &&
                        string.IsNullOrWhiteSpace(dr[3] + "") && string.IsNullOrWhiteSpace(dr[6] + "") &&
                        string.IsNullOrWhiteSpace(dr[7] + ""))
                        continue;
                    dtNew.Rows.Add(new object[] { dr[0], dr[2], dr[3], dr[6], dr[7] , name });
                }
            }
            for (int i = 0; i < dtNew.Rows.Count; i++)
            {
                var dr = dtNew.Rows[i];
                if (string.IsNullOrEmpty(dr[0] + ""))
                {
                    dr[0] = dtNew.Rows[i - 1][0];
                }
            }

           



            DataTable dtNaip = new DataTable();
            dtNaip.Columns.Add("机场名称", typeof(string));
            dtNaip.Columns.Add("航路点名称", typeof(string));
            dtNaip.Columns.Add("航路点呼号", typeof(string));
            dtNaip.Columns.Add("进离场类型", typeof(string));
            dtNaip.Columns.Add("规则类型", typeof(string));
            dtNaip.Columns.Add("管理局", typeof(string));
            for (int i = 0; i < dtNew.Rows.Count; i++)
            {
                var dr = dtNew.Rows[i];
                string str1 = dr[1] +""; //进场_传统
                string str2 = dr[2] + "";//进场_PBN
                string str3 = dr[3] + "";//离场_传统
                string str4 = dr[4] + "";//离场_PBN

                if (!string.IsNullOrWhiteSpace(str1))
                {
                    str1 = str1.Trim();
                    string[] aryStr = str1.Split(' ');
                    dtNaip.Rows.Add(new object[] { dr[0], aryStr[0], aryStr.Length > 1 ? aryStr[1] : "", "进场", "传统", dr[5] });
                }
                if (!string.IsNullOrWhiteSpace(str2))
                {
                    str2 = str2.Trim();
                    string[] aryStr = str2.Split(' ');
                    dtNaip.Rows.Add(new object[] { dr[0], aryStr[0], aryStr.Length > 1 ? aryStr[1] : "", "进场", "PBN", dr[5] });
                }
                if (!string.IsNullOrWhiteSpace(str3))
                {
                    str3 = str3.Trim();
                    string[] aryStr = str3.Split(' ');
                    dtNaip.Rows.Add(new object[] { dr[0], aryStr[0], aryStr.Length > 1 ? aryStr[1] : "", "离场", "传统", dr[5] });
                }
                if (!string.IsNullOrWhiteSpace(str4))
                {
                    str4 = str4.Trim();
                    string[] aryStr = str4.Split(' ');
                    dtNaip.Rows.Add(new object[] { dr[0], aryStr[0], aryStr.Length > 1 ? aryStr[1] : "", "离场", "PBN", dr[5] });
                }
            }
            this.dataGridView1.DataSource = dtNaip;
            this.Text = dtNaip.Rows.Count+"";

            return lstResult;
        }


      
    }
}
