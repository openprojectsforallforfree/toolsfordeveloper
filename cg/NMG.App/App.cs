﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NMG.Core;
using NMG.Core.Domain;
using NMG.Core.Reader;
using NMG.Core.TextFormatter;

namespace NHibernateMappingGenerator
{
    public partial class App : Form
    {
        private readonly IList<int> _cachedTableListSelection = new List<int>();
        private readonly BackgroundWorker worker;
        private Connection _currentConnection;
        private Table _currentTable;
        private IList<Table> _tables;
        private ApplicationSettings applicationSettings;
        private IList<Column> gridData;
        private IMetadataReader metadataReader;
        public App()
        {
            InitializeComponent();
            ownersComboBox.SelectedIndexChanged += OwnersSelectedIndexChanged;
            tablesListBox.SelectedIndexChanged += TablesListSelectedIndexChanged;
            connectionNameComboBox.SelectedIndexChanged += ConnectionNameSelectedIndexChanged;
            dbTableDetailsGridView.DataError += DataError;
            connectionButton.Click += ConnectionButtonClick;
            dbTableDetailsGridView.CurrentCellDirtyStateChanged += OnTableDetailsCellDirty;
            BindData();

            sequencesComboBox.Enabled = false;
            TableFilterTextBox.Enabled = false;
            Closing += App_Closing;
            worker = new BackgroundWorker { WorkerSupportsCancellation = true };
        }

        public bool IsByCode
        {
            get { return byCodeMappingOption.Checked; }
        }

        public bool IsCastle
        {
            get { return castleMappingOption.Checked; }
        }

        public bool IsEntityFramework
        {
            get { return entityFrameworkRadionBtn.Checked; }
        }

        public bool IsFluent
        {
            get { return fluentMappingOption.Checked; }
        }

        private Language LanguageSelected
        {
            get { return vbRadioButton.Checked ? Language.VB : Language.CSharp; }
        }

        protected override void OnLoad(EventArgs e)
        {
            LoadApplicationSettings();
        }

        private static string AddSlashToFolderPath(string folderPath)
        {
            if (!folderPath.EndsWith(Path.DirectorySeparatorChar.ToString(CultureInfo.InvariantCulture)))
            {
                folderPath += Path.DirectorySeparatorChar;
            }
            return folderPath;
        }

        private void App_Closing(object sender, CancelEventArgs e)
        {
            CaptureApplicationSettings();
            applicationSettings.Save();
        }

        private void BindData()
        {
            columnName.DataPropertyName = "Name";
            isPrimaryKey.DataPropertyName = "IsPrimaryKey";
            isForeignKey.DataPropertyName = "IsForeignKey";
            isUniqueKey.DataPropertyName = "IsUnique";
            isNullable.DataPropertyName = "IsNullable";
            columnDataType.DataPropertyName = "DataType";
            cSharpType.DataPropertyName = "MappedDataType";
            cSharpType.DataSource = new DotNetTypes();
        }

        private void btnSaveXML_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (metadataReader == null)
            {
                MessageBox.Show("Connect to db first!");
            }
            else
            {
                var _tables = metadataReader.GetTables(ownersComboBox.SelectedItem.ToString());
                if (_tables.Count < 1)
                {
                    MessageBox.Show("Connect to table first!");
                    return;
                }
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    foreach (var item in _tables)
                    {
                        var gridData = metadataReader.GetTableDetails(item, ownersComboBox.SelectedItem.ToString()) ??
                                 new List<Column>();
                        item.Columns = gridData;
                    }

                    AllTables at = new AllTables();
                    at.DataBaseName = _currentConnection.Name;
                    at.Tables = _tables;
                    at.Save(sfd.FileName);
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (worker != null)
            {
                worker.CancelAsync();
            }
        }

