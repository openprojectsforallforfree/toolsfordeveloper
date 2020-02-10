using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ICg;

namespace myCg
{
   
    public class clsEntry : BasicFrame, Frame
    {

        public override void GenCreate_Beforelines()
        {
            outText.AppendLine("TableName = \"" +  tablename  +"\";" );
        }
        public override void GenCreate_loop_Fistline()
        {
            if (type.ToLower() == "nodb")
            {
                return;
            }
            outText.AppendFormat("ColumnList.AddNewEditColumn(\"{0}\",{1}, {2} ,true ,false );", dbName, crudeType, controlName);
            outText.AppendLine();
        }
        public override void GenCreate_loop()
        {
            if (type.ToLower() == "nodb")
            {
                return;
            }
            outText.AppendFormat("ColumnList.AddNewEditColumn(\"{0}\",{1}, {2} );", dbName, crudeType, controlName);
            if (cntrol == "lblComboBox")
            {
                outText.AppendLine();
                outText.AppendFormat("ComboBoxDataLoader.LoadData(\"Id\", \"Title\", \"{1}\", {0}.cmbBox , true);", controlName, constraint);
            }
            if (cntrol == "ComboBox")
            {
                outText.AppendLine();
                outText.AppendFormat("ComboBoxDataLoader.LoadData(\"Id\",\"Title\",\"{1}\", {0}, true);", controlName, constraint);
            }
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
