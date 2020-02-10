<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDuplicationForm
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
        Me.txtText = New System.Windows.Forms.TextBox
        Me.txtList = New System.Windows.Forms.TextBox
        Me.btnOk = New System.Windows.Forms.Button
        Me.txtFind = New System.Windows.Forms.TextBox
        Me.txtDelimiter = New System.Windows.Forms.TextBox
        Me.btnWithDelimiter = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtOut = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'txtText
        '
        Me.txtText.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtText.Location = New System.Drawing.Point(369, 32)
        Me.txtText.Multiline = True
        Me.txtText.Name = "txtText"
        Me.txtText.Size = New System.Drawing.Size(724, 451)
        Me.txtText.TabIndex = 0
        '
        'txtList
        '
        Me.txtList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtList.Location = New System.Drawing.Point(12, 71)
        Me.txtList.Multiline = True
        Me.txtList.Name = "txtList"
        Me.txtList.Size = New System.Drawing.Size(351, 621)
        Me.txtList.TabIndex = 0
        '
        'btnOk
        '
        Me.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnOk.Location = New System.Drawing.Point(892, 661)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(84, 30)
        Me.btnOk.TabIndex = 1
        Me.btnOk.Text = "Ok"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'txtFind
        '
        Me.txtFind.Location = New System.Drawing.Point(15, 32)
        Me.txtFind.Name = "txtFind"
        Me.txtFind.Size = New System.Drawing.Size(351, 20)
        Me.txtFind.TabIndex = 2
        '
        'txtDelimiter
        '
        Me.txtDelimiter.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.txtDelimiter.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDelimiter.Location = New System.Drawing.Point(424, 661)
        Me.txtDelimiter.Name = "txtDelimiter"
        Me.txtDelimiter.Size = New System.Drawing.Size(66, 26)
        Me.txtDelimiter.TabIndex = 2
        Me.txtDelimiter.Text = ","
        '
        'btnWithDelimiter
        '
        Me.btnWithDelimiter.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnWithDelimiter.Location = New System.Drawing.Point(982, 662)
        Me.btnWithDelimiter.Name = "btnWithDelimiter"
        Me.btnWithDelimiter.Size = New System.Drawing.Size(111, 30)
        Me.btnWithDelimiter.TabIndex = 1
        Me.btnWithDelimiter.Text = "Use Delimiter,Ok"
        Me.btnWithDelimiter.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(371, 671)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Delimiter"
        '
        'txtOut
        '
        Me.txtOut.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOut.Location = New System.Drawing.Point(369, 511)
        Me.txtOut.Multiline = True
        Me.txtOut.Name = "txtOut"
        Me.txtOut.Size = New System.Drawing.Size(724, 144)
        Me.txtOut.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Replace"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(27, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Find"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(366, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(31, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Input"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(371, 486)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Output"
        '
        'frmDuplicationForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1105, 704)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtDelimiter)
        Me.Controls.Add(Me.txtFind)
        Me.Controls.Add(Me.btnWithDelimiter)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.txtList)
        Me.Controls.Add(Me.txtOut)
        Me.Controls.Add(Me.txtText)
        Me.Name = "frmDuplicationForm"
        Me.Text = "Duplication"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtText As System.Windows.Forms.TextBox
    Friend WithEvents txtList As System.Windows.Forms.TextBox
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents txtFind As System.Windows.Forms.TextBox
    Friend WithEvents txtDelimiter As System.Windows.Forms.TextBox
    Friend WithEvents btnWithDelimiter As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtOut As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
