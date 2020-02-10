using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Bsoft.Common
{
    public class CsvReader
    {

        public static DataTable TransferCSVToTable(string filePath)
        {
            string tablename = Path.GetFileNameWithoutExtension(filePath);
            DataTable dt = new DataTable();
            dt.TableName = tablename;
            List<List<string>> v = ReadCSV(filePath);
            //add columns
            if (v.Count > 0)
            {
                bool colAdded = false;
                foreach (var item in v)
                {
                    if (colAdded == false)
                    {
                        colAdded = true;
                        foreach (var col in item)
                        {
                            dt.Columns.Add(col);
                        }
                    }
                    else
                    {
                        dt.Rows.Add(item.ToArray());
                    }
                }
            }
            return dt;
        }

        public static List<List<string>> ReadCSV(string filePath)
        {
            var csvRead = new List<List<string>>();
            using (var csvReader = new TextFieldParser(filePath))
            {
                csvReader.SetDelimiters(new string[] { "," });
                csvReader.HasFieldsEnclosedInQuotes = true;
                while (true)
                {
                    List<string> a = new List<string>();
                    string[] parts = csvReader.ReadFields();

                    if (parts == null)
                    {
                        break;
                    }
                    else
                    {
                        a = parts.ToList();
                        csvRead.Add(a);
                    }
                }
            }
            return csvRead;
        }


        public static void WriteCSV(DataTable sourceTable, TextWriter writer, bool includeHeaders)
        {
            if (includeHeaders)
            {
                IEnumerable<String> headerValues = sourceTable.Columns
                    .OfType<DataColumn>()
                    .Select(column => QuoteValue(column.ColumnName));

                writer.WriteLine(String.Join(",", headerValues.ToArray()));
            }

            IEnumerable<String> items = null;

            foreach (DataRow row in sourceTable.Rows)
            {
                items = row.ItemArray.Select(o => QuoteValue(o.ToString()));
                writer.WriteLine(String.Join(",", items.ToArray()));
            }

            writer.Flush();
        }

        private static string QuoteValue(string value)
        {
            return String.Concat("\"",
            value.Replace("\"", "\"\""), "\"");
        }


    }
}
