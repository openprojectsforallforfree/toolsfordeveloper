using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 using Microsoft.VisualBasic;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using PlugIn;
namespace ClassLibrary1
{
   
    public class clsInsertSql : BasicFrame, Base
    {
        public override void GenCreate_Beforelines()
        {
            outText.AppendFormat("insert into {0}  (", tablename);
        }
        public override void GenCreate_loop_Fistline()
        {
            outText.AppendFormat("{0}", dbname);
        }

        public override void GenCreate_loop()
        {
            outText.AppendFormat(",{0}", dbname);
        }

        public override void GenCreate_loop_Lastline()
        {
            outText.AppendFormat(",{0})", dbname);
        }

        public override void GenCreate_Afterlines()
        {
            outText.AppendFormat(" values (", dbname);
        }

    }

    public class clsInsertSqlValues : BasicFrame, Base 
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

}
