using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.VisualStyles;
using NMG.Core.Domain;
using NMG.UI;
using Xipton.Razor;
using System.IO;

namespace NMG.Core.Bsoft
{
    public class RazorCore
    {
        public string GetExecuted(string Template, Table md)
        {
            ITemplate template;
            try
            {
                RazorMachine rm = new RazorMachine();
                template = rm.ExecuteContent(Template, md);
            }
            catch
            {
                throw;
            }
            return template.Result;
        }


        public string Genetate(RgMapping map, RgProject _rgProject)
        {
            string allMEssages = string.Empty;
            foreach (var template in map.TemplateRelativePaths)
            {
                string templtFile = FileHelper.GetFullPath(_rgProject.TemplateRootFolder, template);
                string message = GenerateIt(map.TableName, templtFile, _rgProject.SolutionRootFolder,
                    _rgProject.Alltables.Tables);
                allMEssages += message + System.Environment.NewLine;
            }
            return allMEssages;
        }

        private string ReadTemplate(string templateFullPath)
        {
            if (System.IO.File.Exists(templateFullPath))
            {
                string templateString = File.ReadAllText(templateFullPath);
                return templateString.Replace("$$", "@@");
            }
            else
            {
                return "::Error ! File Doesn't Exit :" + templateFullPath;
            }
        }

        private TemplateOutputs ReadOutputFile(string outptString, string templateName)
        {
            TemplateOutputs templateOutputs = new TemplateOutputs();
            templateOutputs.FileNameFromTemplate = templateName;
            using (StringReader reader = new StringReader(outptString))
            {
                string line = string.Empty;
                do
                {
                    line = reader.ReadLine();
                    if (line != null)
                    {
                        if (line.Trim().StartsWith("##filename"))
                        {
                            templateOutputs.FileNameFromTemplate = line.Trim().Replace("##filename", "").Replace("##", "").Replace("=", "").Trim();

                        }
                        else
                        {
                            templateOutputs.FinalOutputString = templateOutputs.FinalOutputString + line + System.Environment.NewLine;
                        }
                    }

                } while (line != null);
            }
            return templateOutputs;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="templateFullPath"></param>
        /// <param name="solutionRootFolder"></param>
        /// <param name="tables"></param>
        /// <returns> Error message or generated file name</returns>
        public string GenerateIt(String tableName, string templateFullPath, string solutionRootFolder, List<Table> tables)
        {
            string templateString = ReadTemplate(templateFullPath);
            if (templateString.Contains("::"))
            {
                return templateString;//Error!
            }
            Table table = (from Table t in tables
                           where t.Name == tableName
                           select t).First();
            string tempOutput;
            try
            {
                tempOutput = GetExecuted(templateString, table);
            }
            catch (Exception ex)
            {
                return "::Error ! " + ex.Message.Replace(@"\r\n", System.Environment.NewLine);
            }
            string templatename = Path.GetFileNameWithoutExtension(templateFullPath);
            TemplateOutputs templateOutputs = ReadOutputFile(tempOutput, templatename);
            string outputFile = templateOutputs.GetFinalFileNameForceFully(solutionRootFolder);
            File.WriteAllText(outputFile, templateOutputs.FinalOutputString);

            return outputFile;
        }

        public string GenerateItString(String tableName, string templateFullPath, string solutionRootFolder, List<Table> tables)
        {
            string templateString = ReadTemplate(templateFullPath);
            if (templateString.Contains("::"))
            {
                return templateString;//Error!
            }
            Table table = (from Table t in tables
                           where t.Name == tableName
                           select t).First();
            string tempOutput;
            try
            {
                tempOutput = GetExecuted(templateString, table);
            }
            catch (Exception ex)
            {
                return "::Error ! " + ex.Message.Replace(@"\r\n", System.Environment.NewLine);
            }

            return tempOutput;
        }
    }

    /// <summary>
    /// Parameters from template ## declarative 
    /// </summary>
    public class TemplateOutputs
    {
        public string FileNameFromTemplate { get; set; }//##filename
        public string FinalOutputString { get; set; }//file is written here

        public string GetFinalFileNameForceFully(string RootFolder)
        {
            if (FileNameFromTemplate.StartsWith(@"\"))
            {
                FileNameFromTemplate = FileNameFromTemplate.Remove(0, 1);
            }
            string path = Path.Combine(RootFolder, FileNameFromTemplate);
            if (Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            if (File.Exists(path))
            {
                //backup old if preexsit
                File.Move(path, GetNewBackupFileName(path));
            }
            else
            {
                //create directory if not exist
                Directory.CreateDirectory(Path.GetDirectoryName(path)); //create directory if not existing
            }
            return path;
        }

        private string GetNewBackupFileName(string file)
        {
            string filename = Path.GetFileName(file);
            string folder = Path.GetDirectoryName(file);
            string backupFolder = Path.Combine(folder, "BackupRG");
            string backupFileName = Path.Combine(backupFolder, filename);
            if (!Directory.Exists(backupFolder))
            {
                Directory.CreateDirectory(backupFolder);
            }
            int maxExtenstion = 0;
            foreach (var f in Directory.GetFiles(backupFolder))
            {
                if (f.StartsWith(backupFileName))
                {
                    string str = f.Replace(backupFileName + "_", "");
                    int i = int.Parse(str);
                    if (i > maxExtenstion)
                    {
                        maxExtenstion = i;
                    }
                }
            }
            return backupFileName + "_" + (++maxExtenstion).ToString();


        }
    }
}
