<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRegex
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
        Me.txtOk = New System.Windows.Forms.Button
        Me.FileBrowser2 = New CG.fileBrowser
        Me.FileBrowser1 = New CG.fileBrowser
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'txtOk
        '
        Me.txtOk.Location = New System.Drawing.Point(475, 112)
        Me.txtOk.Name = "txtOk"
        Me.txtOk.Size = New System.Drawing.Size(94, 42)
        Me.txtOk.TabIndex = 1
        Me.txtOk.Text = "Ok"
        Me.txtOk.UseVisualStyleBackColor = True
        '
        'FileBrowser2
        '
        Me.FileBrowser2.BackColor = System.Drawing.Color.Transparent
        Me.FileBrowser2.browsertype = CG.filebrowsertype.openfile
        Me.FileBrowser2.filter = ""
        Me.FileBrowser2.Location = New System.Drawing.Point(111, 71)
        Me.FileBrowser2.Name = "FileBrowser2"
        Me.FileBrowser2.path = ""
        Me.FileBrowser2.Size = New System.Drawing.Size(458, 23)
        Me.FileBrowser2.TabIndex = 2
        '
        'FileBrowser1
        '
        Me.FileBrowser1.BackColor = System.Drawing.Color.Transparent
        Me.FileBrowser1.browsertype = CG.filebrowsertype.openfile
        Me.FileBrowser1.filter = ""
        Me.FileBrowser1.Location = New System.Drawing.Point(111, 29)
        Me.FileBrowser1.Name = "FileBrowser1"
        Me.FileBrowser1.path = ""
        Me.FileBrowser1.Size = New System.Drawing.Size(458, 23)
        Me.FileBrowser1.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(53, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "InputFile"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(53, 80)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "OutputFile"
        '
        'frmRegex
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(608, 175)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.FileBrowser2)
        Me.Controls.Add(Me.FileBrowser1)
        Me.Controls.Add(Me.txtOk)
        Me.Name = "frmRegex"
        Me.Text = "frmRegex"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtOk As System.Windows.Forms.Button
    Friend WithEvents FileBrowser1 As CG.fileBrowser
    Friend WithEvents FileBrowser2 As CG.fileBrowser
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
