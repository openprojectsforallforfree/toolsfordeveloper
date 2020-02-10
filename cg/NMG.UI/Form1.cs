using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
using NMG.Core.Domain;
using NMG.Core.Bsoft;
using Language = FastColoredTextBoxNS.Language;

///Nodes => Template =>TempateFolder,Template
/// Project=>Table,Template
namespace NMG.UI
{
    public partial class Form1 : Form
    {
        private string _projPath;

        private RgProject _rgProject = new RgProject();
        public Form1()
        {
            InitializeComponent();
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    if ((int)m.Result == HTCLIENT)
                    {
                        m.Result = (IntPtr)HTCAPTION;
                    }

                    return;
            }

            base.WndProc(ref m);
        }

        #region Menu

        private void btnReadTableXML_Click(object sender, EventArgs e)
        {
            string _Tablexmlpath = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _Tablexmlpath = ofd.FileName;
                mnuReadTable.ToolTipText = _Tablexmlpath;
            }

            if (File.Exists(_Tablexmlpath)
              )
            {
                _rgProject.Alltables = AllTables.Load(_Tablexmlpath);
                LoadTablesToTree(_rgProject.Alltables.Tables);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var newProject = new RgProject();//temporary to get data

            foreach (TreeNode item in TreeTables.Nodes)
            {
                if (item.Tag.GetType() == typeof(Table))
                {
                    string tableName = ((Table)item.Tag).Name;
                    if (item.Nodes.Count > 0)
                    {
                        var hasMap = false;
                        var rgmap = new RgMapping();
                        foreach (TreeNode mapping in item.Nodes)
                        {
                            hasMap = true;
                            rgmap.TemplateRelativePaths.Add(((Template)mapping.Tag).TemplatePath);
                        }
                        if (hasMap)
                        {
                            rgmap.TableName = tableName;
                            newProject.RgMappings.Add(rgmap);
                        }
                    }
                }
            }
            _rgProject.RgMappings = newProject.RgMappings;
            _rgProject.Save(_projPath);
            mnuProject.ToolTipText = _projPath;
        }