        private void CaptureApplicationSettings()
        {
            if (applicationSettings == null)
            {
                applicationSettings = new ApplicationSettings();
            }

            applicationSettings.Language = cSharpRadioButton.Checked ? Language.CSharp : Language.VB;

            var validationStyle = ValidationStyle.None;
            if (dataAnnotationsRadioButton.Checked) validationStyle = ValidationStyle.Microsoft;
            if (nhibernateValidationRadioButton.Checked) validationStyle = ValidationStyle.Nhibernate;

            applicationSettings.ValidationStyle = validationStyle;
            applicationSettings.IsFluent = fluentMappingOption.Checked;
            applicationSettings.IsEntityFramework = entityFrameworkRadionBtn.Checked;
            applicationSettings.IsAutoProperty = autoPropertyRadioBtn.Checked;

            applicationSettings.InheritenceAndInterfaces = textBoxInheritence.Text;
            applicationSettings.ForeignEntityCollectionType = comboBoxForeignCollection.Text;
            applicationSettings.FieldPrefixRemovalList = applicationSettings.FieldPrefixRemovalList;
            applicationSettings.FieldNamingConvention = GetFieldNamingConvention();
            applicationSettings.Prefix = prefixTextBox.Text;
            applicationSettings.IsCastle = IsCastle;
            applicationSettings.ClassNamePrefix = textBoxClassNamePrefix.Text;
            applicationSettings.EnableInflections = EnableInflectionsCheckBox.Checked;
            applicationSettings.GeneratePartialClasses = partialClassesCheckBox.Checked;
            applicationSettings.GenerateWcfContracts = wcfDataContractCheckBox.Checked;
            applicationSettings.GenerateInFolders = generateInFoldersCheckBox.Checked;
            applicationSettings.IsByCode = IsByCode;
            applicationSettings.UseLazy = useLazyLoadingCheckBox.Checked;
            applicationSettings.IncludeForeignKeys = includeForeignKeysCheckBox.Checked;
            applicationSettings.NameFkAsForeignTable = nameAsForeignTableCheckBox.Checked;
            applicationSettings.IncludeHasMany = includeHasManyCheckBox.Checked;
            applicationSettings.IncludeLengthAndScale = includeLengthAndScaleCheckBox.Checked;
            applicationSettings.LastUsedConnection = _currentConnection == null ? (Guid?)null : _currentConnection.Id;
        }

        private void connectBtnClicked(object sender, EventArgs e)
        {
            if (_currentConnection == null)
                return;

            toolStripStatusLabel.Text = string.Format("Connecting to {0}...", _currentConnection.Name);
            statusStrip1.Refresh();
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                tablesListBox.DataSource = null;
                tablesListBox.DisplayMember = "Name";
                sequencesComboBox.Items.Clear();

                metadataReader = MetadataFactory.GetReader(_currentConnection.Type, _currentConnection.ConnectionString);

                toolStripStatusLabel.Text = "Retrieving owners...";
                statusStrip1.Refresh();
                PopulateOwners();

                toolStripStatusLabel.Text = string.Empty;
            }
            catch (Exception ex)
            {
                toolStripStatusLabel.Text = ex.Message;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void ConnectionButtonClick(object sender, EventArgs e)
        {
            // Belt and braces
            if (applicationSettings == null)
            {
                LoadApplicationSettings();
            }

            var connectionDialog = new ConnectionDialog();

            // Edit current connection
            if (_currentConnection != null)
            {
                connectionDialog.Connection = _currentConnection;
            }

            var result = connectionDialog.ShowDialog();
            switch (result)
            {
                case DialogResult.OK:
                    // Add or Update Connection
                    _currentConnection = connectionDialog.Connection;
                    var connectionToUpdate = applicationSettings.Connections.FirstOrDefault(connection => connection.Id == _currentConnection.Id);

                    if (connectionToUpdate == null)
                    {
                        // Add new connection
                        applicationSettings.Connections.Add(_currentConnection);
                    }

                    break;

                case DialogResult.Abort:
                    // Delete Connection
                    applicationSettings.Connections.Remove(_currentConnection);
                    _currentConnection = null;
                    break;
            }

            // Refresh data connections drop down
            connectionNameComboBox.DataSource = null;
            connectionNameComboBox.DataSource = applicationSettings.Connections;
            connectionNameComboBox.DisplayMember = "Name";
            connectionNameComboBox.SelectedItem = _currentConnection;
        }

        private void ConnectionNameSelectedIndexChanged(object sender, EventArgs e)
        {
            if (connectionNameComboBox.SelectedItem == null) return;

            _currentConnection = (Connection)connectionNameComboBox.SelectedItem;

            pOracleOnlyOptions.Hide();

            if (_currentConnection.Type == ServerType.Oracle)
                pOracleOnlyOptions.Show();
        }

        private void DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            toolStripStatusLabel.Text = string.Format("Error in column {0} of row {1} - {3}. Detail : {2}", e.ColumnIndex, e.RowIndex, e.Exception.Message, (gridData != null ? gridData[e.RowIndex].Name : ""));
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            var appSettings = e.Argument as ApplicationSettings;
            var items = tablesListBox.Items;
            var pOptions = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };
            Parallel.ForEach(items.Cast<Table>(), pOptions, (table, loopState) =>
            {
                if (worker != null && worker.CancellationPending && !loopState.IsStopped)
                {
                    loopState.Stop();
                    loopState.Break();
                    Thread.Sleep(1000);
                }
                string name = "";
                if (ownersComboBox.InvokeRequired)
                {
                    ownersComboBox.Invoke(new MethodInvoker(delegate
                    {
                        name = ownersComboBox.SelectedItem.ToString();
                    }));
                }
                else
                {
                    name = ownersComboBox.SelectedItem.ToString();
                }
                metadataReader.GetTableDetails(table, name);

            });
        }



