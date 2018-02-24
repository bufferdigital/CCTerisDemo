using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QrDemo.Teris
{
    public partial class TerisDemo : Form
    {
        Point m_potCenter = new System.Drawing.Point(200, 100);
        Point m_Location = new Point(100, 100);
        TerisTen m_curTen;
        System.Windows.Forms.Timer m_timerRun = new System.Windows.Forms.Timer();
        int left = 100, right = 300, top = 100, bottom = 500;
        List<TerisUnit> m_lstExistUnit = new List<TerisUnit>();
        public TerisDemo()
        {
            InitializeComponent();
            this.m_timerRun.Interval = 500;
            this.m_timerRun.Tick += M_timerRun_Tick;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Down)
            {
                Run(new Point(m_curTen.Location.X, m_curTen.Location.Y + CommonUtils.SquareSideLength));
            }
            else if (keyData == Keys.Up)
            {
                m_curTen.Type += 1;
                Run(new Point(m_curTen.Location.X, m_curTen.Location.Y));
            }
            else if (keyData == Keys.Left)
            {
                Run(new Point(m_curTen.Location.X - CommonUtils.SquareSideLength, m_curTen.Location.Y));
            }
            else if (keyData == Keys.Right)
            {
                Run(new Point(m_curTen.Location.X + CommonUtils.SquareSideLength, m_curTen.Location.Y));
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void M_timerRun_Tick(object sender, EventArgs e)
        {
            bool brtn = Run(new Point(m_curTen.Location.X, m_curTen.Location.Y + CommonUtils.SquareSideLength));
            if (brtn)
            {
                m_lstExistUnit.AddRange(m_curTen.Units);
                RandModel();
                Run(new Point(m_curTen.Location.X, m_curTen.Location.Y));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Pen p = new Pen(Color.Red, 2);
            SolidBrush sb = new SolidBrush(Color.Green);
            Point p1 = new Point(100, 100);
            Point p2 = new Point(300, 100);
            Point p3 = new Point(100, 500);
            Point p4 = new Point(300, 500);
            Graphics g = e.Graphics;

            g.DrawLine(p, new Point(p1.X, p1.Y), new Point(p2.X, p2.Y));
            g.DrawLine(p, new Point(p1.X, p1.Y), new Point(p3.X, p3.Y));
            for (int i = 0; i < 40; i++)
            {
                int height = (i + 1) * 10;
                g.DrawLine(p, new Point(p1.X, p1.Y + height), new Point(p2.X, p2.Y + height));
            }
            for (int i = 0; i < 20; i++)
            {
                int width = (i + 1) * 10;
                g.DrawLine(p, new Point(p1.X + width, p1.Y), new Point(p3.X + width, p3.Y));
            }

            for (int i = 0; i < this.m_lstExistUnit.Count; i++)
            {
                    g.FillPolygon(sb, m_lstExistUnit[i].Points);
            }
            if (m_curTen == null) return;
            for (int i = 0; i < m_curTen.Units.Count; i++)
                g.FillPolygon(sb, m_curTen.Units[i].Points);
        }
        //start
        private void button1_Click(object sender, EventArgs e)
        {
            RandModel();
            this.m_timerRun.Start();
            //Run();
        }
        //stop
        private void button2_Click(object sender, EventArgs e)
        {

        }
        //pause
        private void button3_Click(object sender, EventArgs e)
        {
            this.m_timerRun.Stop();
        }

        private bool Run(Point potNew)
        {
            var lstNextTen = m_curTen.GetUnits(potNew, m_curTen.Type);
            bool bIsNotCollisionDetection = CollisionDetection(lstNextTen);
            if (bIsNotCollisionDetection)
            {
                m_curTen.SetLocation(potNew);
                m_curTen.SetUnits(m_curTen.Type);
                Invalidate(new Rectangle(m_Location, new Size(200, 400)));
            }
            return !bIsNotCollisionDetection;
        }
        //随机生成模型
        private void RandModel()
        {
            Random rand = new Random();
            int iType = rand.Next(0, 3);
            m_curTen = new TerisTen(m_potCenter, iType);
            return;
        }
        private bool CollisionDetection(List<TerisUnit> lstUnits)
        {
            for (int i = 0; i < lstUnits.Count; i++)
            {
                var unit = lstUnits[i];
                if (this.m_lstExistUnit.Exists(v=>v.Equals(unit)))
                {
                    return false;
                }
                for (int j = 0; j < unit.Points.Length; j++)
                {
                    var pot = unit.Points[j];
                    if (!(pot.X >= left && pot.X <= right && pot.Y >= top && pot.Y <= bottom))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }

    /// <summary>
    /// +
    /// </summary>
    public class TerisTen
    {
        public int Type { get; set; }
        public Point Location { get; set; }
        public List<TerisUnit> Units { get; set; }
        public TerisTen(Point location, int iType)
        {
            this.Location = location;
            this.Type = iType;
            Units = new List<TerisUnit>();
            Units.AddRange(new TerisUnit[] { new TerisUnit(), new TerisUnit(), new TerisUnit(), new TerisUnit(), });
            SetUnits(this.Type);
        }

        //设置新的位置
        public void SetLocation(Point newLocation)
        {
            Location = newLocation;
            //SetUnits(Type);
        }
        public List<TerisUnit> GetUnits(Point potNewLocation, int iNewType)
        {
            List<TerisUnit> lstUnits = new List<TerisUnit>();
            lstUnits.AddRange(new TerisUnit[] { new TerisUnit(), new TerisUnit(), new TerisUnit(), new TerisUnit(), });
            if (iNewType % 4 == 0)
            {
                lstUnits[0].Points = CommonUtils.GetUnit(new Point(potNewLocation.X, potNewLocation.Y - CommonUtils.SquareSideLength));
                lstUnits[1].Points = CommonUtils.GetUnit(new Point(potNewLocation.X, potNewLocation.Y));
                lstUnits[2].Points = CommonUtils.GetUnit(new Point(potNewLocation.X - CommonUtils.SquareSideLength, potNewLocation.Y));
                lstUnits[3].Points = CommonUtils.GetUnit(new Point(potNewLocation.X + CommonUtils.SquareSideLength, potNewLocation.Y));
            }
            else if (iNewType % 4 == 1)
            {
                lstUnits[0].Points = CommonUtils.GetUnit(new Point(potNewLocation.X, potNewLocation.Y - CommonUtils.SquareSideLength));
                lstUnits[1].Points = CommonUtils.GetUnit(new Point(potNewLocation.X, potNewLocation.Y));
                lstUnits[2].Points = CommonUtils.GetUnit(new Point(potNewLocation.X, potNewLocation.Y + CommonUtils.SquareSideLength));
                lstUnits[3].Points = CommonUtils.GetUnit(new Point(potNewLocation.X + CommonUtils.SquareSideLength, potNewLocation.Y));
            }
            else if (iNewType % 4 == 2)
            {
                lstUnits[0].Points = CommonUtils.GetUnit(new Point(potNewLocation.X, potNewLocation.Y + CommonUtils.SquareSideLength));
                lstUnits[1].Points = CommonUtils.GetUnit(new Point(potNewLocation.X, potNewLocation.Y));
                lstUnits[2].Points = CommonUtils.GetUnit(new Point(potNewLocation.X - CommonUtils.SquareSideLength, potNewLocation.Y));
                lstUnits[3].Points = CommonUtils.GetUnit(new Point(potNewLocation.X + CommonUtils.SquareSideLength, potNewLocation.Y));
            }
            else if (iNewType % 4 == 3)
            {
                lstUnits[0].Points = CommonUtils.GetUnit(new Point(potNewLocation.X, potNewLocation.Y - CommonUtils.SquareSideLength));
                lstUnits[1].Points = CommonUtils.GetUnit(new Point(potNewLocation.X, potNewLocation.Y));
                lstUnits[2].Points = CommonUtils.GetUnit(new Point(potNewLocation.X - CommonUtils.SquareSideLength, potNewLocation.Y));
                lstUnits[3].Points = CommonUtils.GetUnit(new Point(potNewLocation.X, potNewLocation.Y + CommonUtils.SquareSideLength));
            }
            return lstUnits;
        }
        public void SetUnits(int iType)
        {
            List<TerisUnit> lstUnits = new List<TerisUnit>();
            if (iType % 4 == 0)
            {
                Units[0].Points = CommonUtils.GetUnit(new Point(Location.X, Location.Y - CommonUtils.SquareSideLength));
                Units[1].Points = CommonUtils.GetUnit(new Point(Location.X, Location.Y));
                Units[2].Points = CommonUtils.GetUnit(new Point(Location.X - CommonUtils.SquareSideLength, Location.Y));
                Units[3].Points = CommonUtils.GetUnit(new Point(Location.X + CommonUtils.SquareSideLength, Location.Y));
            }
            else if (iType % 4 == 1)
            {
                Units[0].Points = CommonUtils.GetUnit(new Point(Location.X, Location.Y - CommonUtils.SquareSideLength));
                Units[1].Points = CommonUtils.GetUnit(new Point(Location.X, Location.Y));
                Units[2].Points = CommonUtils.GetUnit(new Point(Location.X, Location.Y + CommonUtils.SquareSideLength));
                Units[3].Points = CommonUtils.GetUnit(new Point(Location.X + CommonUtils.SquareSideLength, Location.Y));
            }
            else if (iType % 4 == 2)
            {
                Units[0].Points = CommonUtils.GetUnit(new Point(Location.X, Location.Y + CommonUtils.SquareSideLength));
                Units[1].Points = CommonUtils.GetUnit(new Point(Location.X, Location.Y));
                Units[2].Points = CommonUtils.GetUnit(new Point(Location.X - CommonUtils.SquareSideLength, Location.Y));
                Units[3].Points = CommonUtils.GetUnit(new Point(Location.X + CommonUtils.SquareSideLength, Location.Y));
            }
            else if (iType % 4 == 3)
            {
                Units[0].Points = CommonUtils.GetUnit(new Point(Location.X, Location.Y - CommonUtils.SquareSideLength));
                Units[1].Points = CommonUtils.GetUnit(new Point(Location.X, Location.Y));
                Units[2].Points = CommonUtils.GetUnit(new Point(Location.X - CommonUtils.SquareSideLength, Location.Y));
                Units[3].Points = CommonUtils.GetUnit(new Point(Location.X, Location.Y + CommonUtils.SquareSideLength));
            }
            return;
        }
    }
    public class TerisUnit
    {
        public Point[] Points { get; set; }
        public override bool Equals(object obj)
        {
            TerisUnit unit = obj as TerisUnit;
            return Points[0].X == unit.Points[0].X && Points[0].Y == unit.Points[0].Y;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class CommonUtils
    {
        public static int SquareSideLength = 10;
        public static int CollisionDetectionBottom = 500;
        public static Point[] GetUnit(Point Location)
        {
            return new Point[] { new Point(Location.X,Location.Y), new Point(Location.X, Location.Y+CommonUtils.SquareSideLength)
                ,new Point(Location.X + CommonUtils.SquareSideLength,Location.Y+CommonUtils.SquareSideLength),new Point(Location.X+CommonUtils.SquareSideLength,Location.Y)};
        }
    }
}
