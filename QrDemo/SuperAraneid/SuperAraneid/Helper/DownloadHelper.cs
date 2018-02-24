using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperAraneid.Helper
{
    public delegate string StopTimeHandler(string name);
    public class DownloadHelper
    {
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="url"></param>
        public string DowloadImg(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                try
                {
                    if (!url.Contains("http"))
                    {
                        url = Global.WebUrl + url;
                    }
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Timeout = 2000;
                    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.1.4322)"; // 
                    request.AllowAutoRedirect = true;//是否允许302
                    WebResponse response = request.GetResponse();
                    Stream reader = response.GetResponseStream();
                    string aFirstName = Guid.NewGuid().ToString();  //文件名
                    string aLastName = url.Substring(url.LastIndexOf(".") + 1, (url.Length - url.LastIndexOf(".") - 1));   //扩展名

                    FileStream writer = new FileStream(Global.FloderUrl + aFirstName + "." + aLastName, FileMode.OpenOrCreate, FileAccess.Write);
                    byte[] buff = new byte[512];

                    int c = 0; //实际读取的字节数
                    while ((c = reader.Read(buff, 0, buff.Length)) > 0)
                    {
                        writer.Write(buff, 0, c);
                    }
                    writer.Close();
                    writer.Dispose();
                    reader.Close();
                    reader.Dispose();
                    response.Close();
                    return (aFirstName + "." + aLastName);
                }
                catch (Exception)
                {
                    return "错误：地址" + url;
                }
            }
            return "错误：地址为空";
        }
    }
}