        private void mnuNew_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Project Files|*.rgf";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                TreeTables.Nodes.Clear();
                _projPath = sfd.FileName;
                mnuProject.ToolTipText = _projPath;
                _rgProject = new RgProject();
                _rgProject.TemplateRootFolder = templateRoot;
                _rgProject.SolutionRootFolder = Path.Combine(Path.GetDirectoryName(_projPath), "Outputs");
                _rgProject.Save(_projPath);
            }
        }

        private void mnuOutput_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                this._rgProject.SolutionRootFolder = fbd.SelectedPath;
                mnuOutput.ToolTipText = fbd.SelectedPath;
            }
        }

        private void LoadProjectFile()
        {
            //Laod it
            mnuProject.ToolTipText = _projPath;
            this._rgProject = RgProject.Load(_projPath);
            if (this._rgProject != null && _rgProject.Alltables != null)
            {
                LoadTablesToTree(_rgProject.Alltables.Tables);
                //add templates
                foreach (RgMapping rgMaps in _rgProject.RgMappings)
                {
                    TreeNode tn = TreeTables.Nodes.Find(rgMaps.TableName, false).FirstOrDefault();
                    if (tn != null)
                    {
                        foreach (String template in rgMaps.TemplateRelativePaths)
                        {
                            var templateName = FileHelper.GetTemplateName(template);
                            AddTemplateNode(tn, new Template { TemplatePath = template, TemplateName = templateName });
                        }
                    }
                }
                TreeTables.ExpandAll();
            }
            mnuOutput.ToolTipText = _rgProject.SolutionRootFolder;

        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Project Files|*.rgf";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _projPath = ofd.FileName;
            }
            LoadProjectFile();
        }
        #endregion Menu

        #region Load


        private string templateRoot = "";
        private void Form1_Load(object sender, EventArgs e)
        {
            _projPath = "proj.rgf";//default file
            templateRoot = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "Templates");
            LoadTemplateTree(TreeTemplates, templateRoot);
            if (File.Exists(_projPath))
            {
                LoadProjectFile();
            }

        }


        //Load folderpath to tre
        private void LoadTemplateTree(TreeView treeView, string path)
        {
            if (!Directory.Exists(path))
            {
                MessageBox.Show("Please have the template folder : " + path);
                return;
            }
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
            treeView.ExpandAll();
        }

        /// <summary>
        /// Load Folder path helper
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <returns></returns>
        private TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);
            directoryNode.Tag = new TemplateFolder { Name = directoryInfo.Name, FolderPath = directoryInfo.FullName };
            foreach (var directory in directoryInfo.GetDirectories())
            {
                var tn = CreateDirectoryNode(directory);
                tn.Tag = new TemplateFolder { Name = directory.Name, FolderPath = directory.FullName };
                directoryNode.Nodes.Add(tn);
            }
            foreach (var file in directoryInfo.GetFiles())
            {
                AddTemplateNode(directoryNode, new Template { TemplateName = file.Name, TemplatePath = FileHelper.GetRelativePath(templateRoot, file.FullName) });
            }
            return directoryNode;
        }

        private void LoadTablesToTree(List<Table> tables)
        {
            TreeNode tn;
            TreeTables.Nodes.Clear();
            foreach (var table in tables)
            {
                TreeNode tnn = TreeTables.Nodes.Add(table.Name, table.Name);
                tnn.Tag = table;
                tnn.ToolTipText = table.Name;
            }
            TreeTables.ExpandAll();
        }


        private void MoveNodesRecursive(TreeNode SourceNode, TreeNode DestinationNode)
        {
            if (DestinationNode.Tag.GetType() == typeof(Table))
            {
                foreach (TreeNode tn in SourceNode.Nodes)
                {
                    //if (NewNode.Tag.GetType() == typeof(Table) && DestinationNode.Tag.GetType() == typeof(Project))
                    if (tn.Tag.GetType() == typeof(Template))
                    {
                        AddTemplateNode(tn, DestinationNode);
                    }
                    MoveNodesRecursive(tn, DestinationNode);
                }
            }
        }



        #endregion Load


        #region Node Helpers
        private void AddTemplateNode(TreeNode Source, TreeNode Destination)
        {
            string templatePath = ((Template)Source.Tag).TemplatePath;
            string templateName = ((Template)Source.Tag).TemplateName;
            foreach (TreeNode node in Destination.Nodes)
            {
                if (node.ToolTipText == templatePath)
                {
                    MessageBox.Show(templateName + "Already added!");
                    return;
                }
            }
            AddTemplateNode(Destination, new Template { TemplatePath = templatePath, TemplateName = templateName });
        }

        private void AddTemplateNode(TreeNode toNode, Template template)
        {
            TreeNode tnadded = toNode.Nodes.Add(template.TemplateName);
            tnadded.ToolTipText = template.TemplatePath;
            tnadded.Tag = template;
        }

        private void RemoveTemplateNode(TreeNode fromNode)
        {
            if (fromNode.Tag.GetType() == typeof(Table))
            {
                if (fromNode.Nodes.Count > 0)
                {
                    for (int i = fromNode.Nodes.Count - 1; i >= 0; i--)
                    {
                        fromNode.Nodes[i].Remove();
                    }
                }
            }
            else if (fromNode.Tag.GetType() == typeof(Template))
            {
                fromNode.Remove();
            }

        }


        #endregion


        #region Drag To Table

        /// <summary>
        /// All tables will have rgMapping nodes
        ///     If templatefolder add all sub nodes to table
        ///     If template add it to table
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="Destination"></param>
        private void DragDropFromTemplateToTableTree(TreeNode Source, TreeNode Destination)
        {
            if (Source.Tag.GetType() == typeof(TemplateFolder) && Destination.Tag.GetType() == typeof(Table))
            {
                MoveNodesRecursive(Source, Destination);
            }
            if (Source.Tag.GetType() == typeof(Template) && Destination.Tag.GetType() == typeof(Table))
            {
                AddTemplateNode(Source, Destination);
            }
        }

        private void DragDropFromTableToTableTree(TreeNode Source, TreeNode Destination)
        {
            if (Source.Tag.GetType() == typeof(Template) && Destination.Tag.GetType() == typeof(Table))
            {
                AddTemplateNode(Source, Destination);
                //  MoveNodesRecursive(Source, Destination);
            }
            if (Source.Tag.GetType() == typeof(Table) && Destination.Tag.GetType() == typeof(Table))
            {
                MoveNodesRecursive(Source, Destination);
            }
        }



        private void TreeTables_DragDrop(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
            TreeNode NewNode;
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
                TreeNode DestinationNode = ((TreeView)sender).GetNodeAt(pt);
                if (DestinationNode == null)
                {
                    MessageBox.Show("Please drag to a node!");
                    return;
                }
                NewNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                if (DestinationNode.TreeView != NewNode.TreeView && NewNode.Tag != null)
                {
                    DragDropFromTemplateToTableTree(NewNode, DestinationNode);
                }
                else if (DestinationNode.TreeView == NewNode.TreeView)
                {
                    DragDropFromTableToTableTree(NewNode, DestinationNode);
                }

            }
            TreeTables.ExpandAll();
        }

        private void TreeTables_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TreeTemplates_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Copy);
        }
        #endregion Drag


        #region Drag in Table
        private void TreeTables_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Copy);
        }

        private void TreeTemplates_DragDrop(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;

            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
                var newNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                RemoveTemplateNode(newNode);
            }
            TreeTables.ExpandAll();
        }

        private void TreeTemplates_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
        #endregion

        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RazorCore rc = new RazorCore();
            string allMEssages = string.Empty;
            foreach (var map in _rgProject.RgMappings)
            {
                allMEssages = rc.Genetate(map, _rgProject);
            }
            MessageBox.Show(allMEssages);
        }

       

        //For Context Menu
        private void TreeTemplates_MouseUp(object sender, MouseEventArgs e)
        {
            // Show menu only if the right mouse button is clicked.
            if (e.Button == MouseButtons.Right)
            {
                
                // Point where the mouse is clicked.
                Point p = new Point(e.X, e.Y);

                // Get the node that the user has clicked.
                TreeNode node = TreeTemplates.GetNodeAt(p);
                if (node != null)
                {
                    TreeTemplates.SelectedNode = node;
                    if (TreeTemplates.SelectedNode.Tag.GetType() == typeof(Template))
                    {
                        mnuExplore.Visible = true;
                        btnSeeinExplorer.Visible = true;
                    }
                    else
                    {
                        mnuExplore.Visible = false;
                        btnSeeinExplorer.Visible = true;
                    }
                    ContextMenuTemplate.Show(TreeTemplates, p);
                    //break;
                }
            }
        }

        private void mnuExplore_Click(object sender, EventArgs e)
        {
            if (TreeTemplates.SelectedNode.Tag.GetType() == typeof(Template))
            {
                string filepath = FileHelper.GetFullPath(templateRoot , ((Template)(TreeTemplates.SelectedNode.Tag)).TemplatePath);
                Process.Start(filepath);
            }
            else
            {
                string filepath = FileHelper.GetFullPath(templateRoot,((TemplateFolder)(TreeTemplates.SelectedNode.Tag)).FolderPath);
                Process.Start(filepath);
            }

        }

        private void btnSeeinExplorer_Click(object sender, EventArgs e)
        {
            if (TreeTemplates.SelectedNode.Tag.GetType() == typeof(Template))
            {
                string filepath = Path.GetDirectoryName(FileHelper.GetFullPath(templateRoot,((Template)(TreeTemplates.SelectedNode.Tag)).TemplatePath));
                Process.Start(filepath);
            }
            else
            {
                string filepath = FileHelper.GetFullPath(templateRoot,((TemplateFolder)(TreeTemplates.SelectedNode.Tag)).FolderPath);
                Process.Start(filepath);
            }

        }

        private void mnuClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
            }
            else
            {
                WindowState = FormWindowState.Maximized;
            }
        }

        private void mnuMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void TreeTables_MouseUp(object sender, MouseEventArgs e)
        {
            // Show menu only if the right mouse button is clicked.
            if (e.Button == MouseButtons.Right)
            {

                // Point where the mouse is clicked.
                Point p = new Point(e.X, e.Y);

                // Get the node that the user has clicked.
                TreeNode node = TreeTemplates.GetNodeAt(p);
                if (node != null)
                {
                    TreeTemplates.SelectedNode = node;
                    if (TreeTemplates.SelectedNode.Tag.GetType() == typeof(Table))
                    {
                        mnuExplore.Visible = true;
                        btnSeeinExplorer.Visible = true;
                    }
                    else
                    {
                        mnuExplore.Visible = false;
                        btnSeeinExplorer.Visible = true;
                    }
                    contextMenuStripProject.Show(TreeTables, p);
                    //break;
                }
            }
        }


        /// <summary>
        /// context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void generateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (TreeTables.SelectedNode.Tag.GetType() == typeof(Table))
            {
                Table t = (Table)TreeTables.SelectedNode.Tag;
                var map = (from RgMapping v in _rgProject.RgMappings
                           where v.TableName == t.Name
                           select v).FirstOrDefault();
                if (map == null)
                {
                    MessageBox.Show(string.Format("Map for {0} not found.\nTry saving data first.", t.Name))
                     ;
                    return;
                }
                RazorCore rc = new RazorCore();
                string message =rc.Genetate(map,_rgProject);
                MessageBox.Show(message);
            }
            else if (TreeTables.SelectedNode.Tag.GetType() == typeof(Template))
            {
                string gen = GenerateSingle();
                MessageBox.Show(gen);
            }

            //foreach (TreeNode tplt in TreeTemplates.SelectedNode.Nodes)
            //{
            //    var template = (Template)tplt.Tag;

            //}
            //string filepath = Path.GetDirectoryName(FileHelper.GetFullTemplatePath(((Template)(TreeTemplates.SelectedNode.Tag)).TemplatePath));


            //foreach (var map in _rgProject.RgMappings)
            //{
            //    foreach (var template in map.TemplateRelativePaths)
            //    {
            //        string templtFile = Path.Combine(_rgProject.TemplateRootFolder, template.Remove(0, 1));
            //        string message = rc.GenerateIt(map.TableName, templtFile, _rgProject.SolutionRootFolder, _rgProject.Alltables.Tables);
            //        MessageBox.Show(message);
            //    }
            //}

        }

        private string GenerateSingle()
        {
            Template tplt = (Template)TreeTables.SelectedNode.Tag;
            Table t = (Table)TreeTables.SelectedNode.Parent.Tag;
            var map = (from RgMapping v in _rgProject.RgMappings
                       where v.TableName == t.Name
                       select v).FirstOrDefault();
            if (map == null)
            {
                return string.Format("Map for {0} not found.\nTry saving data.", t.Name);
            }

            var rc = new RazorCore();
            string templtFile = Path.Combine(_rgProject.TemplateRootFolder, tplt.TemplatePath.Remove(0, 1));
            string message = rc.GenerateIt(map.TableName, templtFile, _rgProject.SolutionRootFolder,
                _rgProject.Alltables.Tables);
            return message;
        }

        private void TreeTables_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (TreeTables.SelectedNode.Tag.GetType() == typeof(Template))
            {
                Template tplt = (Template)TreeTables.SelectedNode.Tag;
                Table t = (Table)TreeTables.SelectedNode.Parent.Tag;
                var map = (from RgMapping v in _rgProject.RgMappings
                           where v.TableName == t.Name
                           select v).FirstOrDefault();
                if (map == null)
                {
                    MessageBox.Show(string.Format("Map for {0} not found.\n Try saving data first.", t.Name));
                    return;
                }

                var rc = new RazorCore();
                string templtFile = FileHelper.GetFullPath (templateRoot, tplt.TemplatePath);
                string message = rc.GenerateItString(map.TableName, templtFile, _rgProject.SolutionRootFolder,
                    _rgProject.Alltables.Tables);
                mapCodeFastColoredTextBox.Language = Language.CSharp;
                mapCodeFastColoredTextBox.Text = message;
            }
        }

        private void TreeTemplates_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (TreeTemplates.SelectedNode.Tag.GetType() == typeof (Template))
            {
                var t = ((Template) TreeTemplates.SelectedNode.Tag);
                string s = File.ReadAllText(FileHelper.GetFullPath(templateRoot, t.TemplatePath));
                mapCodeFastColoredTextBox.Language = Language.CSharp;
                mapCodeFastColoredTextBox.Text = s;
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (TreeTemplates.SelectedNode.Tag.GetType() == typeof(Template))
            {
                var t = ((Template)TreeTemplates.SelectedNode.Tag);
                File.WriteAllText(FileHelper.GetFullPath(templateRoot, t.TemplatePath), mapCodeFastColoredTextBox.Text);
            }
        }

        private void mnuCreateXML_Click(object sender, EventArgs e)
        {
            Process.Start("NHibernateMappingGenerator.exe");
        }
    }
}