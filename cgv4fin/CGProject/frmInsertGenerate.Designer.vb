<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInsertGenerate
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.gridTables = New System.Windows.Forms.DataGridView
        Me.gridFields = New System.Windows.Forms.DataGridView
        Me.Button1 = New System.Windows.Forms.Button
        Me.txtOut = New System.Windows.Forms.TextBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.gridDaata = New System.Windows.Forms.DataGridView
        Me.chkShowData = New System.Windows.Forms.CheckBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        CType(Me.gridTables, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridFields, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridDaata, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gridTables
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gridTables.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.gridTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gridTables.DefaultCellStyle = DataGridViewCellStyle2
        Me.gridTables.GridColor = System.Drawing.Color.White
        Me.gridTables.Location = New System.Drawing.Point(12, 32)
        Me.gridTables.Name = "gridTables"
        Me.gridTables.Size = New System.Drawing.Size(247, 432)
        Me.gridTables.TabIndex = 0
        '
        'gridFields
        '
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.Teal
        Me.gridFields.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.gridFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gridFields.DefaultCellStyle = DataGridViewCellStyle4
        Me.gridFields.Location = New System.Drawing.Point(268, 32)
        Me.gridFields.Name = "gridFields"
        Me.gridFields.Size = New System.Drawing.Size(247, 432)
        Me.gridFields.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(481, 556)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(88, 32)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Generate"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtOut
        '
        Me.txtOut.Location = New System.Drawing.Point(12, 480)
        Me.txtOut.Multiline = True
        Me.txtOut.Name = "txtOut"
        Me.txtOut.Size = New System.Drawing.Size(811, 69)
        Me.txtOut.TabIndex = 4
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(355, 556)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(126, 31)
        Me.Button2.TabIndex = 5
        Me.Button2.Text = "Configuration String"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'gridDaata
        '
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gridDaata.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.gridDaata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.PeachPuff
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gridDaata.DefaultCellStyle = DataGridViewCellStyle6
        Me.gridDaata.Location = New System.Drawing.Point(525, 35)
        Me.gridDaata.MultiSelect = False
        Me.gridDaata.Name = "gridDaata"
        Me.gridDaata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gridDaata.Size = New System.Drawing.Size(299, 427)
        Me.gridDaata.TabIndex = 1
        '
        'chkShowData
        '
        Me.chkShowData.AutoSize = True
        Me.chkShowData.Checked = True
        Me.chkShowData.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShowData.Location = New System.Drawing.Point(627, 561)
        Me.chkShowData.Name = "chkShowData"
        Me.chkShowData.Size = New System.Drawing.Size(79, 17)
        Me.chkShowData.TabIndex = 7
        Me.chkShowData.Text = "Show Data"
        Me.chkShowData.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Tables"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(268, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Fields"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(529, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Data"
        '
        'frmInsertGenerate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(834, 595)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.chkShowData)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.txtOut)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.gridDaata)
        Me.Controls.Add(Me.gridFields)
        Me.Controls.Add(Me.gridTables)
        Me.Name = "frmInsertGenerate"
        Me.Text = "frmInsertGenerate"
        CType(Me.gridTables, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridFields, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridDaata, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gridTables As System.Windows.Forms.DataGridView
    Friend WithEvents gridFields As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtOut As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents gridDaata As System.Windows.Forms.DataGridView
    Friend WithEvents chkShowData As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
