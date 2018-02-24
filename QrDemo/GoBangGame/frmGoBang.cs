using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QrDemo.GoBangGame
{
    public partial class frmGoBang : Form
    {
        Point[,] m_aryBoard = new Point[11, 11];
        List<Point> m_lstPieces = new List<Point>();
        Point m_potMove = new Point();
        int CellWidth = 30;
        int PieceWidth = 20;
        public frmGoBang()
        {
            InitializeComponent();
            Point location = new Point(100, 100);
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    m_aryBoard[i, j] = new Point(location.X + j * CellWidth, location.Y + i * CellWidth);
                }
            }
            this.MouseMove += FrmGoBang_MouseMove;
            this.MouseClick += FrmGoBang_MouseClick;
        }

        private void FrmGoBang_MouseClick(object sender, MouseEventArgs e)
        {
            Point blocation = GetPosition(e.Location);
            if (blocation.X >= 0 && blocation.Y >= 0)
            {
                m_lstPieces.Add(blocation);
                Invalidate();
            }
        }

        private void FrmGoBang_MouseMove(object sender, MouseEventArgs e)
        {
            m_potMove = e.Location;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            Pen pen = new Pen(Color.Black);
            DrawBoard(e.Graphics, pen);
            Pen pen1 = new Pen(Color.Red);
            DrawTrace(e.Graphics, pen1, m_potMove);
            DrawPieces(e.Graphics, 1);
        }

        private void DrawBoard(Graphics g, Pen pen)
        {
            int width = CellWidth;
            Point location = new Point(100, 100);
            ////横线
            //for (int i = 0; i < 11; i++)
            //{
            //    g.DrawLine(pen, new Point(location.X, location.Y + i * width),
            //        new Point(location.X + (n - 1) * width, location.Y + i * width));
            //}
            ////竖线
            //for (int i = 0; i < 11; i++)
            //{
            //    g.DrawLine(pen, new Point(location.X + i * width, location.Y),
            //        new Point(location.X + i * width, location.Y + (n - 1) * width));
            //}
            GraphicsPath gp = new GraphicsPath();
            for (int i = 0; i < 11; i++)
            {
                gp.StartFigure();
                gp.AddLine(m_aryBoard[i, 0], m_aryBoard[i, 10]);
                gp.CloseFigure();
                gp.AddLine(m_aryBoard[0, i], m_aryBoard[10, i]);
            }
            g.DrawPath(pen, gp);
            return;
        }

        private void DrawPieces(Graphics g, int iType)
        {
            Color clr = iType == 1 ? Color.Black : Color.White;
            SolidBrush sb = new SolidBrush(clr);
            int r = PieceWidth / 2;
            foreach (var pot in m_lstPieces)
            {
                g.FillEllipse(sb, new Rectangle(pot.X - r, pot.Y - r, PieceWidth, PieceWidth));
            }
            return;
        }

        private void DrawTrace(Graphics g, Pen pen, Point pos)
        {
            int w = 1 * CellWidth / 3;
            Point blocation = GetPosition(pos);
            if (blocation.X >= 0 && blocation.Y >= 0)
            {
                Point pot1 = new Point(blocation.X - PieceWidth, blocation.Y - PieceWidth);
                Point pot12 = new Point(pot1.X + w, pot1.Y);
                Point pot13 = new Point(pot1.X, pot1.Y + w);

                Point pot2 = new Point(blocation.X + PieceWidth, blocation.Y - PieceWidth);
                Point pot22 = new Point(pot2.X - w, pot2.Y);
                Point pot23 = new Point(pot2.X, pot2.Y + w);

                Point pot3 = new Point(blocation.X + PieceWidth, blocation.Y + PieceWidth);
                Point pot32 = new Point(pot3.X - w, pot3.Y);
                Point pot33 = new Point(pot3.X, pot3.Y - w);

                Point pot4 = new Point(blocation.X - PieceWidth, blocation.Y + PieceWidth);
                Point pot42 = new Point(pot4.X + w, pot4.Y);
                Point pot43 = new Point(pot4.X, pot4.Y - w);

                g.DrawLines(pen, new Point[] { pot12, pot1, pot13 });
                g.DrawLines(pen, new Point[] { pot22, pot2, pot23 });
                g.DrawLines(pen, new Point[] { pot32, pot3, pot33 });
                g.DrawLines(pen, new Point[] { pot42, pot4, pot43 });
                g.FillEllipse(new SolidBrush(Color.Red), new Rectangle(blocation.X - 5, blocation.Y - 5, 10, 10));
            }
            return;
        }

        private Point GetPosition(Point pos)
        {
            Point blocation = new Point(-1, -1);
            int mx = (pos.X - 100) / CellWidth;
            int nx = (pos.X - 100) % CellWidth;
            int x = (nx * 1.0 / CellWidth) <= 0.5 ? mx : mx + 1;
            int my = (pos.Y - 100) / CellWidth;
            int ny = (pos.Y - 100) % CellWidth;
            int y = (ny * 1.0 / CellWidth) <= 0.5 ? my : my + 1;         
            if (x >= 0 && x <= 10 && y >= 0 && y <= 10)
            {
                blocation = m_aryBoard[y, x];
            }
            return blocation;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Point pot = new Point(100, 100);
            Graphics g = this.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            DrawBoard(g, new Pen(Color.Blue));
            //DrawPieces(g, pot, 1);
            //g.FillPie(new SolidBrush(Color.Red), pot.X, pot.Y, 10, 10, 0, 360);
            return;
        }

        public void Clear()
        {

        }
    }
}
