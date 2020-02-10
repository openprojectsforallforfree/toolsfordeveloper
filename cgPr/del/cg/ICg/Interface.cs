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
namespace ICg
{
    public interface Frame
    {
        void GenCreate_Beforelines();
        void GenCreate_loop_Fistline();
        void GenCreate_loop();
        void GenCreate_loop_Lastline();
        void GenCreate_Afterlines();
        string gencreate(DataTable dt);
    }
}
