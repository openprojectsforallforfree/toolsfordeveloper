Imports System.Text
Public Enum ControlEnum
    TextBox
    ComboBox
    Label
    Grid
    GroupBox
    RadioButton
    CheckBox
End Enum
Public Class clsDeclare_Control
    Inherits BasicFrame
    Implements Frame

    
    Public Overrides Sub GenCreate_Afterlines()
    End Sub
    Public Overrides Sub GenCreate_Beforelines()
    End Sub
    Public Overrides Sub GenCreate_loop()
        getcontrol()
    End Sub
    Public Overrides Sub GenCreate_loop_Fistline()
        GenCreate_loop()
    End Sub
    Public Overrides Sub GenCreate_loop_Lastline()
        GenCreate_loop()
    End Sub
    Public Sub getcontrol()
        Select Case Cntrol
            Case "lblComboBox"

                outText.AppendLine(" private Bsoft.Controls.lblComboBox " & controlName & "; ")

            Case "lblTextBox"

                outText.AppendLine(" private Bsoft.Controls.lblTextBox " & controlName & "; ")

            Case "TextBox"

                outText.AppendLine(" private System.Windows.Forms.TextBox " & controlName & "; ")

            Case "ComboBox"

                outText.AppendLine(" private System.Windows.Forms.ComboBox " & controlName & "; ")

            Case "Label"

                outText.AppendLine(" private System.Windows.Forms.Label  " & "lbl" & dbname & "; ")

            Case "Grid"

                outText.AppendLine(" private System.Windows.Forms.DataGridViewTextBoxColumn  " & colName & "; ")

            Case "GroupBox"

                outText.AppendLine(" private System.Windows.Forms.GroupBox " & controlName & "; ")

            Case "RadioButton"

                outText.AppendLine(" private System.Windows.Forms.RadioButton " & controlName & "; ")

            Case "CheckBox"

                outText.AppendLine(" private System.Windows.Forms.CheckBox " & controlName & "; ")

            Case "VtextBox"

                outText.AppendLine(" private Bsoft.Controls.VtextBox " & controlName & "; ")

            Case "DateTimePicker"

                outText.AppendLine(" System.Windows.Forms.DateTimePicker " & controlName & "; ")

        End Select
    End Sub
End Class

