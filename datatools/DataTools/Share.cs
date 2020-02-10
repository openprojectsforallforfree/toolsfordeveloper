using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataTools
{
   public class Share
    {
        public static  DataTable getdDatatable(DataSet ds)
        {
            DataTable finaldt = new DataTable();
            DataTable tempfinaldt = new DataTable();
            foreach (DataTable dt in ds.Tables)
            {
                if (dt.Columns.Count == 10)
                {
                    tempfinaldt = dt;
                    break;
                }
            }
            //format
            foreach (DataColumn dc in tempfinaldt.Columns)
            {
                finaldt.Columns.Add(tempfinaldt.Rows[0][dc].ToString());
            }
            int colcount = 0;
            for (int i = 0; i < tempfinaldt.Rows.Count - 1; i++)
            {
                colcount = 0;
                finaldt.Rows.Add();
                foreach (DataColumn dc in tempfinaldt.Columns)
                {
                    string value = tempfinaldt.Rows[i + 1][dc].ToString();
                    string insting = "";
                    insting = Regex.Match(value, @"\>([^7]*)\<").Groups[1].Value.ToString();


                    if (insting.Trim() == "")
                    {
                        insting = value;
                    }
                    finaldt.Rows[i][colcount++] = insting;
                }
            }
            return finaldt;
        }

        public static DataTable getdDatatableSS(DataSet ds)
        {
            DataTable finaldt = new DataTable();
            DataTable tempfinaldt = new DataTable();
            foreach (DataTable dt in ds.Tables)
            {
                if (dt.Columns.Count == 9)
                {
                    tempfinaldt = dt;
                    break;
                }
            }
            //format
            foreach (DataColumn dc in tempfinaldt.Columns)
            {
                finaldt.Columns.Add(tempfinaldt.Rows[0][dc].ToString());
            }
            int colcount = 0;
            for (int i = 0; i < tempfinaldt.Rows.Count - 1; i++)
            {
                colcount = 0;
                finaldt.Rows.Add();
                foreach (DataColumn dc in tempfinaldt.Columns)
                {
                    string value = tempfinaldt.Rows[i + 1][dc].ToString();
                    string insting = "";
                    insting = Regex.Match(value, @"\>([^7]*)\<").Groups[1].Value.ToString();


                    if (insting.Trim() == "")
                    {
                        insting = value;
                    }
                    finaldt.Rows[i][colcount++] = insting;
                }
            }
            return finaldt;
        }
    }
}
