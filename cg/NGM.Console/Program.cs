using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NMG;
using NMG.Core.Bsoft;

namespace NGM.ConsoleProg
{
    class Program
    {
        private static void Main(string[] args)
        {
            string projectfile = "proj.rgf";
            if (args.Length>0)
            {
                projectfile = args[0];
            }
            string allMEssages = string.Empty;
            if (File.Exists(projectfile))
            {
                RgProject _rgProject = new RgProject();
                _rgProject = RgProject.Load("proj.rgf");
                RazorCore rc = new RazorCore();
               
                foreach (var map in _rgProject.RgMappings)
                {
                    allMEssages = rc.Genetate(map, _rgProject);
                }
            }
            else
            {
                allMEssages = "File : proj.rgf could not be found.";
            }
           
            Console.WriteLine(allMEssages);
            Console.WriteLine("Complete");
            Console.ReadKey();
        }

        //FileProvider fileProvider = new FileProvider();
            //fileProvider.Project = Project.Load(fileProvider.CgProjectXML);
            //fileProvider.All_Tables = AllTables.Load(fileProvider.TableXmlPath);
            //RazorCore rc = new RazorCore();
            //if (fileProvider.Project == null)
            //{
            //    return;
            //}

            //foreach (var projectTable in fileProvider.Project.Tables)
            //{
            //    foreach (var template in projectTable.Templates)
            //    {
            //        string templtFile = Path.Combine(fileProvider.TemplateFolderPath, template.TemplatePath.Remove(0, 1));
            //        string outputFile = Path.Combine(Directory.GetParent(fileProvider.VisualStudioSolutionFolder).FullName, template.VisualStudioFolder.FolderPath.Remove(0, 1));
            //        string message = rc.Generate(projectTable.Name, templtFile, outputFile, fileProvider.All_Tables.Tables);

            //        Console.WriteLine("Generated " + message);
            //    }
            //}
            //Console.WriteLine("Success!");
            //Console.ReadKey();
        //}

        //private string Genetate(RgMapping map)
        //{
        //    string allMEssages = string.Empty;
        //    var rc = new RazorCore();
        //    foreach (var template in map.TemplateRelativePaths)
        //    {
        //        string templtFile = FileHelper.GetFullPath(templateRoot, template);
        //        string message = rc.GenerateIt(map.TableName, templtFile, _rgProject.SolutionRootFolder,
        //            _rgProject.Alltables.Tables);
        //        allMEssages += message + System.Environment.NewLine;
        //    }
        //    return allMEssages;
        //}
    }
}
