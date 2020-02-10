using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Collections;
using System.Data;
using System.Diagnostics;
using ICg;
using System.Windows.Forms;



class myDataLoad : PlugIn.MyClassBase
{
    public myDataLoad()
        : base()
    {
        Menu.Text = "DataLoad";
    }
    public override void Execute()
    {
        string textout = "";
        DataSet ds = ICg.ExcelFunction.getData();
        if (ds == null)
            return;


        foreach (DataTable dt in ds.Tables)
        {
            textout = textout + "\n" + new clsInsertSql().gencreate(dt) + new clsInsertSqlValues().gencreate(dt);

        }
        try
        {
            Clipboard.SetText(textout);
        }
        catch
        {
        }

    }
}


public class clsInsertSql : BasicFrame, Frame
{
    public override void GenCreate_Beforelines()
    {
        outText.AppendFormat("insert into {0}  (", tablename);

    }
    public override void GenCreate_loop_Fistline()
    {
        outText.AppendFormat("{0}", dbName);
    }

    public override void GenCreate_loop()
    {
        outText.AppendFormat(",{0}", dbName);
    }

    public override void GenCreate_loop_Lastline()
    {
        outText.AppendFormat(",{0})", dbName);
    }

    public override void GenCreate_Afterlines()
    {
        outText.AppendFormat(" values (", dbName);
    }

}

public class clsInsertSqlValues : BasicFrame, Frame
{

    public override void GenCreate_Beforelines()
    {

    }
    public override void GenCreate_loop_Fistline()
    {
        if (isNumberType())
        {
            outText.AppendFormat("{0}", txt);
        }
        else
        {
            outText.AppendFormat("'{0}'", txt);
        }

    }

    public override void GenCreate_loop()
    {
        if (isNumberType())
        {
            outText.AppendFormat(",{0}", txt);
        }
        else
        {
            outText.AppendFormat(",'{0}'", txt);
        }

    }

    public override void GenCreate_loop_Lastline()
    {
        if (isNumberType())
        {
            outText.AppendFormat(",{0})", txt);
        }
        else
        {
            outText.AppendFormat(",'{0}')", txt);
        }
    }


    public override void GenCreate_Afterlines()
    {
    }

}

