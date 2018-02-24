using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QrDemo
{
    public partial class frmScreenShots : Form
    {
        List<Line> m_lstLine = new List<Line>();
        bool m_bDrawState = false;
        Line m_curLine = new Line();
        Bitmap m_imgFullScreen = null;

        [DllImport("gdi32.dll")]
        static extern int SetROP2(IntPtr hdc, int fnDrawMode);
        public frmScreenShots()
        {
            InitializeComponent();
            this.pictureBox1.MouseDown += PictureBox1_MouseDown;
            this.pictureBox1.MouseMove += PictureBox1_MouseMove;
            this.pictureBox1.Paint += PictureBox1_Paint;
            //g = this.pictureBox1.CreateGraphics();
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.WindowState = FormWindowState.Maximized;

        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            DrawLine(e.Graphics, m_curLine);
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_bDrawState && m_curLine.StartPoint.X > 10)
            {
                m_curLine.EndPoint = e.Location;
                this.pictureBox1.Invalidate();
                //g.DrawLine(new Pen(new SolidBrush(Color.Red), 5), m_curLine.StartPoint, m_curLine.EndPoint);
                //DrawLine(m_curLine);
            }
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_curLine = new Line();
                if (m_bDrawState)
                {
                    m_curLine.StartPoint = e.Location;
                }
            }
            else
            {
                m_curLine.EndPoint = e.Location;
                this.pictureBox1.Invalidate();
                //DrawLine(m_curLine);
                m_bDrawState = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            m_imgFullScreen = ShotFullScreen();
            m_bDrawState = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //m_curLine = new Line() { StartPoint = new Point(0, 0), EndPoint = new Point(300, 300) };
            m_imgFullScreen = ShotFullScreen();
            var g2 = this.pictureBox1.CreateGraphics();
            //g2.DrawImage(m_imgFullScreen, new Rectangle(new Point(100, 100), new Size(300, 300)));
            Rectangle rc = new Rectangle(new Point(0, 0), new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));
            g2.DrawImage(m_imgFullScreen, rc);
            g2.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.Gray)), rc);
            Clipboard.SetImage(m_imgFullScreen);

            Point sp1 = new Point(100, 100);
            Point sp2 = new Point(800, 800);
            var img = GetScreenImage(sp1, sp2, m_imgFullScreen);
            //Clipboard.SetImage(img);
            g2.DrawImage(img, new Rectangle(sp1, new Size(700, 700)));

            Pen p = new Pen(Color.Red, 5);
            g2.DrawLine(p, sp1, sp2);
            return;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Rectangle rc = new Rectangle(new Point(0, 0), new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));
            var g = this.CreateGraphics();
            g.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.Red)), rc);
        }

        private void DrawLine(Graphics g, Line line)
        {
            if (m_imgFullScreen == null || line.StartPoint.X == line.EndPoint.X
                || line.StartPoint.Y == line.EndPoint.Y)
                return;

            Point sp1 = this.PointToScreen(line.StartPoint);
            Point sp2 = this.PointToScreen(line.EndPoint);
            if (sp1.X == sp2.X || sp1.Y == sp2.Y)
                return;
            var img = GetScreenImage(sp1, sp2, m_imgFullScreen);

            Point p1 = new Point(line.StartPoint.X, line.EndPoint.Y);
            Point p2 = new Point(line.EndPoint.X, line.StartPoint.Y);
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;          
            //g.FillPolygon(new SolidBrush(Color.FromArgb(200, Color.Red)), new Point[] { line.StartPoint, p1, line.EndPoint, p2, line.StartPoint });

            //g.DrawImage(img, new Point[] { line.StartPoint, p1, line.EndPoint, p2 });
            Rectangle rc = new Rectangle(new Point(0, 0), new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));
            g.DrawImage(m_imgFullScreen, rc);
            g.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.Gray)), rc);
            g.DrawImage(img, new Point[] { line.StartPoint, p1, line.EndPoint, p2, });

        }
        private void DrawRect(Graphics g, Line line)
        {
            if (m_imgFullScreen == null || line.StartPoint.X == line.EndPoint.X
                || line.StartPoint.Y == line.EndPoint.Y)
                return;

            Point sp1 = this.PointToScreen(line.StartPoint);
            Point sp2 = this.PointToScreen(line.EndPoint);
            if (sp1.X == sp2.X || sp1.Y == sp2.Y)
                return;
            var img = GetScreenImage(new Point(100, 100), new Point(500, 500), m_imgFullScreen);

            Point p1 = new Point(line.StartPoint.X, line.EndPoint.Y);
            Point p2 = new Point(line.EndPoint.X, line.StartPoint.Y);
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;          
            //g.FillPolygon(new SolidBrush(Color.FromArgb(200, Color.Red)), new Point[] { line.StartPoint, p1, line.EndPoint, p2, line.StartPoint });

            //g.DrawImage(img, new Point[] { line.StartPoint, p1, line.EndPoint, p2 });
            g.DrawImage(img, line.StartPoint);
            //g.DrawImage();
            return;
        }

        private Bitmap GetScreenImage(Point ps, Point pe, Bitmap baseMap)
        {
            //新建一个图片，让它与屏幕图片相同
            Bitmap copyBmp = (Bitmap)baseMap.Clone();
            //鼠标按下时的坐标
            Point newPoint = new Point(pe.X, pe.Y);

            //新建画板和画笔
            Graphics g = Graphics.FromImage(copyBmp);
            Pen p = new Pen(Color.Azure, 1);//画笔的颜色为azure 宽度为1

            //获取矩形的长度 
            int width = Math.Abs(ps.X - pe.Y);
            int height = Math.Abs(ps.Y - pe.Y);
            newPoint.X = ps.X < pe.X ? ps.X : newPoint.X;
            newPoint.Y = ps.Y < pe.Y ? ps.Y : newPoint.Y;

            var rc = new Rectangle(newPoint, new Size(width, height));
            var captureImg = CaptureImage(baseMap, newPoint, rc);

            //释放目前的画板
            g.Dispose();
            p.Dispose();
            return captureImg;
        }

        private Bitmap ShotFullScreen()
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
        public Bitmap CaptureImage(Bitmap baseMap, Point psrc, Rectangle rc)
        {
            //创建新图位图
            Bitmap bitmap = new Bitmap(rc.Width, rc.Height);
            //创建作图区域
            Graphics graphic = Graphics.FromImage(bitmap);
            //截取原图相应区域写入作图区
            graphic.DrawImage(baseMap, 0,0, rc, GraphicsUnit.Pixel);
            Clipboard.SetImage(bitmap);
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

    public class Line
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
    }

    public class MultiLine
    {
        public List<Line> Lines { get; set; }
        public List<Point> Points { get; set; }
    }
}