Public Class clsControl
    Inherits BasicFrame
    Implements Frame
    Dim left_s As Integer = 0
    Dim top_s As Integer = 0
    Dim i As Integer = 0
    Public Overrides Sub GenCreate_Afterlines()
    End Sub
    Public Overrides Sub GenCreate_Beforelines()
    End Sub
    Public Overrides Sub GenCreate_loop()
        getcontrol("this")
    End Sub
    Public Overrides Sub GenCreate_loop_Fistline()
        GenCreate_loop()
    End Sub
    Public Overrides Sub GenCreate_loop_Lastline()
        GenCreate_loop()
    End Sub

    Public Sub getcontrol(ByVal parent As String)
        Select Case Cntrol
            Case "lblComboBox"

                ' left_s = left_s + 10
                top_s = top_s + 30
                outText.AppendLine("// ")
                outText.AppendLine("//" & controlName)
                outText.AppendLine("// ")
                outText.AppendLine(" this." & controlName & " = new Bsoft.Controls.lblComboBox();")
                outText.AppendLine(" this." & controlName & " .BackColor = System.Drawing.Color.Transparent;")
                outText.AppendLine(" this." & controlName & " .Name = " & q & controlName & q & ";")
                parent = "flwLayout"
                outText.AppendLine(" this." & parent & ".Controls.Add(this." & controlName & ");")

                If txt.Trim() = "" Then
                    txt = dbname
                End If
                outText.AppendLine(" this." & controlName & " .Label = " & q & txt & q & ";")

            Case "lblTextBox"

                ' left_s = left_s + 10
                top_s = top_s + 30
                outText.AppendLine("// ")
                outText.AppendLine("//" & controlName)
                outText.AppendLine("// ")
                outText.AppendLine(" this." & controlName & " = new Bsoft.Controls.lblTextBox();")
                outText.AppendLine(" this." & controlName & " .BackColor = System.Drawing.Color.Transparent;")
                outText.AppendLine(" this." & controlName & " .Name = " & q & controlName & q & ";")
                If type.ToLower() = "integer" Or type.ToLower.Contains("float") Then
                    outText.AppendLine(" this." & controlName & ".vtxtBox.ValidationType = Bsoft.Controls.VtextBox.ValidationTypeEnum.Integer;")
                End If
                'make id invisble,disabled by default
                If dbname.ToLower() = "id" Then
                    outText.AppendLine(" this." & controlName & " .Visible = false ;")
                    outText.AppendLine(" this." & controlName & " .Enabled = false ;")
                End If
                'For limiting the input characters eg for varchar
                Dim getnum As Integer = 0
                getnum = getNumber(type)
                If getnum > 0 Then
                    outText.AppendLine(" this." & controlName & ".vtxtBox.MaxLength = " & getnum.ToString() & ";")
                End If
                parent = "flwLayout"
                outText.AppendLine(" this." & parent & ".Controls.Add(this." & controlName & ");")

                If txt.Trim() = "" Then
                    txt = dbname
                End If
                outText.AppendLine(" this." & controlName & " .Label = " & q & txt & q & ";")

            Case "TextBox"

                ' left_s = left_s + 10
                top_s = top_s + 30
                outText.AppendLine("// ")
                outText.AppendLine("//" & controlName)
                outText.AppendLine("// ")
                outText.AppendLine(" this." & controlName & " = new System.Windows.Forms.TextBox();")
                outText.AppendLine(" this." & controlName & " .Location = new System.Drawing.Point(" & left_s.ToString() & ", " & top_s.ToString() & ");")
                outText.AppendLine(" this." & controlName & " .Name = " & q & controlName & q & ";")

                Dim getnum As Integer = 0
                getnum = getNumber(type)
                If getnum > 0 Then
                    outText.AppendLine(" this." & controlName & " .MaxLength = " & getnum.ToString() & ";")
                End If
                outText.AppendLine(" this." & controlName & " .Size = new System.Drawing.Size(160, 20);")
                'outText.AppendLine(" this." & controlName & " .Text = " & q & txt & q & ";")
                outText.AppendLine(" this." & controlName & " .TabIndex = " & i.ToString() & ";")
                If parent = "this" Then
                    outText.AppendLine(" this.Controls.Add(this." & controlName & ");")
                Else
                    outText.AppendLine(" this." & parent & ".Controls.Add(this." & controlName & ");")
                End If

            Case "ComboBox"

                ' left_s = left_s + 10
                top_s = top_s + 30
                outText.AppendLine("// ")
                outText.AppendLine("//" & controlName)
                outText.AppendLine("// ")
                outText.AppendLine(" this." & controlName & " = new System.Windows.Forms.ComboBox();")
                outText.AppendLine(" this." & controlName & " .FormattingEnabled = true;")
                outText.AppendLine(" this." & controlName & " .Location = new System.Drawing.Point(" & left_s.ToString() & ", " & top_s.ToString() & ");")
                outText.AppendLine(" this." & controlName & " .Name = " & q & controlName & q & ";")
                outText.AppendLine(" this." & controlName & " .Size = new System.Drawing.Size(160, 20);")
                outText.AppendLine(" this." & controlName & " .TabIndex = " & i.ToString() & ";")
                If parent = "this" Then
                    outText.AppendLine(" this.Controls.Add(this." & controlName & ");")
                Else
                    outText.AppendLine(" this." & parent & ".Controls.Add(this." & controlName & ");")
                End If

            Case "Label"

                top_s = top_s + 30
                outText.AppendLine("// ")
                outText.AppendLine("// " & "lbl" & dbname)
                outText.AppendLine("// ")
                outText.AppendLine("this." & "lbl" & dbname & " = new System.Windows.Forms.Label();")
                outText.AppendLine("this." & "lbl" & dbname & ".BackColor = System.Drawing.Color.Transparent;")
                outText.AppendLine("this." & "lbl" & dbname & ".Location =new System.Drawing.Point(" & left_s.ToString() & ", " & top_s.ToString() & ");")
                outText.AppendLine("this." & "lbl" & dbname & ".Name =" & q & controlName & q & ";")
                outText.AppendLine("this." & "lbl" & dbname & ".Size = new System.Drawing.Size(160, 22);")
                outText.AppendLine("this." & "lbl" & dbname & ".TabIndex = 0;")
                If txt.Trim() = "" Then
                    txt = dbname
                End If
                outText.AppendLine(" this." & controlName & " .Text = " & q & txt & q & ";")
                outText.AppendLine("this." & "lbl" & dbname & ".TextAlign = System.Drawing.ContentAlignment.MiddleRight;")
                If parent = "this" Then
                    outText.AppendLine(" this.Controls.Add(this." & controlName & ");")
                Else
                    outText.AppendLine(" this." & parent & ".Controls.Add(this." & controlName & ");")

                End If
            Case "Grid"

                outText.AppendLine("// ")
                outText.AppendLine("// " & colName)
                outText.AppendLine("// ")
                outText.AppendLine("this." & colName & " = new System.Windows.Forms.DataGridViewTextBoxColumn();")

                outText.AppendLine("this." & colName & ".AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;")
                If txt.Trim() = "" Then
                    txt = dbname
                End If
                outText.AppendLine(" this." & colName & " .HeaderText = " & q & txt & q & ";")
                outText.AppendLine(" this." & colName & " .Tag = " & q & dbname & q & ";")
                outText.AppendLine("this." & colName & ".Name = " & q & colName & q & ";")


            Case "GroupBox"

                ' left_s = left_s + 10
                top_s = top_s + 30
                outText.AppendLine("// ")
                outText.AppendLine("//" & controlName)
                outText.AppendLine("// ")
                outText.AppendLine(" this." & controlName & " = new System.Windows.Forms.GroupBox();")
                outText.AppendLine(" this." & controlName & " .Location = new System.Drawing.Point(" & left_s.ToString() & ", " & top_s.ToString() & ");")
                outText.AppendLine(" this." & controlName & " .Name = " & q & controlName & q & ";")
                outText.AppendLine(" this." & controlName & " .Size = new System.Drawing.Size(160, 20);")
                outText.AppendLine(" this." & controlName & " .TabIndex = " & i.ToString() & ";")

                outText.AppendLine(" this." & controlName & " .BackColor = System.Drawing.Color.Transparent; ")
                outText.AppendLine(" this." & controlName & " .TabStop = false;")
                outText.AppendLine(" this." & controlName & " .Text = " & q & txt & q & ";")

                If parent = "this" Then
                    outText.AppendLine(" this.Controls.Add(this." & controlName & ");")
                Else
                    outText.AppendLine(" this." & parent & ".Controls.Add(this." & controlName & ");")
                End If

            Case "RadioButton"

                ' left_s = left_s + 10
                top_s = top_s + 30
                outText.AppendLine("// ")
                outText.AppendLine("//" & controlName)
                outText.AppendLine("// ")
                outText.AppendLine(" this." & controlName & " = new System.Windows.Forms.RadioButton();")
                outText.AppendLine(" this." & controlName & ".UseVisualStyleBackColor = true;")
                outText.AppendLine(" this." & controlName & ".AutoSize = true;")
                outText.AppendLine(" this." & controlName & " .Location = new System.Drawing.Point(" & left_s.ToString() & ", " & top_s.ToString() & ");")
                outText.AppendLine(" this." & controlName & " .Name = " & q & controlName & q & ";")
                outText.AppendLine(" this." & controlName & " .Size = new System.Drawing.Size(160, 20);")
                outText.AppendLine(" this." & controlName & " .TabIndex = " & i.ToString() & ";")
                If txt.Trim() = "" Then
                    txt = dbname
                End If
                outText.AppendLine(" this." & controlName & " .Text = " & q & txt & q & ";")
                If parent = "this" Then
                    outText.AppendLine(" this.Controls.Add(this." & controlName & ");")
                Else
                    outText.AppendLine(" this." & parent & ".Controls.Add(this." & controlName & ");")
                End If

            Case "CheckBox"

                ' left_s = left_s + 10
                top_s = top_s + 30
                outText.AppendLine("// ")
                outText.AppendLine("//" & controlName)
                outText.AppendLine("// ")
                outText.AppendLine(" this." & controlName & " = new System.Windows.Forms.CheckBox();")
                outText.AppendLine(" this." & controlName & ".UseVisualStyleBackColor = true;")
                outText.AppendLine(" this." & controlName & ".AutoSize = true;")
                outText.AppendLine(" this." & controlName & " .Location = new System.Drawing.Point(" & left_s.ToString() & ", " & top_s.ToString() & ");")
                outText.AppendLine(" this." & controlName & " .Name = " & q & controlName & q & ";")
                outText.AppendLine(" this." & controlName & " .Size = new System.Drawing.Size(160, 20);")
                If txt.Trim() = "" Then
                    txt = dbname
                End If
                outText.AppendLine(" this." & controlName & " .Text = " & q & txt & q & ";")
                outText.AppendLine(" this." & controlName & " .TabIndex = " & i.ToString() & ";")
                If parent = "this" Then
                    outText.AppendLine(" this.Controls.Add(this." & controlName & ");")
                Else
                    outText.AppendLine(" this." & parent & ".Controls.Add(this." & controlName & ");")
                End If

            Case "VtextBox"

                ' left_s = left_s + 10
                top_s = top_s + 30
                outText.AppendLine("// ")
                outText.AppendLine("//" & controlName)
                outText.AppendLine("// ")
                outText.AppendLine(" this." & controlName & " = new Bsoft.Controls.VtextBox();")
                outText.AppendLine(" this." & controlName & " .Location = new System.Drawing.Point(" & left_s.ToString() & ", " & top_s.ToString() & ");")
                outText.AppendLine(" this." & controlName & " .Name = " & q & controlName & q & ";")
                outText.AppendLine(" this." & controlName & " .Size = new System.Drawing.Size(160, 20);")
                If type.ToLower() = "Integer".ToLower() Then
                    outText.AppendLine(" this." & controlName & " .ValidationType = Bsoft.Controls.VtextBox.ValidationTypeEnum.Integer;")
                Else
                    outText.AppendLine(" this." & controlName & " .ValidationType = Bsoft.Controls.VtextBox.ValidationTypeEnum.No_Validation;")
                End If


                outText.AppendLine(" this." & controlName & " .Value = " & q & q & ";")
                outText.AppendLine(" this." & controlName & " .TabIndex = " & i.ToString() & ";")
                If parent = "this" Then
                    outText.AppendLine(" this.Controls.Add(this." & controlName & ");")
                Else
                    outText.AppendLine(" this." & parent & ".Controls.Add(this." & controlName & ");")
                End If

            Case "DateTimePicker"

                ' left_s = left_s + 10
                top_s = top_s + 30
                outText.AppendLine("// ")
                outText.AppendLine("//" & controlName)
                outText.AppendLine("// ")
                outText.AppendLine(" this." & controlName & " = new System.Windows.Forms.DateTimePicker();")
                outText.AppendLine(" this." & controlName & " .Location = new System.Drawing.Point(" & left_s.ToString() & ", " & top_s.ToString() & ");")
                outText.AppendLine(" this." & controlName & " .Name = " & q & controlName & q & ";")
                outText.AppendLine(" this." & controlName & " .Size = new System.Drawing.Size(110, 20);")
                outText.AppendLine(" this." & controlName & ".CustomFormat = " & q & "dd/MMM/yyyy" & q & ";")
                outText.AppendLine(" this." & controlName & ".Format = System.Windows.Forms.DateTimePickerFormat.Custom;")
                outText.AppendLine(" this." & controlName & " .TabIndex = " & i.ToString() & ";")
                If parent = "this" Then
                    outText.AppendLine(" this.Controls.Add(this." & controlName & ");")
                Else
                    outText.AppendLine(" this." & parent & ".Controls.Add(this." & controlName & ");")
                End If

        End Select
    End Sub
   
End Class
