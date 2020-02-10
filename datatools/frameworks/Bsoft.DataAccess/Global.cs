using Bsoft.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bsoft.DataAccess;


namespace Bsoft.Data
{
    public static class Global
    {
        public static DBConnect Db { get; set; }
        public static DBStructure Ds { get; set; }

        public static DatabaseType dbKind
        {
            set { Ds.dbKind = value; }
            get { return Ds.dbKind; }
        }

        public static void StartConnection(string p, DatabaseType databaseType)
        {
           
            Db = new DBConnect();
            Db.ConnectionString = p;
            Ds = new DBStructure(Db.ConnectionString);
            Ds.Con = Db;
            dbKind = databaseType;
        }
    }


}
