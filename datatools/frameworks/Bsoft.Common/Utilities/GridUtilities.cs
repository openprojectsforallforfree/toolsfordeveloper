using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Bsoft.Common.Utilities
{
    public class GridUtility
    {
        public static DataSet getDataTable(DataGridView grd)
        {
            //By Dhiraj October 2008

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            DataRow dr;

            for (int col = 0; col < grd.Columns.Count; col++)
            {
                dt.Columns.Add(grd.Columns[col].Name.ToString());
            }

            for (int row = 0; row < grd.RowCount; row++)
            {
                dr = dt.NewRow();
                for (int col = 0; col < grd.Columns.Count; col++)
                {
                    dr[col] = grd.Rows[row].Cells[col].Value;
                }
                dt.Rows.Add(dr);
            }

            dt.AcceptChanges();
            ds.Tables.Add(dt);
            return ds;
        }

        public void SetProperties(DataGridView dataGridView)
        {
            dataGridView.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.CellEndEdit += dataGridView1_CellEndEdit;
            dataGridView.EditingControlShowing += DataGridView1_EditingControlShowing;
            dataGridView.CellEnter += DataGridView_CellEnter;
            dataGridView.CellLeave += DataGridView_CellLeave;
        }

        public static void SetGridRowSerialNo(DataGridView grid)
        {
            try
            {
                int cnt = 1;
                foreach (DataGridViewRow dr in grid.Rows)
                {
                    dr.HeaderCell.Value = (cnt++).ToString();
                }
            }
            catch { }
        }

        private void DataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCellStyle fooCellStyle = new DataGridViewCellStyle();
            fooCellStyle.BackColor = System.Drawing.Color.Gold;
            ((DataGridView)sender).CurrentCell.Style.ApplyStyle(fooCellStyle);
        }

        private void DataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCellStyle barCellStyle = new DataGridViewCellStyle();
            bool even = ((DataGridView)sender).CurrentCell.RowIndex % 2 == 0;
            if (even)
            {
                barCellStyle.BackColor = ((DataGridView)sender).DefaultCellStyle.BackColor;
            }
            else
            {
                barCellStyle.BackColor = ((DataGridView)sender).AlternatingRowsDefaultCellStyle.BackColor;
            }
            ((DataGridView)sender).CurrentCell.Style.ApplyStyle(barCellStyle);
        }

        #region Validation allow only numbers

        private void DataGridView1_EditingControlShowing(object sender, System.Windows.Forms.DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox)
            {
                ((TextBox)e.Control).KeyPress -= TextBox_KeyPress;
                ((TextBox)e.Control).KeyPress += TextBox_KeyPress;
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ("0123456789.".IndexOf(e.KeyChar) == -1)
            {
                if (e.KeyChar != Convert.ToChar(Keys.Back))
                {
                    e.Handled = true;
                }
            }
            if (((TextBox)sender).Text.Contains('.') && e.KeyChar == '.')
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null || ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "")
            {
                ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
            }
        }

        #endregion Validation allow only numbers

        /// <summary>
        /// Sets datatble to existing designed grid for display
        /// all columns in grid must also be in datatable with same name other wise exception is thrown
        ///
        /// </summary>
        /// <param name="datatable"></param>
        /// <param name="dataGridView"></param>
        public static void SetGrid(DataTable datatable, DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            foreach (DataRow row in datatable.Rows)
            {
                List<string> list = new List<string>();
                foreach (DataGridViewColumn item in dataGridView.Columns)
                {
                    list.Add(row[item.Name].ToString());
                }
                dataGridView.Rows.Add(list.ToArray());
            }
        }

        /// <summary>
        /// Returns datable from datagridview
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(DataGridView dataGridView)
        {
            DataTable dt = new DataTable();
            foreach (DataGridViewColumn item in dataGridView.Columns)
            {
                dt.Columns.Add(item.Name);
            }

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                List<string> list = new List<string>();
                foreach (DataGridViewColumn item in dataGridView.Columns)
                {
                    if (row.Cells[item.Name].Value == null)
                    {
                        list.Add("");
                    }
                    else
                    {
                        list.Add(row.Cells[item.Name].Value.ToString());
                    }
                }
                dt.Rows.Add(list.ToArray());
            }
            return dt;
        }

        public static void UpdateARow(DataGridView dataGridView, int row, string columnName, object value)
        {
            dataGridView.Rows[row].Cells[columnName].Value = value.ToString();
        }

        public static string GetValue(DataGridView dataGridView, int row, string columnName)
        {
            return dataGridView.Rows[row].Cells[columnName].Value.ToString();
        }

        public static void Increase(DataGridView dataGridView, string ColumnName, int row)
        {
            GridUtility.UpdateARow(dataGridView, row, ColumnName, Convert.ToDouble(GridUtility.GetValue(dataGridView, row, ColumnName)) + 1);
        }

        public static void Decrease(DataGridView dataGridView, string ColumnName, int row)
        {
            if ((Convert.ToDouble(GridUtility.GetValue(dataGridView, row, ColumnName)) - 1) > 0)
            {
                GridUtility.UpdateARow(dataGridView, row, ColumnName, Convert.ToDouble(GridUtility.GetValue(dataGridView, row, ColumnName)) - 1);
            }
        }

        public static void Increase(DataGridView dataGridView, string ColumnName, int row, decimal qty)
        {
            GridUtility.UpdateARow(dataGridView, row, ColumnName, Conversion.ToDecimal(GridUtility.GetValue(dataGridView, row, ColumnName)) + qty);
        }

        public static decimal GetSum(DataTable datatable, string ColumnName)
        {
            decimal total = 0;

            foreach (DataRow row in datatable.Rows)
            {
                total += Conversion.ToDecimal(row, ColumnName);
            }
            return total;
        }

        public static decimal GetSum(DataGridView dataGridView, string ColumnName)
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                total += Conversion.ToDecimal(row, ColumnName);
            }
            return total;
        }

        public static decimal GetSum(DataGridView dataGridView, string ColumnName, int fromIndex, int toIndex)
        {
            decimal total = 0;
            if (fromIndex < 0)
            {
                fromIndex = 0;
            }
            if (toIndex >= dataGridView.Rows.Count)
            {
                toIndex = dataGridView.Rows.Count - 1;
            }
            for (int i = fromIndex; i <= toIndex; i++)
            {
                total += Conversion.ToDecimal(dataGridView.Rows[i], ColumnName);
            }
            return total;
        }

        public static void SetDataGridViewProperties(DataGridView dataGridView)
        {
            DataGridViewCellStyle dataGridRowStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridColumnHeaderCellStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridCellStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewRowHeaderCellStyle = new DataGridViewCellStyle();
            dataGridView.ColumnHeadersHeight = 35;
            dataGridView.RowHeadersWidth = 34;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToResizeRows = true;
            dataGridView.MultiSelect = false;

            dataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(223)))), ((int)(((byte)(224)))));
            dataGridView.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;

            dataGridView.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;//makes slow
            dataGridView.StandardTab = true; //required...
            ////Column Header Style
            dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridColumnHeaderCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridColumnHeaderCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(65)))), ((int)(((byte)(109)))));
            dataGridColumnHeaderCellStyle.Font = dataGridView.ColumnHeadersDefaultCellStyle.Font;
            dataGridColumnHeaderCellStyle.ForeColor = System.Drawing.Color.White;
            dataGridColumnHeaderCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridColumnHeaderCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridColumnHeaderCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridView.ColumnHeadersDefaultCellStyle = dataGridColumnHeaderCellStyle;

            ////Cell Style
            dataGridCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(223)))), ((int)(((byte)(224)))));
            dataGridCellStyle.Font = dataGridView.DefaultCellStyle.Font;
            dataGridCellStyle.ForeColor = System.Drawing.Color.Black;
            dataGridCellStyle.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dataGridCellStyle.NullValue = string.Empty;
            dataGridView.DefaultCellStyle = dataGridCellStyle;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridRowStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            //dataGridRowStyle.BackColor = System.Drawing.Color.LightGray;
            dataGridView.AlternatingRowsDefaultCellStyle = dataGridRowStyle;

            //Row Header Style
            dataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewRowHeaderCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewRowHeaderCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(65)))), ((int)(((byte)(109)))));
            //dataGridViewRowHeaderCellStyle.Font = dataGridView.RowHeadersDefaultCellStyle.Font;
            //dataGridViewRowHeaderCellStyle.Font = new System.Drawing.Font("Shangrila Numeric", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // dataGridViewRowHeaderCellStyle.Font = Bsoft.Design.FormDesginBase.DigitFont;
            dataGridViewRowHeaderCellStyle.ForeColor = System.Drawing.Color.White;
            dataGridViewRowHeaderCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewRowHeaderCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewRowHeaderCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridView.RowHeadersDefaultCellStyle = dataGridViewRowHeaderCellStyle;
        }
    }

    public class GridSizeUtility
    {
        public void AdjustHeight(DataGridView dataGridView)
        {
            int totalRowHeight = dataGridView.ColumnHeadersHeight;
            if (rowHeight == 0)
            {
                if (dataGridView.Rows.Count > 0)
                {
                    rowHeight = dataGridView.Rows[0].Height;
                }
            }
            // foreach (DataGridViewRow row in dataGridView1.Rows)
            totalRowHeight += (rowHeight * dataGridView.Rows.Count);
            dataGridView.Height = totalRowHeight + 1;
        }

        public void AdjustWidth(DataGridView dataGridView)
        {
            if (totalColumnHeight == 0)
            {
                totalColumnHeight = dataGridView.RowHeadersWidth;
                foreach (DataGridViewColumn col in dataGridView.Columns)
                    totalColumnHeight += col.Width;
                dataGridView.Width = totalColumnHeight + 2;
            }
        }

        public void AdjustSize(DataGridView dataGridView)
        {
            int totalRowHeight = dataGridView.ColumnHeadersHeight;
            if (rowHeight == 0)
            {
                if (dataGridView.Rows.Count > 0)
                {
                    rowHeight = dataGridView.Rows[0].Height;
                }
            }
            // foreach (DataGridViewRow row in dataGridView1.Rows)
            totalRowHeight += (rowHeight * dataGridView.Rows.Count);
            dataGridView.Height = totalRowHeight + 2;
            if (totalColumnHeight == 0)
            {
                totalColumnHeight = dataGridView.RowHeadersWidth;
                foreach (DataGridViewColumn col in dataGridView.Columns)
                    totalColumnHeight += col.Width;
                dataGridView.Width = totalColumnHeight + 2;
            }
        }

        private int rowHeight = 0;
        private int totalColumnHeight = 0;

        public static void AdjustCalculatedHeight(DataGridView dataGridView)
        {
            int totalRowHeight = dataGridView.ColumnHeadersHeight;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Visible)
                {
                    totalRowHeight += row.Height;
                }
            }
            dataGridView.Height = totalRowHeight + 1;
        }
    }
}