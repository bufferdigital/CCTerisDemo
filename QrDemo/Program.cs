using QrDemo.GoBangGame;
using QrDemo.NoForder;
using QrDemo.QrGridControl;
using QrDemo.SuperAraneidDemo;
using QrDemo.Teris;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QrDemo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmNoFord());
        }
    }
}
