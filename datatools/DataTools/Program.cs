using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Bsoft.Common;
using Bsoft.Common.Utilities;
using Bsoft.Data;
using Bsoft.Web;
using Bsoft.DataAccess;


namespace DataTools
{
    class Program
    {
        private static void Main(string[] args)
        {
            string command = string.Empty;

            if (args.Length > 1)
            {
                command = args.FirstOrDefault().ToLower();
            }

            Global.StartConnection(DataTools.Properties.Settings.Default.Con, DatabaseType.SQLServer);
           
            switch (command)
            {
                case "getshare":
                    GetShare();
                    break;
                case "getsharenow":
                    GetShare(new DateTime(2015, 1, 1), DateTime.Now);
                    break;
                case "export":
                    Utilites.Export("share", Environment.CurrentDirectory, true);
                    break;
                case "import":
                    Utilites.Import(Path.Combine(Environment.CurrentDirectory, "share1.csv"), true, true);
                    break;
                case "loadfile":
                    string loadFile = DataTools.Properties.Settings.Default.LoadFile;
                    Utilites.Import(loadFile, false, true);
                    break;
                case "loadtypedfile":
                    string loadtypedfile = DataTools.Properties.Settings.Default.LoadFile;
                    if (args.Length>1)
                    {
                        loadtypedfile = args[1];
                    }
                    Utilites.Import(loadtypedfile, true, true);
                    break;
                case "?":
                case "help":
                    Console.WriteLine("getshare");
                    Console.Write("::get share details from http://www.nepalstock.com/datanepse/index.php");
                    Console.WriteLine("getsharenow");
                    Console.WriteLine("export");
                    Console.WriteLine("import");

                    break;
                default:
                    break;
            }





        }

        public static void GetShare()
        {
            string html = WebUtilities.ReadString(@"http://www.nepalstock.com/datanepse/index.php");
            DataSet ds = HtmlUtilitites.ConvertHTMLTablesToDataSet(html);
            DataTable dt = Share.getdDatatable(ds);
            dt.TableName = "ShareTemp";
            DataColumn dc = dt.Columns.Add("InsertedDate");
            foreach (DataRow Row in dt.Rows)
            {
                Row[dc] = DateTime.Now;
            }

            Global.Ds.DropIfExist(dt.TableName);
            Global.Ds.CreateTABLE(dt, false);
            Global.Db.BulkCopy(dt);
        }


        public static void GetShare(DateTime from, DateTime to)
        {
            string url = @"http://www.sharesansar.com/previous.php?date={0}";
            for (DateTime i = from; i < to; i = i.AddDays(1))
            {
                string date = string.Format("{0}-{1:00}-{2:00}", i.Year, i.Month, i.Day);
                string html = WebUtilities.ReadString(string.Format(url, date));
                DataSet ds = HtmlUtilitites.ConvertHTMLTablesToDataSet(html);
                DataTable dt = Share.getdDatatableSS(ds);
                if (dt.Rows.Count > 0)
                {
                    dt.TableName = "ShareHistory";
                    DataColumn dc = dt.Columns.Add("InsertedDate");
                    foreach (DataRow Row in dt.Rows)
                    {
                        Row[dc] = DateTime.Now;
                    }

                    if (!Global.Ds.DoesTableExists(dt.TableName))
                    {
                        Global.Ds.CreateTABLE(dt, false);
                    }
                    Global.Db.BulkCopy(dt);
                }
                else
                {
                    System.IO.File.WriteAllText("Log.txt", "No Data for : " + date);
                }
            }
        }
    }
}
