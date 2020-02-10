<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectMainTable
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cmbTables = New System.Windows.Forms.ComboBox
        Me.btnOk = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'cmbTables
        '
        Me.cmbTables.FormattingEnabled = True
        Me.cmbTables.Location = New System.Drawing.Point(76, 45)
        Me.cmbTables.Name = "cmbTables"
        Me.cmbTables.Size = New System.Drawing.Size(210, 21)
        Me.cmbTables.TabIndex = 0
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(147, 92)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(75, 28)
        Me.btnOk.TabIndex = 1
        Me.btnOk.Text = "Ok"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'frmSelectMainTable
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(359, 132)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.cmbTables)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmSelectMainTable"
        Me.Text = "frmSelectMainTable"
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents cmbTables As System.Windows.Forms.ComboBox
    Friend WithEvents btnOk As System.Windows.Forms.Button
End Class
