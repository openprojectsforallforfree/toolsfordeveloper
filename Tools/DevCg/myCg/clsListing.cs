using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using ICg;
namespace myCg
{
    
    public class clsListing : BasicFrame, Frame
    {
        public override void GenCreate_Beforelines()
        {
            outText.AppendLine("TableName = \"" + tablename +   "\";");
        }
        public override void GenCreate_loop_Fistline()
        {
            if (type.ToLower() == "nodb")
            {
                return;
            }
            outText.AppendFormat("DataTableColumns.Add( \"" + "{0}" + "\",{1}," + "\"{2}" + "\",true ,true);", dbName, crudeType, colName);
            outText.AppendLine();
        }
        public override void GenCreate_loop()
        {
            if (type.ToLower() == "nodb")
            {
                return;
            }
            outText.AppendFormat("DataTableColumns.Add( \"" + "{0}\"" + ",{1},\"" + "{2}\""   + ");", dbName, crudeType, colName);
            outText.AppendLine();
        }
        public override void GenCreate_loop_Lastline()
        {
            GenCreate_loop();
        }
        public override void GenCreate_Afterlines()
        {

        }
    }
}
