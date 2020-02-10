using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Bsoft.Common
{
    public class DataTableUtility
    {
        public static DataTable SwapRowAndColumn(DataTable dt)
        {
            DataTable dtnew = new DataTable();
            dtnew.Columns.Add("Name");
            dtnew.Columns.Add("Value");
            foreach (DataColumn dc in dt.Columns)
            {
                dtnew.Rows.Add(dc.ColumnName, dt.Rows[0][dc].ToString());
            }
            return dtnew;
        }

        public static void SetListingGrid(DataGridView dgv, DataTable dt)
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            if (dgv.Columns.Count < 1)
            {
                dgv.Columns.Add("Title", "Value");
            }
            //dgv.ColumnHeadersVisible = false;
            foreach (DataColumn dc in dt.Columns)
            {
                dgv.Rows.Add(dt.Rows[0][dc].ToString());
                dgv.Rows[dgv.Rows.Count - 1].HeaderCell.Value = dc.ColumnName;
            }
        }
    }

    /// <summary>
    /// set limit of result count
    /// </summary>
    public class DataTableSearchClass
    {
        private List<string> index = new List<string>();
        private DataTable _dt = new DataTable();
        private string s = string.Empty;
        public int Limit = -1;

        public DataTableSearchClass(DataTable dtIn)
        {
            _dt = dtIn;
            foreach (DataRow item in dtIn.Rows)
            {
                s = string.Empty;
                for (int i = 0; i < dtIn.Columns.Count; i++)
                {
                    s += item[i].ToString();
                }
                index.Add(s.ToLower());
            }
        }

        public DataTable Search(string searchString)
        {
            searchString = searchString.ToLower();
            List<int> searched = new List<int>();
            DataTable dtout = _dt.Clone();
            for (int i = 0; i < index.Count; i++)
            {
                if (index[i].Contains(searchString))
                {
                    searched.Add(i);
                }
                if (Limit != -1 && i == Limit - 1)
                {
                    break;
                }
            }
            //make output
            foreach (var item in searched)
            {
                dtout.ImportRow(_dt.Rows[item]);
            }
            //dtout.AcceptChanges();
            return dtout;
        }
    }
}