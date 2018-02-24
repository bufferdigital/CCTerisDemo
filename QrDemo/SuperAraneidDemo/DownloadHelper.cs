using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QrDemo.SuperAraneidDemo
{
    class DownloadHelper
    {
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="url"></param>
        public static string DowloadImg(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                try
                {
                    if (!url.Contains("http"))
                    {
                        url = GlobalConfig.WebUrl + url;
                    }
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Timeout = 2000;
                    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
                    request.AllowAutoRedirect = true;//是否允许302
                    WebResponse response = request.GetResponse();
                    Stream reader = response.GetResponseStream();
                    string aFirstName = Guid.NewGuid().ToString();  //文件名
                    string aLastName = url.Substring(url.LastIndexOf(".") + 1, (url.Length - url.LastIndexOf(".") - 1));   //扩展名

                    FileStream writer = new FileStream(GlobalConfig.FloderUrl + aFirstName + "." + aLastName, FileMode.OpenOrCreate, FileAccess.Write);
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
