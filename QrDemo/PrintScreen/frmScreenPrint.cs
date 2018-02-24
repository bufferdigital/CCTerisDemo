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
    public partial class frmScreenPrint : Form
    {
        ClipStatus m_bDrawState = ClipStatus.None;
        Point m_pStart, m_pEnd;
        Bitmap m_imgBack;
        Rectangle m_rcCapture;
        public frmScreenPrint()
        {
            InitializeComponent();
            m_imgBack = GetFullScreen();
            this.Opacity = 0.5;
            this.WindowState = FormWindowState.Maximized;
            this.MouseDown += FrmScreenPrint_MouseDown;
            this.MouseMove += FrmScreenPrint_MouseMove;
            this.MouseUp += FrmScreenPrint_MouseUp;
            this.MouseDoubleClick += FrmScreenPrint_MouseDoubleClick;
        }

        private void FrmScreenPrint_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (m_bDrawState != ClipStatus.None)
            {
                Graphics g = e.Graphics;
                Point ps = new Point(m_pStart.X, m_pStart.Y);
                Point pe = new Point(m_pEnd.X, m_pEnd.Y);
                //鼠标按下时的坐标
                Point newPoint = new Point(pe.X, pe.Y);
                //获取矩形的长度 
                int width = Math.Abs(ps.X - pe.X);
                int height = Math.Abs(ps.Y - pe.Y);
                if (width <= 0 || height <= 0) return;
                newPoint.X = ps.X < pe.X ? ps.X : newPoint.X;
                newPoint.Y = ps.Y < pe.Y ? ps.Y : newPoint.Y;
                Rectangle rc = new Rectangle(newPoint, new Size(width, height));
                var img = GetScreenImage(rc, m_imgBack);
                Pen p = new Pen(Color.Red, 5);
                //g.DrawRectangle(p, rc);
                //g.DrawEllipse(p, new Rectangle(ps, new Size(10, 10)));
                //g.DrawEllipse(p, new Rectangle(pe, new Size(10, 10)));
                //g.DrawImage(img, rc);
                //g.Dispose();

                Graphics dc = Graphics.FromImage((System.Drawing.Image)img);
                dc.DrawRectangle(p, new Rectangle(new Point(0, 0), new Size(width, height)));
                //将要绘制的内容绘制到dc上
                g.DrawImage(img, rc);
                dc.Dispose();
            }
        }
        private void FrmScreenPrint_MouseDown(object sender, MouseEventArgs e)
        {
            if (m_bDrawState == ClipStatus.None)
            {
                m_bDrawState = ClipStatus.Start;
                m_pStart = e.Location;
            }
            else if (m_bDrawState == ClipStatus.Move)
            {

            }
        }

        private void FrmScreenPrint_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.Location;
            if (m_bDrawState == ClipStatus.Start)
            {
                m_pEnd = p;
                Invalidate();
            }
            else if (m_rcCapture.Width > 0 && m_rcCapture.Height > 0)
            {
                if ((p.X > m_rcCapture.Left && p.X < m_rcCapture.Right) && (p.Y > m_rcCapture.Bottom && p.Y < m_rcCapture.Top))
                {
                    m_bDrawState = ClipStatus.Move;
                    this.Cursor = Cursors.Hand;
                }
            }
        }
        private void FrmScreenPrint_MouseUp(object sender, MouseEventArgs e)
        {
            m_bDrawState = ClipStatus.Compelete;
            m_pEnd = e.Location;
        }

        //获取当前屏幕的截图
        private Bitmap GetFullScreen()
        {
            //新建一个和屏幕大小相同的图片img  也可以用BitMap
            Bitmap img = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            //创建一个画板 让我们可以在画板上画图  大小和屏幕大小一样大
            Graphics g = Graphics.FromImage(img);
            //将屏幕图片拷贝到空白图片img
            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), Screen.PrimaryScreen.Bounds.Size);
            //Clipboard.SetImage(img);          
            //GetScreenImage(new Point(300, 300), new Point(600, 600), img);
            return img;
        }
        //获取截图区域的图片
        private Bitmap GetScreenImage(Rectangle rc, Bitmap baseMap)
        {
            //新建一个图片，让它与屏幕图片相同
            Bitmap copyBmp = (Bitmap)baseMap.Clone();
            //新建画板和画笔
            Graphics g = Graphics.FromImage(copyBmp);
            //var rc = new Rectangle(newPoint, new Size(width, height));
            var captureImg = CaptureImage(baseMap, new Point(0, 0), rc);
            //释放目前的画板
            g.Dispose();
            return captureImg;
        }
        public Bitmap CaptureImage(Bitmap baseMap, Point psrc, Rectangle rc)
        {
            //创建新图位图
            Bitmap bitmap = new Bitmap(rc.Width, rc.Height);
            //创建作图区域
            Graphics graphic = Graphics.FromImage(bitmap);
            //截取原图相应区域写入作图区
            graphic.DrawImage(baseMap, psrc.X, psrc.Y, rc, GraphicsUnit.Pixel);
            //Clipboard.SetImage(bitmap);
            //从作图区生成新图
            //Image saveImage = Image.FromHbitmap(bitmap.GetHbitmap());
            //保存图片
            //saveImage.Save(toImagePath, ImageFormat.Png);
            //释放资源   
            //saveImage.Dispose();
            graphic.Dispose();
            return bitmap;
        }
    }
    public enum ClipStatus
    {
        None,
        Start,
        Compelete,
        Move,
    }
}
