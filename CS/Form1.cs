using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraGrid.Columns;

namespace WindowsApplication767 {
    public partial class Form1 : Form
    {
                private DataTable CreateTable(int RowCount)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Name", typeof(string));
            tbl.Columns.Add("Date", typeof(DateTime));
            for (int i = 0; i < RowCount; i++)
                tbl.Rows.Add(new object[] { String.Format("Name{0}", i), DateTime.Now.AddDays(i) });
            return tbl;
        }
        

        public Form1()
        {
            InitializeComponent();
            gridControl1.DataSource = CreateTable(20);
        }

        public string GetError(object value, int rowHandle, GridColumn column)
        {
            if (value == null) return "value is null";
            if (!(value is int)) return string.Empty; ;
            if ((int)value % 2 == 0) return "value is even";
            if ((int)value < 10) return "value < 10";
            return string.Empty;
        }

        void SetError(BaseEditViewInfo cellInfo, string errorIconText)
        {
            if (errorIconText == string.Empty) return;
            cellInfo.ErrorIconText = errorIconText;
            cellInfo.ShowErrorIcon = true;
            cellInfo.FillBackground = true;
            cellInfo.ErrorIcon = DXErrorProvider.GetErrorIconInternal(ErrorType.Critical);
        }


        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            BaseEditViewInfo info = ((GridCellInfo)e.Cell).ViewInfo;
            string error = GetError(e.CellValue, e.RowHandle, e.Column);
            SetError(info, error);
            info.CalcViewInfo(e.Graphics);
        }


        private void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData) e.Value = e.RowHandle;
        }

        private void gridView1_ShownEditor(object sender, EventArgs e)
        {
            string error = GetError(gridView1.GetFocusedValue(), gridView1.FocusedRowHandle, gridView1.FocusedColumn);
            if (error == string.Empty) return;
            gridView1.SetColumnError(gridView1.FocusedColumn, error);
        }
    }

}