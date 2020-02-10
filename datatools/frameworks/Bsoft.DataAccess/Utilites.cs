using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Bsoft.Common;
using Bsoft.Data;

namespace Bsoft.DataAccess
{
    public class Utilites
    {
        public static void Export(string tableName, string folderPath, bool hasTypeInformation)
        {
            DataTable dt = Global.Db.ExecuteDataTable("select * from " + tableName);
            if (hasTypeInformation)
            {
                List<string> types = GetColumnsAndDetails(tableName);
                DataTable newDt = new DataTable();
                foreach (DataColumn cols in dt.Columns)
                {
                    newDt.Columns.Add(cols.ColumnName);
                }
                newDt.Rows.Add(types.ToArray());
                foreach (DataRow rows in dt.Rows)
                {
                    newDt.Rows.Add(rows.ItemArray);
                }
                dt = newDt;
            }
            using (StreamWriter writer = File.CreateText(Path.Combine(folderPath, tableName + ".csv")))
            {
                CsvReader.WriteCSV(dt, writer, true);
            }
        }

        public static void Import(string filePath, bool hasTypeInformation,bool dropIfExsit)
        {
            string tableName = Path.GetFileNameWithoutExtension(filePath);
            if (dropIfExsit)
            {
                Global.Ds.DropIfExist(tableName);
            }
            DataTable dt = GetTypedDataTable(filePath, false);
            dt.TableName = tableName;
            if (hasTypeInformation)
            {
                Global.Ds.CreateTableFirstRowIsType(dt, false);
                DataTable dtNew = dt.Clone();
                //skip first row
                bool first = true;
                foreach (DataRow dr in dt.Rows)
                {
                    if (!first)
                    {
                        dtNew.Rows.Add(dr.ItemArray);
                    }
                    first = false;
                }
                dt = dtNew;
            }
            Global.Db.BulkCopy(dt);
        }

        private static DataTable GetTypedDataTable(string filePath, bool hasTypeInformation)
        {
            DataTable dt = CsvReader.TransferCSVToTable(filePath);
            if (hasTypeInformation)
            {
                DataTable dtNew = new DataTable();
                foreach (DataColumn dcol in dt.Columns)
                {
                    Type t = Type.GetType(dt.Rows[0][dcol].ToString());
                    dtNew.Columns.Add(dcol.ColumnName, t);
                }
                bool first = true;
                foreach (DataRow dr in dt.Rows)
                {
                    if (!first)
                    {
                        dtNew.Rows.Add(dr.ItemArray);
                    }
                    first = false;
                }
                dt = dtNew;
            }
            return dt;
        }

        private static List<string> GetColumnsAndDetails(string tableName)
        {
            List<string> tbls = new List<string>();
            string sql = @"select 
        data_type + case data_type
            when 'sql_variant' then ''
            when 'text' then ''
            when 'ntext' then ''
            when 'xml' then ''
            when 'decimal' then '(' + cast(numeric_precision as varchar) + ', ' + cast(numeric_scale as varchar) + ')'
            else coalesce('('+case when character_maximum_length = -1 then 'MAX' else cast(character_maximum_length as varchar) end +')','') end + ' ' +
        case when exists ( 
        select id from syscolumns
        where object_name(id)=table_name
        and name=column_name
        and columnproperty(id,name,'IsIdentity') = 1 
        ) then
        'IDENTITY(' + 
        cast(ident_seed(table_name) as varchar) + ',' + 
        cast(ident_incr(table_name) as varchar) + ')'
        else ''
        end + ' ' +
         (case when IS_NULLABLE = 'No' then 'NOT ' else '' end ) + 'NULL ' + 
          case when information_schema.columns.COLUMN_DEFAULT IS NOT NULL THEN 'DEFAULT '+ information_schema.columns.COLUMN_DEFAULT ELSE '' END 
 from   information_schema.columns where table_name = '{0}'


 ".With(tableName);

            DataTable dt = Global.Db.ExecuteDataTable(sql);
            var v = (from DataRow dr in dt.Rows
                     select dr[0].ToString()).ToList();
            return v;
        }
    }
}
