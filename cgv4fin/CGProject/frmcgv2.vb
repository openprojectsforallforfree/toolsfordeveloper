Imports Microsoft.Office.Core
Imports Microsoft.Office.Interop.Excel
Imports System.Text
Public Class frmcgv2
#Region "declaration"
    Const q = """"
    ' " & q & "
    Dim tablename As String
    Dim left_s, top_s As Integer
    Dim i As Integer
    Dim txtout As String
    Dim dbname, type, constraint, txt, control As String
    Dim controlName, CrudeType, colName As String
    Dim forceControl As String
#End Region
   
    ''' <summary>
    ''' Db Listing Entry Gridrange dataloader
    ''' </summary>
    ''' <param name="what"></param>
    ''' <remarks></remarks>
    Private Sub GenCreate_Beforelines(ByVal what As String)

        If what = "db" Then
            addline("if (!_dbStruct.DoesTableExists( " & q & tablename & q & ")){")
            addline("SQLCreate.Remove(0, SQLCreate.Length);")
            addline("SQLCreate.Append( " & q & " CREATE TABLE " & tablename & "( " & q & ");")
        End If
        If what = "listing" Then
            addline("TableName = " & q & tablename & q & ";")
        End If

        If what = "entry" Then
            addline("TableName = " & q & tablename & q & ";")
        End If


        If what = "gridaddrange" Then
            addline("this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {")
        End If
        If what = "dataloader" Then
            addline("DataTable dt = new DataTable();")
            addline("StringBuilder SQL = new StringBuilder();")
            addline("SQL.Append(" & q & "select * from " & tablename & q & ");")
            addline("dt = BLL.GlobalResources.SelectDBConnection.ExecuteDataTable(SQL.ToString());")
            addline("   if (dt.Rows.Count > 0)  {")


        End If


    End Sub

    ''' <summary>
    ''' DB Listing Entry
    ''' </summary>
    ''' <param name="what"></param>
    ''' <remarks></remarks>
    Private Sub GenCreate_loop_Fistline(ByVal what As String)

        Dim a As New StringBuilder
        If what = "db" Then

            If constraint.ToLower = "pk" Then
                a.AppendFormat("SQLCreate.Append( " & q & " \n {0} {1} primary key identity(1,1) " & q & ");", dbname, type)

            Else
                a.AppendFormat("SQLCreate.Append( " & q & " \n {0} {1} " & q & ");", dbname, type)
            End If
        ElseIf what = "listing" Then
            a.AppendFormat("DataTableColumns.Add( " & q & "{0}" & q & ",{1}," & q & "{2}" & q & ",true ,true);", dbname, CrudeType, colName)
        ElseIf what = "entry" Then
            a.AppendFormat("ColumnList.AddNewEditColumn(" & q & "{0}" & q & ",{1}, {2} ,true ,false );", dbname, CrudeType, controlName)
        Else
            GenCreate_loop(what)
        End If
        addline(a.ToString())
    End Sub

    ''' <summary>
    ''' All
    ''' </summary>
    ''' <param name="what"></param>
    ''' <remarks></remarks>
    Private Sub GenCreate_loop(ByVal what As String)
        If constraint Is Nothing Then
            constraint = "********************"
        End If
        Dim a As New StringBuilder
        If what = "db" Then
            If constraint.ToLower = "pk" Then
                a.AppendFormat("SQLCreate.Append( " & q & " \n, {0} {1} primary key identity(1,1) " & q & ");", dbname, type, constraint, control)
            Else
                If InStr(dbname, "fk") > 0 Then
                    a.AppendFormat("SQLCreate.Append( " & q & " \n, {0} {1} FOREIGN KEY REFERENCES {2} (id) " & q & ");", dbname, type, constraint, control)
                Else
                    a.AppendFormat("SQLCreate.Append( " & q & " \n, {0} {1} " & q & ");", dbname, type, constraint, control)
                End If

            End If

        End If

        If what = "listing" Then

            a.AppendFormat("DataTableColumns.Add( " & q & "{0}" & q & ",{1}," & q & "{2}" & q & ");", dbname, CrudeType, colName)
        End If

        If what = "entry" Then

            a.AppendFormat("ColumnList.AddNewEditColumn(" & q & "{0}" & q & ",{1}, {2} );", dbname, CrudeType, controlName)
        End If
        If what = "control" Or what = "declare" Then

            getcontrol(what)
        End If
        If what = "Grid" Then
            control = "Grid"
            getcontrol(what)
        End If

        If what = "griddeclare" Then
            control = "Grid"
            getcontrol("declare")
        End If

        If what = "gridaddrange" Then
            addline("this." & "col" & dbname & ",")
        End If

        If what = "dataloader" Then
            addline("  lbl" & dbname & ".Text= dt.Rows[0][" & q & dbname & q & "].ToString();")
        End If



        addline(a.ToString())
    End Sub

    Private Sub getcontrol(ByVal what As String)

        Select Case control
            Case "TextBox"
                If what = "declare" Then
                    addline(" private System.Windows.Forms.TextBox " & controlName & "; ")
                Else
                    ' left_s = left_s + 10
                    top_s = top_s + 30
                    addline("// ")
                    addline("//" & controlName)
                    addline("// ")
                    addline(" this." & controlName & " = new System.Windows.Forms.TextBox();")
                    addline(" this." & controlName & " .Location = new System.Drawing.Point(" & left_s.ToString() & ", " & top_s.ToString() & ");")
                    addline(" this." & controlName & " .Name = " & q & controlName & q & ";")
                    addline(" this." & controlName & " .Size = new System.Drawing.Size(160, 20);")
                    addline(" this." & controlName & " .Text = " & q & txt & q & ";")
                    addline(" this." & controlName & " .TabIndex = " & i.ToString() & ";")
                    If txtParent.Text = "this" Then
                        addline(" this.Controls.Add(this." & controlName & ");")
                    Else
                        addline(" this." & txtParent.Text & ".Controls.Add(this." & controlName & ");")
                    End If
                End If

            Case "ComboBox"
                If what = "declare" Then
                    addline(" private System.Windows.Forms.ComboBox " & controlName & "; ")
                Else
                    ' left_s = left_s + 10
                    top_s = top_s + 30
                    addline("// ")
                    addline("//" & controlName)
                    addline("// ")




                    addline(" this." & controlName & " = new System.Windows.Forms.ComboBox();")

                    addline(" this." & controlName & " .FormattingEnabled = true;")
                    addline(" this." & controlName & " .Location = new System.Drawing.Point(" & left_s.ToString() & ", " & top_s.ToString() & ");")
                    addline(" this." & controlName & " .Name = " & q & controlName & q & ";")
                    addline(" this." & controlName & " .Size = new System.Drawing.Size(160, 20);")
                    addline(" this." & controlName & " .TabIndex = " & i.ToString() & ";")
                    If txtParent.Text = "this" Then
                        addline(" this.Controls.Add(this." & controlName & ");")
                    Else
                        addline(" this." & txtParent.Text & ".Controls.Add(this." & controlName & ");")
                    End If
                End If
            Case "Label"
                If what = "declare" Then
                    addline(" private System.Windows.Forms.Label  " & "lbl" & dbname & "; ")
                Else
                    top_s = top_s + 30
                    addline("// ")
                    addline("// " & "lbl" & dbname)
                    addline("// ")
                    addline("this." & "lbl" & dbname & " = new System.Windows.Forms.Label();")
                    addline("this." & "lbl" & dbname & ".BackColor = System.Drawing.Color.Transparent;")
                    addline("this." & "lbl" & dbname & ".Location =new System.Drawing.Point(" & left_s.ToString() & ", " & top_s.ToString() & ");")
                    addline("this." & "lbl" & dbname & ".Name =" & q & controlName & q & ";")
                    addline("this." & "lbl" & dbname & ".Size = new System.Drawing.Size(160, 22);")
                    addline("this." & "lbl" & dbname & ".TabIndex = 0;")
                    addline(" this." & controlName & " .Text = " & q & txt & q & ";")
                    addline("this." & "lbl" & dbname & ".TextAlign = System.Drawing.ContentAlignment.MiddleRight;")
                    If txtParent.Text = "this" Then
                        addline(" this.Controls.Add(this." & controlName & ");")
                    Else
                        addline(" this." & txtParent.Text & ".Controls.Add(this." & controlName & ");")
                    End If
                End If
            Case "Grid"
                If what = "declare" Then
                    addline(" private System.Windows.Forms.DataGridViewTextBoxColumn  " & colName & "; ")
                Else
                    addline("// ")
                    addline("// " & colName)
                    addline("// ")
                    addline("this." & colName & " = new System.Windows.Forms.DataGridViewTextBoxColumn();")

                    addline("this." & colName & ".AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;")
                    addline(" this." & colName & " .HeaderText = " & q & txt & q & ";")
                    addline(" this." & colName & " .Tag = " & q & dbname & q & ";")
                    addline("this." & colName & ".Name = " & q & colName & q & ";")
                End If

            Case "GroupBox"
                If what = "declare" Then
                    addline(" private System.Windows.Forms.GroupBox " & controlName & "; ")
                Else
                    ' left_s = left_s + 10
                    top_s = top_s + 30
                    addline("// ")
                    addline("//" & controlName)
                    addline("// ")
                    addline(" this." & controlName & " = new System.Windows.Forms.GroupBox();")
                    addline(" this." & controlName & " .Location = new System.Drawing.Point(" & left_s.ToString() & ", " & top_s.ToString() & ");")
                    addline(" this." & controlName & " .Name = " & q & controlName & q & ";")
                    addline(" this." & controlName & " .Size = new System.Drawing.Size(160, 20);")
                    addline(" this." & controlName & " .TabIndex = " & i.ToString() & ";")

                    addline(" this." & controlName & " .BackColor = System.Drawing.Color.Transparent; ")
                    addline(" this." & controlName & " .TabStop = false;")
                    addline(" this." & controlName & " .Text = " & q & txt & q & ";")

                    If txtParent.Text = "this" Then
                        addline(" this.Controls.Add(this." & controlName & ");")
                    Else
                        addline(" this." & txtParent.Text & ".Controls.Add(this." & controlName & ");")
                    End If
                End If
            Case "RadioButton"
                If what = "declare" Then
                    addline(" private System.Windows.Forms.RadioButton " & controlName & "; ")
                Else
                    ' left_s = left_s + 10
                    top_s = top_s + 30
                    addline("// ")
                    addline("//" & controlName)
                    addline("// ")
                    addline(" this." & controlName & " = new System.Windows.Forms.RadioButton();")
                    addline(" this." & controlName & ".UseVisualStyleBackColor = true;")
                    addline(" this." & controlName & ".AutoSize = true;")
                    addline(" this." & controlName & " .Location = new System.Drawing.Point(" & left_s.ToString() & ", " & top_s.ToString() & ");")
                    addline(" this." & controlName & " .Name = " & q & controlName & q & ";")
                    addline(" this." & controlName & " .Size = new System.Drawing.Size(160, 20);")
                    addline(" this." & controlName & " .TabIndex = " & i.ToString() & ";")
                    addline(" this." & controlName & " .Text = " & q & txt & q & ";")
                    If txtParent.Text = "this" Then
                        addline(" this.Controls.Add(this." & controlName & ");")
                    Else
                        addline(" this." & txtParent.Text & ".Controls.Add(this." & controlName & ");")
                    End If
                End If
            Case "CheckBox"
                If what = "declare" Then
                    addline(" private System.Windows.Forms.CheckBox " & controlName & "; ")
                Else
                    ' left_s = left_s + 10
                    top_s = top_s + 30
                    addline("// ")
                    addline("//" & controlName)
                    addline("// ")
                    addline(" this." & controlName & " = new System.Windows.Forms.CheckBox();")
                    addline(" this." & controlName & ".UseVisualStyleBackColor = true;")
                    addline(" this." & controlName & ".AutoSize = true;")
                    addline(" this." & controlName & " .Location = new System.Drawing.Point(" & left_s.ToString() & ", " & top_s.ToString() & ");")
                    addline(" this." & controlName & " .Name = " & q & controlName & q & ";")
                    addline(" this." & controlName & " .Size = new System.Drawing.Size(160, 20);")
                    addline(" this." & controlName & " .Text = " & q & txt & q & ";")
                    addline(" this." & controlName & " .TabIndex = " & i.ToString() & ";")
                    If txtParent.Text = "this" Then
                        addline(" this.Controls.Add(this." & controlName & ");")
                    Else
                        addline(" this." & txtParent.Text & ".Controls.Add(this." & controlName & ");")
                    End If
                End If






        End Select




    End Sub

    ''' <summary>
    ''' range db
    ''' </summary>
    ''' <param name="what"></param>
    ''' <remarks></remarks>
    Private Sub GenCreate_loop_Lastline(ByVal what As String)

        Dim a As New StringBuilder
        If what = "db" Then
            a.AppendFormat("SQLCreate.Append( " & q & " \n, {0} {1} )" & q & ");", dbname, type, constraint, control)
        ElseIf what = "gridaddrange" Then
            addline("this." & "col" & dbname & "});")
        Else
            GenCreate_loop(what)
        End If
        addline(a.ToString())
    End Sub

    ''' <summary>
    ''' Dataloader Db
    ''' </summary>
    ''' <param name="what"></param>
    ''' <remarks></remarks>
    Private Sub GenCreate_Afterlines(ByVal what As String)

        If what = "db" Then
            addline("Rslt = _dbStruct.Con.ExecuteNonQuery(SQLCreate.ToString());")
            addline("LogTrace.WriteInfoLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, " & q & tablename & " table created Successfully. " & q & ");")
            addline("Status = true;}")
        End If
        If what = "dataloader" Then
            addline("  }")
        End If

    End Sub

#Region "General Functions"
    Private Sub addline(ByVal line As String)

        txtout = txtout & vbCr & Line
    End Sub

    Private Function getCrudeType(ByVal Type As String) As String
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
        getCrudeType = Type
    End Function

    Private Sub checkallvalue()
        'If dbname = "" Then
        '    dbname = "*******"
        'End If
        'If type = "" Then
        '    type = "*******"
        'End If
        'If constraint = "" Then
        '    constraint = "*******"
        'End If
        'If txt = "" Then
        '    txt = "*******"
        'End If
        'If control = "" Then
        '    control = "*******"
        'End If
        If forceControl <> "NONE" Then
            control = forceControl
        End If
        Select Case control

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
            Case Else
                controlName = "oth" + dbname
        End Select



        CrudeType = getCrudeType(type)
        colName = "col" + dbname
    End Sub


    Private Sub GenCreate(ByVal what As String)
        lblStatus.Text = "Started " + what
        left_s = 0
        top_s = 0


        txtout = ""

        Dim selection_area As Integer
        Dim selection_column As Integer
        Dim selection_row As Integer
        Dim range As Object
        Dim DATA() As String


        constraint = "*****************"


        Dim Excel As Microsoft.Office.Interop.Excel.Application
        Try

            Excel = GetObject(, "Excel.Application")
        Catch ex As Exception
            MsgBox("Please Open Excel Application" & vbCrLf & ex.Message)
            Return
        End Try

        Dim workbook As Microsoft.Office.Interop.Excel.Workbook = Excel.ActiveWorkbook
        Dim Worksheet As Microsoft.Office.Interop.Excel.Worksheet = Excel.ActiveSheet
        If workbook IsNot Nothing Then
            If Worksheet IsNot Nothing Then
                selection_area = Excel.Selection.Areas.Count
                selection_column = Excel.Selection.Columns.Count
                selection_row = Excel.Selection.Rows.Count

                If selection_area = 1 Then
                    If selection_column = 5 Then
                        ReDim DATA(selection_row - 1)
                        range = Excel.Selection.cells.value

                        tablename = range(1, 1)
                        GenCreate_Beforelines(what)

                        For i = 1 To selection_row - 1

                            dbname = range(i + 1, 1)
                            type = range(i + 1, 2)
                            constraint = range(i + 1, 3)
                            txt = range(i + 1, 4)
                            control = range(i + 1, 5)
                            checkallvalue()
                            'If (selection_column = 4) Then
                            '    gridColname = range(i + 1, 4)
                            'End If
                            'If constraint Is Nothing Then
                            '    constraint = "Noooothingssss"
                            'End If
                            If i = 1 Then
                                GenCreate_loop_Fistline(what)
                            ElseIf i = selection_row - 1 Then
                                GenCreate_loop_Lastline(what)
                            Else
                                GenCreate_loop(what)
                            End If

                        Next
                        GenCreate_Afterlines(what)
                    Else
                        MsgBox("Please Select DbName,Type,Constraint (At least 3 columns)", MsgBoxStyle.Information)
                    End If
                Else
                    MsgBox("Please Select only single Area. ")
                End If
            Else
                MsgBox("Work Sheet not found.")
            End If
        Else
            MsgBox("Please Open Excel Application.")
        End If


        lblStatus.Text = "Finished " + what
    End Sub
#End Region
   

#Region "events"
    Private Sub frmcgv2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        constraint = ""
        
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        forceControl = "NONE"
        Dim declr, ctrl As String
        GenCreate("declare")
        declr = txtout

        GenCreate("control")
        ctrl = txtout
        txtout = ""
        txtout = ctrl + vbCrLf + "}" + declr
        Clipboard.SetText(txtout)
    End Sub

    Private Sub btnGen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGen.Click
        GenCreate("control")
        Clipboard.SetText(txtout)
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        GenCreate("declare")
        Clipboard.SetText(txtout)
    End Sub
    Private Sub cs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cs.Click

        GenCreate("db")
        Clipboard.SetText(txtout)

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        GenCreate("listing")
        'listing()
        Clipboard.SetText(txtout)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        GenCreate("entry")
        'entry()
        Clipboard.SetText(txtout)
    End Sub


#End Region
    
     
   

    
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        forceControl = "NONE"
        Dim grid, gd, gr As String
        GenCreate("Grid")
        grid = txtout
        GenCreate("griddeclare")
        gd = txtout
        GenCreate("gridaddrange")
        gr = txtout
        txtout = ""
        txtout = grid + gr + "}" + gd
        Clipboard.SetText(txtout)
    End Sub

    Private Sub ForceContrl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click, TextBox1.Click, RadioButton1.Click, GroupBox1.Enter, CheckBox1.Click, Button6.Click
        forceControl = sender.GetType.Name.ToString()

        Dim declr, ctrl As String
        GenCreate("declare")
        declr = txtout

        GenCreate("control")
        ctrl = txtout
        txtout = ""
        txtout = ctrl + vbCrLf + "}" + declr
        Clipboard.SetText(txtout)
    End Sub

 
  

   
    Private Sub ForceContrl_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        forceControl = sender.GetType.Name.ToString()
        Dim grid, gd, gr As String
        GenCreate("Grid")
        grid = txtout
        GenCreate("griddeclare")
        gd = txtout
        GenCreate("gridaddrange")
        gr = txtout
        txtout = ""
        txtout = grid + gr + "}" + gd
        Clipboard.SetText(txtout)
    End Sub

    

    Private Sub btnSQL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSQL.Click
        ''forceControl = sender.GetType.Name.ToString()

        Dim declr, ctrl As String
        GenCreate("declare")
        declr = txtout

        GenCreate("control")
        ctrl = txtout
        txtout = ""
        txtout = ctrl + vbCrLf + "}" + declr
        Clipboard.SetText(txtout)
    End Sub
End Class