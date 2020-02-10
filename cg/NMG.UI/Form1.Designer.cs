namespace NMG.UI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStripLeft = new System.Windows.Forms.MenuStrip();
            this.mnuProject = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReadTable = new System.Windows.Forms.ToolStripMenuItem();
            this.generateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TreeTemplates = new System.Windows.Forms.TreeView();
            this.TreeTables = new System.Windows.Forms.TreeView();
            this.ContextMenuTemplate = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuExplore = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSeeinExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripRight = new System.Windows.Forms.MenuStrip();
            this.mnuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMaximize = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMinimize = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripProject = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.generateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mapCodeFastColoredTextBox = new FastColoredTextBoxNS.FastColoredTextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.mnuCreateXML = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripLeft.SuspendLayout();
            this.ContextMenuTemplate.SuspendLayout();
            this.menuStripRight.SuspendLayout();
            this.contextMenuStripProject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapCodeFastColoredTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStripLeft
            // 
            this.menuStripLeft.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStripLeft.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStripLeft.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuProject,
            this.mnuOutput,
            this.mnuReadTable,
            this.mnuCreateXML,
            this.generateToolStripMenuItem});
            this.menuStripLeft.Location = new System.Drawing.Point(0, 0);
            this.menuStripLeft.Name = "menuStripLeft";
            this.menuStripLeft.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStripLeft.ShowItemToolTips = true;
            this.menuStripLeft.Size = new System.Drawing.Size(599, 40);
            this.menuStripLeft.TabIndex = 0;
            this.menuStripLeft.Text = "ToMakeItDraggableAtCenter";
            // 
            // mnuProject
            // 
            this.mnuProject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.mnuProject.Image = global::NMG.UI.Properties.Resources.File;
            this.mnuProject.Name = "mnuProject";
            this.mnuProject.Size = new System.Drawing.Size(115, 36);
            this.mnuProject.Text = "Project XML";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.mnuNew_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // mnuOutput
            // 
            this.mnuOutput.Image = global::NMG.UI.Properties.Resources.Folder;
            this.mnuOutput.Name = "mnuOutput";
            this.mnuOutput.Size = new System.Drawing.Size(89, 36);
            this.mnuOutput.Text = "Output";
            this.mnuOutput.Click += new System.EventHandler(this.mnuOutput_Click);
            // 
            // mnuReadTable
            // 
            this.mnuReadTable.AutoToolTip = true;
            this.mnuReadTable.Image = global::NMG.UI.Properties.Resources.XML;
            this.mnuReadTable.Name = "mnuReadTable";
            this.mnuReadTable.Size = new System.Drawing.Size(116, 36);
            this.mnuReadTable.Text = "&Browse XML";
            this.mnuReadTable.Click += new System.EventHandler(this.btnReadTableXML_Click);
            // 
            // generateToolStripMenuItem
            // 
            this.generateToolStripMenuItem.AutoToolTip = true;
            this.generateToolStripMenuItem.Image = global::NMG.UI.Properties.Resources.Generate;
            this.generateToolStripMenuItem.Name = "generateToolStripMenuItem";
            this.generateToolStripMenuItem.Size = new System.Drawing.Size(98, 36);
            this.generateToolStripMenuItem.Text = "Generate";
            this.generateToolStripMenuItem.Click += new System.EventHandler(this.generateToolStripMenuItem_Click);
            // 
            // TreeTemplates
            // 
            this.TreeTemplates.AllowDrop = true;
            this.TreeTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeTemplates.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TreeTemplates.Location = new System.Drawing.Point(304, 43);
            this.TreeTemplates.Name = "TreeTemplates";
            this.TreeTemplates.Size = new System.Drawing.Size(443, 439);
            this.TreeTemplates.TabIndex = 27;
            this.TreeTemplates.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeTemplates_ItemDrag);
            this.TreeTemplates.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeTemplates_AfterSelect);
            this.TreeTemplates.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeTemplates_DragDrop);
            this.TreeTemplates.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeTemplates_DragEnter);
            this.TreeTemplates.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TreeTemplates_MouseUp);
            // 
            // TreeTables
            // 
            this.TreeTables.AllowDrop = true;
            this.TreeTables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TreeTables.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TreeTables.Location = new System.Drawing.Point(14, 43);
            this.TreeTables.Name = "TreeTables";
            this.TreeTables.ShowNodeToolTips = true;
            this.TreeTables.Size = new System.Drawing.Size(283, 439);
            this.TreeTables.TabIndex = 28;
            this.TreeTables.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeTables_ItemDrag);
            this.TreeTables.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeTables_AfterSelect);
            this.TreeTables.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeTables_DragDrop);
            this.TreeTables.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeTables_DragEnter);
            this.TreeTables.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TreeTables_MouseUp);
            // 
            // ContextMenuTemplate
            // 
            this.ContextMenuTemplate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExplore,
            this.btnSeeinExplorer});
            this.ContextMenuTemplate.Name = "ContextMenu";
            this.ContextMenuTemplate.Size = new System.Drawing.Size(151, 48);
            // 
            // mnuExplore
            // 
            this.mnuExplore.Name = "mnuExplore";
            this.mnuExplore.Size = new System.Drawing.Size(150, 22);
            this.mnuExplore.Text = "Open File";
            this.mnuExplore.Click += new System.EventHandler(this.mnuExplore_Click);
            // 
            // btnSeeinExplorer
            // 
            this.btnSeeinExplorer.Name = "btnSeeinExplorer";
            this.btnSeeinExplorer.Size = new System.Drawing.Size(150, 22);
            this.btnSeeinExplorer.Text = "See in Explorer";
            this.btnSeeinExplorer.Click += new System.EventHandler(this.btnSeeinExplorer_Click);
            // 
            // menuStripRight
            // 
            this.menuStripRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.menuStripRight.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStripRight.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuClose,
            this.mnuMaximize,
            this.mnuMinimize});
            this.menuStripRight.Location = new System.Drawing.Point(1149, 1);
            this.menuStripRight.Name = "menuStripRight";
            this.menuStripRight.Size = new System.Drawing.Size(92, 24);
            this.menuStripRight.TabIndex = 29;
            this.menuStripRight.Text = "menuStripRight";
            // 
            // mnuClose
            // 
            this.mnuClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mnuClose.AutoToolTip = true;
            this.mnuClose.Image = global::NMG.UI.Properties.Resources.close;
            this.mnuClose.Name = "mnuClose";
            this.mnuClose.Size = new System.Drawing.Size(28, 20);
            this.mnuClose.Click += new System.EventHandler(this.mnuClose_Click);
            // 
            // mnuMaximize
            // 
            this.mnuMaximize.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mnuMaximize.Image = global::NMG.UI.Properties.Resources.maximize;
            this.mnuMaximize.Name = "mnuMaximize";
            this.mnuMaximize.Size = new System.Drawing.Size(28, 20);
            this.mnuMaximize.ToolTipText = "Maximize";
            this.mnuMaximize.Click += new System.EventHandler(this.mnuMaximize_Click);
            // 
            // mnuMinimize
            // 
            this.mnuMinimize.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mnuMinimize.Image = global::NMG.UI.Properties.Resources.minimize;
            this.mnuMinimize.Name = "mnuMinimize";
            this.mnuMinimize.Size = new System.Drawing.Size(28, 20);
            this.mnuMinimize.ToolTipText = "Minimize";
            this.mnuMinimize.Click += new System.EventHandler(this.mnuMinimize_Click);
            // 
            // contextMenuStripProject
            // 
            this.contextMenuStripProject.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateToolStripMenuItem1});
            this.contextMenuStripProject.Name = "contextMenuStripProject";
            this.contextMenuStripProject.Size = new System.Drawing.Size(122, 26);
            // 
            // generateToolStripMenuItem1
            // 
            this.generateToolStripMenuItem1.Name = "generateToolStripMenuItem1";
            this.generateToolStripMenuItem1.Size = new System.Drawing.Size(121, 22);
            this.generateToolStripMenuItem1.Text = "Generate";
            this.generateToolStripMenuItem1.Click += new System.EventHandler(this.generateToolStripMenuItem1_Click);
            // 
            // mapCodeFastColoredTextBox
            // 
            this.mapCodeFastColoredTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapCodeFastColoredTextBox.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.mapCodeFastColoredTextBox.BackBrush = null;
            this.mapCodeFastColoredTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.mapCodeFastColoredTextBox.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.mapCodeFastColoredTextBox.IsReplaceMode = false;
            this.mapCodeFastColoredTextBox.Location = new System.Drawing.Point(753, 70);
            this.mapCodeFastColoredTextBox.Name = "mapCodeFastColoredTextBox";
            this.mapCodeFastColoredTextBox.Paddings = new System.Windows.Forms.Padding(0);
            this.mapCodeFastColoredTextBox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.mapCodeFastColoredTextBox.Size = new System.Drawing.Size(473, 412);
            this.mapCodeFastColoredTextBox.TabIndex = 30;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(956, 41);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 29);
            this.btnSave.TabIndex = 31;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
            // 
            // mnuCreateXML
            // 
            this.mnuCreateXML.Name = "mnuCreateXML";
            this.mnuCreateXML.Size = new System.Drawing.Size(80, 36);
            this.mnuCreateXML.Text = "Create XML";
            this.mnuCreateXML.Click += new System.EventHandler(this.mnuCreateXML_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(1238, 496);
            this.ControlBox = false;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.mapCodeFastColoredTextBox);
            this.Controls.Add(this.TreeTables);
            this.Controls.Add(this.TreeTemplates);
            this.Controls.Add(this.menuStripLeft);
            this.Controls.Add(this.menuStripRight);
            this.Font = new System.Drawing.Font("Corbel", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripLeft;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStripLeft.ResumeLayout(false);
            this.menuStripLeft.PerformLayout();
            this.ContextMenuTemplate.ResumeLayout(false);
            this.menuStripRight.ResumeLayout(false);
            this.menuStripRight.PerformLayout();
            this.contextMenuStripProject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mapCodeFastColoredTextBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripLeft;
        private System.Windows.Forms.TreeView TreeTemplates;
        private System.Windows.Forms.TreeView TreeTables;
        private System.Windows.Forms.ToolStripMenuItem mnuReadTable;
        private System.Windows.Forms.ToolStripMenuItem generateToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ContextMenuTemplate;
        private System.Windows.Forms.ToolStripMenuItem mnuExplore;
        private System.Windows.Forms.ToolStripMenuItem btnSeeinExplorer;
        private System.Windows.Forms.ToolStripMenuItem mnuOutput;
        private System.Windows.Forms.MenuStrip menuStripRight;
        private System.Windows.Forms.ToolStripMenuItem mnuClose;
        private System.Windows.Forms.ToolStripMenuItem mnuMaximize;
        private System.Windows.Forms.ToolStripMenuItem mnuMinimize;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripProject;
        private System.Windows.Forms.ToolStripMenuItem generateToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuProject;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private FastColoredTextBoxNS.FastColoredTextBox mapCodeFastColoredTextBox;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ToolStripMenuItem mnuCreateXML;
    }
}

