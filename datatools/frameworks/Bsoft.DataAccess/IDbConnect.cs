using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bsoft.Data
{
    public interface IDbConnect
    {
        System.Data.DataTable ExecuteDataTable(string sql);

        int ExecuteNonQuery(string p);

        bool Open();

        System.Data.ConnectionState State { get; }
    }
}
