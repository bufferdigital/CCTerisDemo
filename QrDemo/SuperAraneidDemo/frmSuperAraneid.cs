using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QrDemo.SuperAraneidDemo
{
    public partial class frmSuperAraneid : Form
    {

        public frmSuperAraneid()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "http://588ku.com/sucai/0-pxnum-0-0-0-1/?h=bd&sem=1";
            //string url = StringHelper.GetPureUrl("https://www.zhihu.com/question/266467210/answer/308342541");
            GlobalConfig.WebUrl = StringHelper.GetPureUrl(url);
            HttpUrlWork work = new HttpUrlWork() { URL = url };
            TaskHelperCopy.CallBackAddUrl = ShowUrl;
            TaskHelperImage.ImageCallBack = ShowImageUrl;
            Task t = TaskHelperCopy.RunProgram(work);
            Task t1 = TaskHelperImage.RunProgram();

            //Task t = RunProgram();
            //t.Wait();
            //Console.ReadKey();
        }

        public void ShowUrl(string strUrl, string strHtml)
        {
            if (!string.IsNullOrWhiteSpace(strHtml))
            {
                var lstImgUrl = HttpHelper.GetHtmlImageUrlList(strHtml);
                foreach (var data in lstImgUrl)
                {
                    TaskHelperImage.taskQueue.Enqueue(data);
                }
            }
            this.BeginInvoke((MethodInvoker)delegate
            {
                this.listBox1.Items.Add(strUrl);
                this.label1.Text = this.listBox1.Items.Count + "";
            });
        }
        public void ShowImageUrl(string strImagePath)
        {        
            this.BeginInvoke((MethodInvoker)delegate
            {
                this.listBox2.Items.Add(strImagePath);             
            });
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string url = "http://blog.sina.com.cn/cc918035096";
            GlobalConfig.WebUrl = url;
            HttpUrlWork work = new HttpUrlWork() { URL = url };
            HttpHelper.HtmlCode(url);
        }
    }
}
