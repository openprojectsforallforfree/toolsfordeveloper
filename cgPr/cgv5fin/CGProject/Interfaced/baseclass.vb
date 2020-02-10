Imports System.Text
'Help
'Uses Text for checkbox,radio button and label only

Public Class BasicFrame
    Implements Frame

#Region "declaration"
    Friend Const q = """"
    ' " & q & "
    Friend tablename As String = ""
    Friend outText As New StringBuilder
    
    Dim left_s As Integer = 0
    Dim top_s As Integer = 0
    Friend dbname As String = "", type As String = "", constraint As String = "", txt As String = ""
    Friend Cntrol As String = ""

    Friend controlName, CrudeType, colName As String

    Friend forceControl As String = ""
    Dim what As String

#End Region

#Region "Interface parts"
    Public Overridable Sub GenCreate_Afterlines() Implements Frame.GenCreate_Afterlines
    End Sub

    Public Overridable Sub GenCreate_Beforelines() Implements Frame.GenCreate_Beforelines
    End Sub

    Public Overridable Sub GenCreate_loop() Implements Frame.GenCreate_loop
    End Sub

    Public Overridable Sub GenCreate_loop_Fistline() Implements Frame.GenCreate_loop_Fistline
    End Sub

    Public Overridable Sub GenCreate_loop_Lastline() Implements Frame.GenCreate_loop_Lastline
    End Sub
#End Region

#Region "Gencreate"
    Public Function gencreate(ByVal starting_left As Integer, ByVal starting_top As Integer, ByVal dt As DataTable) As String
        left_s = starting_left
        top_s = starting_top
        Return (gencreate(dt))
    End Function

    Public Overridable Function gencreate(ByVal dt As DataTable) As String Implements Frame.gencreate
        tablename = dt.TableName
        Dim order As Integer = 0
        GenCreate_Beforelines()
        For Each dr As DataRow In dt.Rows
            correctValues(dr("Table").ToString(), dr("Type").ToString(), dr("Constraint").ToString(), dr("Text").ToString(), dr("Control").ToString())
            If order = 0 Then
                GenCreate_loop_Fistline()
            ElseIf order = dt.Rows.Count - 1 Then
                GenCreate_loop_Lastline()
            Else
                GenCreate_loop()
            End If
            order = order + 1
        Next
        GenCreate_Afterlines()
        Return outText.ToString()
    End Function

#End Region

#Region "Correct values"

    Public Sub correctValues(ByVal _dbname As String, ByVal _type As String, ByVal _constraint As String, ByVal _txt As String, ByVal _control As String)
        'find if empty
        dbname = If(_dbname = Nothing, String.Empty, _dbname)
        type = If(_type = Nothing, String.Empty, _type)
        constraint = If(_constraint = Nothing, String.Empty, _constraint)
        txt = If(_txt = Nothing, String.Empty, _txt)
        Cntrol = If(_control = Nothing, String.Empty, _control)
        dbname = dbname.Replace(" ", "")
        'control 
        If forceControl <> "NONE" And forceControl <> Nothing Then
            Cntrol = forceControl
        End If
        Select Case Cntrol
            Case "lblComboBox"
                controlName = "cmb" + dbname
            Case "lblTextBox"
                controlName = "txt" + dbname
            Case "TextBox"
                controlName = "txt" + dbname
            Case "Label"
                controlName = "lbl" + dbname
            Case "ComboBox"
                controlName = "cmb" + dbname
            Case "CheckBox"
                controlName = "chk" + dbname
            Case "GroupBox"
                controlName = "grp" + dbname
            Case "Grid"
                controlName = "col" + dbname
            Case "RadioButton"
                controlName = "rdo" + dbname
            Case "DateTimePicker"
                controlName = "dt" + dbname
            Case "VtextBox"
                controlName = "txt" + dbname
            Case Else
                controlName = "oth" + dbname
        End Select
        'type
        CrudeType = getCrudeType(type)
        'dbname
        colName = "col" + dbname
    End Sub

    Public Function getNumber(ByVal instring As String) As Integer

        If instring.ToLower() = "integer" Then
            Return 5
        End If

        Dim numString As String = "0"
        Dim blnGotString As Boolean = False
        For Each c As Char In instring
            If [Char].IsDigit(c) Then
                numString += c
                blnGotString = True
            Else
                ' to not to get the discontinuous numbers eg in float(8,2)
                If blnGotString = True Then
                    Exit For
                End If
            End If
        Next
        Dim realNum As Integer = Integer.Parse(numString)
        Return realNum
    End Function

    'Public Sub getcontrol(ByVal what As String, ByVal parent As String)
    '    Select Case Cntrol
    '        Case "TextBox"
    '            If what = "declare" Then
    '                outText.AppendLine(" private System.Windows.Forms.TextBox " & controlName & "; ")
    '            Else
    '                ' left_s = left_s + 10
    '                top_s = top_s + 30
    '                outText.AppendLine("// ")
    '                outText.AppendLine("//" & controlName)
    '                outText.AppendLine("// ")
    '                outText.AppendLine(" this." & controlName & " = new System.Windows.Forms.TextBox();")
    '                outText.AppendLine(" this." & controlName & " .Location = new System.Drawing.Point(" & left_s.ToString() & ", " & top_s.ToString() & ");")
    '                outText.AppendLine(" this." & controlName & " .Name = " & q & controlName & q & ";")
    '                If type.ToLower().IndexOf("char") > 0 Then
    '                    outText.AppendLine(" this." & controlName & " .MaxLength = " & getNumber(type).ToString() & ";")
    '                End If
    '                outText.AppendLine(" this." & controlName & " .Size = new System.Drawing.Size(160, 20);")
    '                'outText.AppendLine(" this." & controlName & " .Text = " & q & txt & q & ";")
    '                outText.AppendLine(" this." & controlName & " .TabIndex = " & i.ToString() & ";")
    '                If parent = "this" Then
    '                    outText.AppendLine(" this.Controls.Add(this." & controlName & ");")
    '                Else
    '                    outText.AppendLine(" this." & parent & ".Controls.Add(this." & controlName & ");")
    '                End If
    '            End If
    '        Case "ComboBox"
    '            If what = "declare" Then
    '                outText.AppendLine(" private System.Windows.Forms.ComboBox " & controlName & "; ")
    '            Else
    '                ' left_s = left_s + 10
    '                top_s = top_s + 30
    '                outText.AppendLine("// ")
    '                outText.AppendLine("//" & controlName)
    '                outText.AppendLine("// ")
    '                outText.AppendLine(" this." & controlName & " = new System.Windows.Forms.ComboBox();")
    '                outText.AppendLine(" this." & controlName & " .FormattingEnabled = true;")
    '                outText.AppendLine(" this." & controlName & " .Location = new System.Drawing.Point(" & left_s.ToString() & ", " & top_s.ToString() & ");")
    '                outText.AppendLine(" this." & controlName & " .Name = " & q & controlName & q & ";")
    '                outText.AppendLine(" this." & controlName & " .Size = new System.Drawing.Size(160, 20);")
    '                outText.AppendLine(" this." & controlName & " .TabIndex = " & i.ToString() & ";")
    '                If parent = "this" Then
    '                    outText.AppendLine(" this.Controls.Add(this." & controlName & ");")
    '                Else
    '                    outText.AppendLine(" this." & parent & ".Controls.Add(this." & controlName & ");")
    '                End If
    '            End If
    '        Case "Label"
    '            If what = "declare" Then
    '                outText.AppendLine(" private System.Windows.Forms.Label  " & "lbl" & dbname & "; ")
    '            Else
    '                top_s = top_s + 30
    '                outText.AppendLine("// ")
    '                outText.AppendLine("// " & "lbl" & dbname)
    '                outText.AppendLine("// ")
    '                outText.AppendLine("this." & "lbl" & dbname & " = new System.Windows.Forms.Label();")
    '                outText.AppendLine("this." & "lbl" & dbname & ".BackColor = System.Drawing.Color.Transparent;")
    '                outText.AppendLine("this." & "lbl" & dbname & ".Location =new System.Drawing.Point(" & left_s.ToString() & ", " & top_s.ToString() & ");")
    '                outText.AppendLine("this." & "lbl" & dbname & ".Name =" & q & controlName & q & ";")
    '                outText.AppendLine("this." & "lbl" & dbname & ".Size = new System.Drawing.Size(160, 22);")
    '                outText.AppendLine("this." & "lbl" & dbname & ".TabIndex = 0;")
    '                If txt.Trim() = "" Then
    '                    txt = dbname
    '                End If
    '                outText.AppendLine(" this." & controlName & " .Text = " & q & txt & q & ";")
    '                outText.AppendLine("this." & "lbl" & dbname & ".TextAlign = System.Drawing.ContentAlignment.MiddleRight;")
    '                If parent = "this" Then
    '                    outText.AppendLine(" this.Controls.Add(this." & controlName & ");")
    '                Else
    '                    outText.AppendLine(" this." & parent & ".Controls.Add(this." & controlName & ");")
    '                End If
    '            End If
    '        Case "Grid"
    '            If what = "declare" Then
    '                outText.AppendLine(" private System.Windows.Forms.DataGridViewTextBoxColumn  " & colName & "; ")
    '            Else
    '                outText.AppendLine("// ")
    '                outText.AppendLine("// " & colName)
    '                outText.AppendLine("// ")
    '                outText.AppendLine("this." & colName & " = new System.Windows.Forms.DataGridViewTextBoxColumn();")

    '                outText.AppendLine("this." & colName & ".AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;")
    '                If txt.Trim() = "" Then
    '                    txt = dbname
    '                End If
    '                outText.AppendLine(" this." & colName & " .HeaderText = " & q & txt & q & ";")
    '                outText.AppendLine(" this." & colName & " .Tag = " & q & dbname & q & ";")
    '                outText.AppendLine("this." & colName & ".Name = " & q & colName & q & ";")
    '            End If

    '        Case "GroupBox"
    '            If what = "declare" Then
    '                outText.AppendLine(" private System.Windows.Forms.GroupBox " & controlName & "; ")
    '            Else
    '                ' left_s = left_s + 10
    '                top_s = top_s + 30
    '                outText.AppendLine("// ")
    '                outText.AppendLine("//" & controlName)
    '                outText.AppendLine("// ")
    '                outText.AppendLine(" this." & controlName & " = new System.Windows.Forms.GroupBox();")
    '                outText.AppendLine(" this." & controlName & " .Location = new System.Drawing.Point(" & left_s.ToString() & ", " & top_s.ToString() & ");")
    '                outText.AppendLine(" this." & controlName & " .Name = " & q & controlName & q & ";")
    '                outText.AppendLine(" this." & controlName & " .Size = new System.Drawing.Size(160, 20);")
    '                outText.AppendLine(" this." & controlName & " .TabIndex = " & i.ToString() & ";")

    '                outText.AppendLine(" this." & controlName & " .BackColor = System.Drawing.Color.Transparent; ")
    '                outText.AppendLine(" this." & controlName & " .TabStop = false;")
    '                outText.AppendLine(" this." & controlName & " .Text = " & q & txt & q & ";")

    '                If parent = "this" Then
    '                    outText.AppendLine(" this.Controls.Add(this." & controlName & ");")
    '                Else
    '                    outText.AppendLine(" this." & parent & ".Controls.Add(this." & controlName & ");")
    '                End If
    '            End If
    '        Case "RadioButton"
    '            If what = "declare" Then
    '                outText.AppendLine(" private System.Windows.Forms.RadioButton " & controlName & "; ")
    '            Else
    '                ' left_s = left_s + 10
    '                top_s = top_s + 30
    '                outText.AppendLine("// ")
    '                outText.AppendLine("//" & controlName)
    '                outText.AppendLine("// ")
    '                outText.AppendLine(" this." & controlName & " = new System.Windows.Forms.RadioButton();")
    '                outText.AppendLine(" this." & controlName & ".UseVisualStyleBackColor = true;")
    '                outText.AppendLine(" this." & controlName & ".AutoSize = true;")
    '                outText.AppendLine(" this." & controlName & " .Location = new System.Drawing.Point(" & left_s.ToString() & ", " & top_s.ToString() & ");")
    '                outText.AppendLine(" this." & controlName & " .Name = " & q & controlName & q & ";")
    '                outText.AppendLine(" this." & controlName & " .Size = new System.Drawing.Size(160, 20);")
    '                outText.AppendLine(" this." & controlName & " .TabIndex = " & i.ToString() & ";")
    '                If txt.Trim() = "" Then
    '                    txt = dbname
    '                End If
    '                outText.AppendLine(" this." & controlName & " .Text = " & q & txt & q & ";")
    '                If parent = "this" Then
    '                    outText.AppendLine(" this.Controls.Add(this." & controlName & ");")
    '                Else
    '                    outText.AppendLine(" this." & parent & ".Controls.Add(this." & controlName & ");")
    '                End If
    '            End If
    '        Case "CheckBox"
    '            If what = "declare" Then
    '                outText.AppendLine(" private System.Windows.Forms.CheckBox " & controlName & "; ")
    '            Else
    '                ' left_s = left_s + 10
    '                top_s = top_s + 30
    '                outText.AppendLine("// ")
    '                outText.AppendLine("//" & controlName)
    '                outText.AppendLine("// ")
    '                outText.AppendLine(" this." & controlName & " = new System.Windows.Forms.CheckBox();")
    '                outText.AppendLine(" this." & controlName & ".UseVisualStyleBackColor = true;")
    '                outText.AppendLine(" this." & controlName & ".AutoSize = true;")
    '                outText.AppendLine(" this." & controlName & " .Location = new System.Drawing.Point(" & left_s.ToString() & ", " & top_s.ToString() & ");")
    '                outText.AppendLine(" this." & controlName & " .Name = " & q & controlName & q & ";")
    '                outText.AppendLine(" this." & controlName & " .Size = new System.Drawing.Size(160, 20);")
    '                If txt.Trim() = "" Then
    '                    txt = dbname
    '                End If
    '                outText.AppendLine(" this." & controlName & " .Text = " & q & txt & q & ";")
    '                outText.AppendLine(" this." & controlName & " .TabIndex = " & i.ToString() & ";")
    '                If parent = "this" Then
    '                    outText.AppendLine(" this.Controls.Add(this." & controlName & ");")
    '                Else
    '                    outText.AppendLine(" this." & parent & ".Controls.Add(this." & controlName & ");")
    '                End If
    '            End If
    '        Case "VtextBox"
    '            If what = "declare" Then
    '                outText.AppendLine(" private Bsoft.Controls.VtextBox " & controlName & "; ")
    '            Else
    '                ' left_s = left_s + 10
    '                top_s = top_s + 30
    '                outText.AppendLine("// ")
    '                outText.AppendLine("//" & controlName)
    '                outText.AppendLine("// ")
    '                outText.AppendLine(" this." & controlName & " = new Bsoft.Controls.VtextBox();")
    '                outText.AppendLine(" this." & controlName & " .Location = new System.Drawing.Point(" & left_s.ToString() & ", " & top_s.ToString() & ");")
    '                outText.AppendLine(" this." & controlName & " .Name = " & q & controlName & q & ";")
    '                outText.AppendLine(" this." & controlName & " .Size = new System.Drawing.Size(160, 20);")
    '                If type.ToLower() = "Integer".ToLower() Then
    '                    outText.AppendLine(" this." & controlName & " .ValidationType = Bsoft.Controls.VtextBox.ValidationTypeEnum.Integer;")
    '                Else
    '                    outText.AppendLine(" this." & controlName & " .ValidationType = Bsoft.Controls.VtextBox.ValidationTypeEnum.No_Validation;")
    '                End If


    '                outText.AppendLine(" this." & controlName & " .Value = " & q & q & ";")
    '                outText.AppendLine(" this." & controlName & " .TabIndex = " & i.ToString() & ";")
    '                If parent = "this" Then
    '                    outText.AppendLine(" this.Controls.Add(this." & controlName & ");")
    '                Else
    '                    outText.AppendLine(" this." & parent & ".Controls.Add(this." & controlName & ");")
    '                End If
    '            End If
    '        Case "DateTimePicker"
    '            If what = "declare" Then
    '                outText.AppendLine(" System.Windows.Forms.DateTimePicker " & controlName & "; ")
    '            Else
    '                ' left_s = left_s + 10
    '                top_s = top_s + 30
    '                outText.AppendLine("// ")
    '                outText.AppendLine("//" & controlName)
    '                outText.AppendLine("// ")
    '                outText.AppendLine(" this." & controlName & " = new System.Windows.Forms.DateTimePicker();")
    '                outText.AppendLine(" this." & controlName & " .Location = new System.Drawing.Point(" & left_s.ToString() & ", " & top_s.ToString() & ");")
    '                outText.AppendLine(" this." & controlName & " .Name = " & q & controlName & q & ";")
    '                outText.AppendLine(" this." & controlName & " .Size = new System.Drawing.Size(110, 20);")
    '                outText.AppendLine(" this." & controlName & ".CustomFormat = " & q & "dd/MMM/yyyy" & q & ";")
    '                outText.AppendLine(" this." & controlName & ".Format = System.Windows.Forms.DateTimePickerFormat.Custom;")
    '                outText.AppendLine(" this." & controlName & " .TabIndex = " & i.ToString() & ";")
    '                If parent = "this" Then
    '                    outText.AppendLine(" this.Controls.Add(this." & controlName & ");")
    '                Else
    '                    outText.AppendLine(" this." & parent & ".Controls.Add(this." & controlName & ");")
    '                End If
    '            End If
    '    End Select
    'End Sub

    Public Function getCrudeType(ByVal Type As String) As String
        If (InStr(Type, "varchar", CompareMethod.Text) > 0) Then
            Type = "ColumnTypes.String"
        End If
        If (InStr(Type, "int", CompareMethod.Text) > 0) Then
            Type = "ColumnTypes.Number"
        End If
        If (InStr(Type, "number", CompareMethod.Text) > 0) Then
            Type = "ColumnTypes.Number"
        End If
        If (InStr(Type, "char", CompareMethod.Text) > 0) Then
            Type = "ColumnTypes.String"
        End If
        If (InStr(Type, "date", CompareMethod.Text) > 0) Then
            Type = "ColumnTypes.String"
        End If
        If (InStr(Type, "float", CompareMethod.Text) > 0) Then
            Type = "ColumnTypes.Number"
        End If

        getCrudeType = Type
    End Function


#End Region

#Region "Functions"
    Public Function isNumberType() As Boolean
        If (InStr(type, "varchar", CompareMethod.Text) > 0) Then
            Return False
        End If
        If (InStr(type, "int", CompareMethod.Text) > 0) Then
            Return True
        End If
        If (InStr(type, "number", CompareMethod.Text) > 0) Then
            Return True
        End If
        If (InStr(type, "char", CompareMethod.Text) > 0) Then
            Return False
        End If
        If (InStr(type, "date", CompareMethod.Text) > 0) Then
            Return False
        End If
        Return False
    End Function
#End Region

End Class