        private void GenerateAllClicked(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = string.Empty;
            var items = tablesListBox.Items;
            if (items.Count == 0)
            {
                toolStripStatusLabel.Text = @"Please connect to a database to populate the tables first.";
                return;
            }
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    toolStripProgressBar1.Maximum = 100;
                    toolStripProgressBar1.Value = 10;
                    worker.DoWork += DoWork;
                    worker.RunWorkerCompleted += WorkerCompleted;
                    CaptureApplicationSettings();
                    worker.RunWorkerAsync(applicationSettings);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel.Text = ex.Message;
            }
        }

        private void GenerateClicked(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = string.Empty;
            var selectedItems = tablesListBox.SelectedItems;
            if (selectedItems.Count == 0)
            {
                toolStripStatusLabel.Text = @"Please select table(s) above to generate the mapping files.";
                return;
            }
            try
            {
                foreach (var selectedItem in selectedItems)
                {
                    toolStripStatusLabel.Text = string.Format("Generating {0} mapping file ...", selectedItem);
                    var table = (Table)selectedItem;
                    metadataReader.GetTableDetails(table, ownersComboBox.SelectedItem.ToString());
                    CaptureApplicationSettings();

                }
                toolStripStatusLabel.Text = @"Generated all files successfully.";
            }
            catch (Exception ex)
            {
                toolStripStatusLabel.Text = ex.Message;
            }
        }

        private ApplicationPreferences GetApplicationPreferences(Table tableName, bool all, ApplicationSettings appSettings)
        {
            string sequence = string.Empty;
            object sequenceName = null;
            if (sequencesComboBox.InvokeRequired)
            {
                sequencesComboBox.Invoke(new MethodInvoker(delegate
                {
                    sequenceName = sequencesComboBox.SelectedItem;
                }));
            }
            else
            {
                sequenceName = sequencesComboBox.SelectedItem;
            }
            if (sequenceName != null && !all)
            {
                sequence = sequenceName.ToString();
            }

            var applicationPreferences = new ApplicationPreferences
                                             {
                                                 ServerType = _currentConnection.Type,

                                                 TableName = tableName.Name,

                                                 Sequence = sequence,

                                                 FieldNamingConvention = GetFieldNamingConvention(),
                                                 FieldGenerationConvention = GetFieldGenerationConvention(),

                                                 ConnectionString = _currentConnection.ConnectionString,

                                             };

            return applicationPreferences;
        }

        private FieldGenerationConvention GetFieldGenerationConvention()
        {
            var convention = FieldGenerationConvention.Field;
            if (autoPropertyRadioBtn.Checked)
                convention = FieldGenerationConvention.AutoProperty;
            if (propertyRadioBtn.Checked)
                convention = FieldGenerationConvention.Property;
            return convention;
        }

        private FieldNamingConvention GetFieldNamingConvention()
        {
            var convention = FieldNamingConvention.SameAsDatabase;
            if (prefixRadioButton.Checked)
                convention = FieldNamingConvention.Prefixed;
            if (camelCasedRadioButton.Checked)
                convention = FieldNamingConvention.CamelCase;
            if (pascalCasedRadioButton.Checked)
                convention = FieldNamingConvention.PascalCase;
            return convention;
        }

        private void includeForeignKeysCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            nameAsForeignTableCheckBox.Enabled = includeForeignKeysCheckBox.Checked;
        }

        private int? LastTableSelected()
        {
            int? lastTableIndex = null;
            foreach (int i in tablesListBox.SelectedIndices)
            {
                if (_cachedTableListSelection.Contains(i))
                    continue;
                lastTableIndex = i;
                break;
            }
            _cachedTableListSelection.Clear();
            foreach (int i in tablesListBox.SelectedIndices)
                _cachedTableListSelection.Add(i);
            return lastTableIndex;
        }

        private void LoadApplicationSettings()
        {
            applicationSettings = ApplicationSettings.Load();
            if (applicationSettings != null)
            {
                // Display all previous connections
                connectionNameComboBox.DataSource = applicationSettings.Connections;
                connectionNameComboBox.DisplayMember = "Name";

                // Set the last used connection
                var lastUsedConnection =
                    applicationSettings.Connections.FirstOrDefault(connection => connection.Id == applicationSettings.LastUsedConnection);
                _currentConnection = lastUsedConnection ?? applicationSettings.Connections.FirstOrDefault();
                connectionNameComboBox.SelectedItem = _currentConnection;

                cSharpRadioButton.Checked = applicationSettings.Language == Language.CSharp;
                vbRadioButton.Checked = applicationSettings.Language == Language.VB;
                noValidationRadioButton.Checked = applicationSettings.ValidationStyle == ValidationStyle.None;
                nhibernateValidationRadioButton.Checked = applicationSettings.ValidationStyle == ValidationStyle.Nhibernate;
                dataAnnotationsRadioButton.Checked = applicationSettings.ValidationStyle == ValidationStyle.Microsoft;
                autoPropertyRadioBtn.Checked = applicationSettings.IsAutoProperty;

                textBoxInheritence.Text = applicationSettings.InheritenceAndInterfaces;
                comboBoxForeignCollection.Text = applicationSettings.ForeignEntityCollectionType;
                textBoxClassNamePrefix.Text = applicationSettings.ClassNamePrefix;
                EnableInflectionsCheckBox.Checked = applicationSettings.EnableInflections;
                wcfDataContractCheckBox.Checked = applicationSettings.GenerateWcfContracts;
                partialClassesCheckBox.Checked = applicationSettings.GeneratePartialClasses;
                useLazyLoadingCheckBox.Checked = applicationSettings.UseLazy;
                includeLengthAndScaleCheckBox.Checked = applicationSettings.IncludeLengthAndScale;
                includeForeignKeysCheckBox.Checked = applicationSettings.IncludeForeignKeys;
                nameAsForeignTableCheckBox.Checked = applicationSettings.NameFkAsForeignTable;
                includeHasManyCheckBox.Checked = applicationSettings.IncludeHasMany;

                fluentMappingOption.Checked = applicationSettings.IsFluent;
                entityFrameworkRadionBtn.Checked = applicationSettings.IsEntityFramework;
                castleMappingOption.Checked = applicationSettings.IsCastle;
                byCodeMappingOption.Checked = applicationSettings.IsByCode;

                if (applicationSettings.FieldPrefixRemovalList == null)
                    applicationSettings.FieldPrefixRemovalList = new List<string>();

                fieldPrefixListBox.Items.AddRange(applicationSettings.FieldPrefixRemovalList.ToArray());
                removeFieldPrefixButton.Enabled = false;

                prefixRadioButton.Checked = !string.IsNullOrEmpty(applicationSettings.Prefix);
                prefixTextBox.Text = applicationSettings.Prefix;
                camelCasedRadioButton.Checked = (applicationSettings.FieldNamingConvention == FieldNamingConvention.CamelCase);
                pascalCasedRadioButton.Checked = (applicationSettings.FieldNamingConvention == FieldNamingConvention.PascalCase);
                sameAsDBRadioButton.Checked = (applicationSettings.FieldNamingConvention == FieldNamingConvention.SameAsDatabase);

                sameAsDBRadioButton.Checked = (!prefixRadioButton.Checked && !pascalCasedRadioButton.Checked &&
                                               !camelCasedRadioButton.Checked);

                generateInFoldersCheckBox.Checked = applicationSettings.GenerateInFolders;
            }
            else
            {
                // Default application settings
                autoPropertyRadioBtn.Checked = true;
                pascalCasedRadioButton.Checked = true;
                cSharpRadioButton.Checked = true;
                byCodeMappingOption.Checked = true;
                includeForeignKeysCheckBox.Checked = true;
                nameAsForeignTableCheckBox.Checked = true;
                includeHasManyCheckBox.Checked = false;
                useLazyLoadingCheckBox.Checked = true;

                comboBoxForeignCollection.Text = "IList";

                CaptureApplicationSettings();
            }

            if (!prefixRadioButton.Checked)
            {
                prefixLabel.Visible = prefixTextBox.Visible = false;
            }
        }

        private void OnAddFieldPrefixButtonClick(object sender, EventArgs e)
        {
            // Check if the prefix has already been added.
            if (applicationSettings.FieldPrefixRemovalList.Any(s => s == fieldPrefixTextBox.Text))
            {
                fieldPrefixTextBox.Text = string.Empty;
                return;
            }

            if (string.IsNullOrEmpty(fieldPrefixTextBox.Text)) return;

            // Add the new prefix
            applicationSettings.FieldPrefixRemovalList.Add(fieldPrefixTextBox.Text);
            fieldPrefixListBox.Items.Clear();
            fieldPrefixListBox.Items.AddRange(applicationSettings.FieldPrefixRemovalList.ToArray());
            fieldPrefixTextBox.Text = string.Empty;
        }

        private void OnFieldPrefixListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            removeFieldPrefixButton.Enabled = fieldPrefixListBox.SelectedIndex != -1;
        }

        private void OnRemoveFieldPrefixButtonClick(object sender, EventArgs e)
        {
            if (fieldPrefixListBox.SelectedIndex == -1) return;

            applicationSettings.FieldPrefixRemovalList.Remove(fieldPrefixListBox.SelectedItem.ToString());
            fieldPrefixListBox.Items.Clear();
            fieldPrefixListBox.Items.AddRange(applicationSettings.FieldPrefixRemovalList.ToArray());
            fieldPrefixListBox.SelectedIndex = -1;
            removeFieldPrefixButton.Enabled = fieldPrefixListBox.SelectedIndex != -1;
        }

        private void OnTableDetailsCellDirty(object sender, EventArgs e)
        {
            if (_currentTable != null)
            {
                ToggleColumnsBasedOnAppSettings(applicationSettings);
            }
        }
        private void OnTableFilterEnter(object sender, EventArgs e)
        {
            var textbox = sender as TextBox;

            if (textbox == null) return;

            if (textbox.Text == textbox.Tag.ToString())
            {
                textbox.TextChanged -= OnTableFilterTextChanged;

                // Clear the hint text in the table filter textbox
                textbox.Text = string.Empty;

                textbox.TextChanged += OnTableFilterTextChanged;
            }
        }

        private void OnTableFilterTextChanged(object sender, EventArgs e)
        {
            var textbox = sender as TextBox;
            if (textbox == null) return;

            // Display the full table list
            if (string.IsNullOrEmpty(textbox.Text))
            {
                SuspendLayout();
                tablesListBox.ClearSelected();
                tablesListBox.DataSource = _tables;
                tablesListBox.SelectedItem = _tables.FirstOrDefault();
                ResumeLayout();
                return;
            }

            // Display filtered list of tables
            var query = (from t in _tables
                         where t.Name.ToLower().Contains(textbox.Text.ToLower())
                         select t).ToList();

            SuspendLayout();
            tablesListBox.ClearSelected();
            tablesListBox.DataSource = query;
            tablesListBox.SelectedItem = query.FirstOrDefault();
            ResumeLayout();
        }

        private void OwnersSelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                PopulateTablesAndSequences();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #region Populate
        private void PopulateOwners()
        {
            var owners = metadataReader.GetOwners();
            if (owners == null || owners.Count == 0)
            {
                owners = new List<string> { "dbo" };
            }

            tablesListBox.SelectedIndexChanged -= TablesListSelectedIndexChanged;

            ownersComboBox.Items.Clear();
            ownersComboBox.Items.AddRange(owners.ToArray());

            tablesListBox.SelectedIndexChanged += TablesListSelectedIndexChanged;
            ownersComboBox.SelectedIndex = 0;
        }

        private void PopulateTableDetails(Table selectedTable)
        {
            toolStripStatusLabel.Text = string.Empty;
            try
            {
                //dbTableDetailsGridView.AutoGenerateColumns = true;
                _currentTable = selectedTable;
                gridData = metadataReader.GetTableDetails(selectedTable, ownersComboBox.SelectedItem.ToString()) ??
                           new List<Column>();

                // Show table details, and toggle columns based on app settings
                dbTableDetailsGridView.SuspendLayout();
                dbTableDetailsGridView.DataSource = gridData;
                dbTableDetailsGridView.ResumeLayout();
            }
            catch (Exception ex)
            {
                toolStripStatusLabel.Text = ex.Message;
            }
        }

        private void PopulateTablesAndSequences()
        {
            try
            {
                toolStripStatusLabel.Text = "Retrieving tables...";
                statusStrip1.Refresh();

                if (ownersComboBox.SelectedItem == null)
                {
                    tablesListBox.DataSource = new List<Table>();
                    return;
                }
                _tables = metadataReader.GetTables(ownersComboBox.SelectedItem.ToString());
                tablesListBox.Enabled = false;
                TableFilterTextBox.Enabled = false;
                tablesListBox.DataSource = _tables;
                tablesListBox.DisplayMember = "Name";

                if (_tables != null && _tables.Any())
                {
                    tablesListBox.Enabled = true;
                    TableFilterTextBox.Enabled = true;
                    tablesListBox.SelectedItem = _tables.FirstOrDefault();
                }

                var sequences = metadataReader.GetSequences(ownersComboBox.SelectedItem.ToString());
                sequencesComboBox.Enabled = false;
                sequencesComboBox.Items.Clear();
                if (sequences != null && sequences.Any())
                {
                    sequencesComboBox.Items.AddRange(sequences.ToArray());
                    sequencesComboBox.Enabled = true;
                    sequencesComboBox.SelectedIndex = 0;
                }

                toolStripStatusLabel.Text = string.Empty;
                statusStrip1.Refresh();
            }
            catch (Exception ex)
            {
                toolStripStatusLabel.Text = ex.Message;
            }
        }
        #endregion

        private void prefixCheckChanged(object sender, EventArgs e)
        {
            prefixLabel.Visible = prefixTextBox.Visible = prefixRadioButton.Checked;
        }

        private void TablesListSelectedIndexChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = string.Empty;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (tablesListBox.SelectedIndex == -1)
                {
                    dbTableDetailsGridView.DataSource = new List<Column>();
                    return;
                }

                var lastTableSelectedIndex = LastTableSelected();
                if (lastTableSelectedIndex != null)
                {
                    var table = tablesListBox.Items[lastTableSelectedIndex.Value] as Table;

                    if (table != null)
                    {
                        CaptureApplicationSettings();

                        PopulateTableDetails(table);

                        ToggleColumnsBasedOnAppSettings(applicationSettings);

                        // Display entity name based on formatted table name
                        var appPreferences = GetApplicationPreferences(table, false, applicationSettings);
                        var formatter = TextFormatterFactory.GetTextFormatter(appPreferences);
                    }
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel.Text = ex.Message;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void ToggleColumnsBasedOnAppSettings(ApplicationSettings appSettings)
        {
            var lengthColumn = dbTableDetailsGridView.Columns["DataLength"];
            if (lengthColumn != null)
                lengthColumn.Visible = appSettings.IncludeLengthAndScale;

            var precisionColumn = dbTableDetailsGridView.Columns["DataPrecision"];
            if (precisionColumn != null)
                precisionColumn.Visible = appSettings.IncludeLengthAndScale;

            var scaleColumn = dbTableDetailsGridView.Columns["DataScale"];
            if (scaleColumn != null)
                scaleColumn.Visible = appSettings.IncludeLengthAndScale;

            var cSharpTypeColumn = dbTableDetailsGridView.Columns["cSharpType"];
            if (cSharpTypeColumn != null)
                cSharpTypeColumn.Visible = !appSettings.IsByCode;

            /*var fkTableNameColumn = dbTableDetailsGridView.Columns["ForeignKeyTableName"];
            var fkColNameColumn = dbTableDetailsGridView.Columns["ForeignKeyColumnName"];
            if (fkColNameColumn != null && fkTableNameColumn != null)
            {
                if (_currentTable.ForeignKeys.Count != 0)
                {
                    // Disable foreign key columns
                    fkTableNameColumn.ReadOnly = false;
                    fkColNameColumn.ReadOnly = false;
                }
                else
                {
                    // Enable foreign key columns
                    fkTableNameColumn.ReadOnly = true;
                    fkColNameColumn.ReadOnly = true;
                }
            }*/
        }
        private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripProgressBar1.Value = 100;
            toolStripStatusLabel.Text = @"Generated all files successfully.";
        }
    }
}