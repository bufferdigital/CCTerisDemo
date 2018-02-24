using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QrDemo.QrGridControl
{
    public partial class frmGridCtrl : Form
    {
        public frmGridCtrl()
        {
            InitializeComponent();

            this.gridView1.OptionsSelection.MultiSelect = true;        
            ////如何显示水平滚动条
            this.gridView1.OptionsView.ColumnAutoWidth = true;
            ////列表宽度自适应内容
            //gridView1.BestFitColumns();
            ////设置成一次选择一行，并且不能被编辑
            //this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            //this.gridView1.OptionsBehavior.Editable = false;
            //this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            ////让各列头禁止排序
            //gridView1.OptionsCustomization.AllowSort = false;
            ////拖动滚动条时固定某一列
            ////设置Columns，选择要固定的列。设置Fixed属性，可以选择：固定在左边、固定在右边、不固定。
            //让GridControl的"查找"框一直显示
            //this.gridView1.OptionsFind.AlwaysVisible = true;
            gridView1.Appearance.SelectedRow.BackColor = Color.FromArgb(150, Color.RoyalBlue);
            gridView1.Appearance.FocusedRow.BackColor = Color.FromArgb(150, Color.RoyalBlue);
            gridView1.Appearance.HideSelectionRow.BackColor = Color.FromArgb(150, Color.RoyalBlue);
            gridView1.Appearance.EvenRow.BackColor = Color.FromArgb(150, 192, 255, 192);
            //gridView1.Appearance.OddRow.BackColor = SystemColors.ControlText;
            gridView1.OptionsView.EnableAppearanceEvenRow = true;
            //gridView1.OptionsView.EnableAppearanceOddRow = true;

            this.gridView1.RowHeight = 30;
            var font = this.gridView1.Appearance.Row.Font;
            var newfont = new Font(font.FontFamily, 12, FontStyle.Regular);
            this.gridView1.Appearance.Row.Font = newfont;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable m_dtDataSrc = new DataTable();
            m_dtDataSrc.Columns.Add("名称", typeof(string));
            m_dtDataSrc.Columns.Add("英文简称", typeof(string));
            m_dtDataSrc.Rows.Add(new object[] { "wo","12312321"});
            m_dtDataSrc.Rows.Add(new object[] { "ni", "44555555" });
            this.gridControl1.DataSource = m_dtDataSrc;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.gridView1.FocusedRowHandle = 1;
            this.gridView1.ClearSelection();
            this.gridView1.SelectRow(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var row = this.gridView1.GetFocusedDataRow();
            this.textEdit1.Text = row[0] + "";
        }
    }
}
