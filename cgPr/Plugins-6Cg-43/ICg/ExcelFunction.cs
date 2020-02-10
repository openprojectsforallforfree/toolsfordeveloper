using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ICg
{
    public static class ExcelFunction
    {
        public static DataSet getData()
        {
            ExcelExtension.GetValues egv = new ExcelExtension.GetValues();
            DataSet ds = egv.getValuesFromExcel();
            return ds;
        }
    }
}
