Imports Microsoft.Office.Core
Imports Microsoft.Office.Interop.Excel
Imports System.Text

'CREATE TABLE Persons
'(
'    PersonID int identity(1,1) NOT NULL,
'    FirstName nvarchar(20),
'    LastName nvarchar(20) NOT NULL,
'    CONSTRAINT PrimKeyPeople PRIMARY KEY(PersonID)
');

''todo
'taborder +
'labels +
'vertical listing +
'grid 
'pk entry and listing

Public Class frmEnja

#Region "Declarations and small functions"


    Const q = """"
    ' " & q & "
    Dim tablename As String
    Dim left_s, top_s As Integer
    Dim i As Integer
    Private Sub test()
        Dim pathProject As String
        pathProject = "C:\Users\dhirajb\Desktop\Enja\projects.xls"
        Dim a As New excelhelper(pathProject, 1)
        Dim b As String
        b = a.read("A1")
        Dim VALUES As New System.Collections.Generic.SortedList(Of String, String)
        VALUES.Add("A11", "D")
        VALUES.Add("B11", "DH")
        VALUES.Add("C11", "DHI")
        VALUES.Add("D11", "DHIR")
        VALUES.Add("E11", "DHIRA")
        VALUES.Add("F11", "DHIRAJ")
        a.WriteexcelRange(VALUES)

        VALUES = a.readExcelRange(VALUES)
        a.close()
    End Sub



    Private Sub addline(ByVal line As String)
        txtOut.Text = txtOut.Text & vbCr & line
    End Sub
#End Region

    Private Sub GenCreate_Beforelines(ByVal what As String)
        If what = "db" Then
            addline("if (!_dbStruct.DoesTableExists( " & q & tablename & q & ")){")
            addline("SQLCreate.Remove(0, SQLCreate.Length);")
            addline("SQLCreate.Append( " & q & " CREATE TABLE " & tablename & "( " & q & ");")
        End If
        If what = "gridaddrange" Then
            addline("this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {")
        End If
        If what = "listing" Then
            addline("TableName = " & q & tablename & q & ";")
        End If

        If what = "entry" Then
            addline("TableName = " & q & tablename & q & ";")
        End If

        If what = "dataloader" Then
            addline("DataTable dt = new DataTable();")
            addline("StringBuilder SQL = new StringBuilder();")
            addline("SQL.Append(" & q & "select * from " & tablename & q & ");")
            addline("dt = BLL.GlobalResources.SelectDBConnection.ExecuteDataTable(SQL.ToString());")
            addline("   if (dt.Rows.Count > 0)  {")


        End If

         
    End Sub

    Private Sub GenCreate_loop_Fistline(ByVal what As String, ByVal dbname As String, ByVal type As String, ByVal constraint As String, ByVal control As String, ByVal col As String, ByVal gridColName As String)
        Dim a As New StringBuilder
        If what = "db" Then

            If constraint.ToLower = "pk" Then
                a.AppendFormat("SQLCreate.Append( " & q & " \n {0} {1} primary key identity(1,1) " & q & ");", dbname, type, constraint, control, col)

            Else
                a.AppendFormat("SQLCreate.Append( " & q & " \n {0} {1} " & q & ");", dbname, type, constraint, control, col)
            End If
        ElseIf what = "listing" Then

            a.AppendFormat("DataTableColumns.Add( " & q & "{0}" & q & ",{1}," & q & "{4}" & q & ",true ,true);", dbname, getCrudeType(type), constraint, control, col)


        ElseIf what = "entry" Then
            type = getCrudeType(type)
            a.AppendFormat("ColumnList.AddNewEditColumn(" & q & "{0}" & q & ",{1}, {3} ,true ,false );", dbname, type, constraint, control, col)



        Else
            GenCreate_loop(what, dbname, type, constraint, control, col, gridColName)
        End If
        addline(a.ToString())
    End Sub

    Private Sub GenCreate_loop(ByVal what As String, ByVal dbname As String, ByVal type As String, ByVal constraint As String, ByVal control As String, ByVal col As String, ByVal gridColname As String)
        Dim a As New StringBuilder
        If what = "db" Then
            If constraint.ToLower = "pk" Then
                a.AppendFormat("SQLCreate.Append( " & q & " \n, {0} {1} primary key identity(1,1) " & q & ");", dbname, type, constraint, control, col)
            Else
                If InStr(dbname, "fk") > 0 Then
                    a.AppendFormat("SQLCreate.Append( " & q & " \n, {0} {1} FOREIGN KEY REFERENCES {2} (id) " & q & ");", dbname, type, constraint, control, col)
                Else
                    a.AppendFormat("SQLCreate.Append( " & q & " \n, {0} {1} " & q & ");", dbname, type, constraint, control, col)
                End If

            End If

        End If

        If what = "listing" Then

            a.AppendFormat("DataTableColumns.Add( " & q & "{0}" & q & ",{1}," & q & "{4}" & q & ");", dbname, getCrudeType(type), constraint, control, col)
        End If

        If what = "entry" Then
            type = getCrudeType(type)
            a.AppendFormat("ColumnList.AddNewEditColumn(" & q & "{0}" & q & ",{1}, {3} );", dbname, type, constraint, control, col)
        End If

        If what = "textbox" Then
            ' left_s = left_s + 10
            top_s = top_s + 30
            addline("// ")
            addline("//" & control)
            addline("// ")
            addline(" this." & control & " = new System.Windows.Forms.TextBox();")
            addline(" this." & control & " .Location = new System.Drawing.Point(" & left_s.ToString() & ", " & top_s.ToString() & ");")
            addline(" this." & control & " .Name = " & q & " & control & " & q & ";")
            addline(" this." & control & " .Size = new System.Drawing.Size(160, 20);")
            addline(" this." & control & " .TabIndex = " & i.ToString() & ";")
            addline(" this.Controls.Add(this." & control & ");")
        End If

        If what = "control" Then
            ' left_s = left_s + 10
            top_s = top_s + 30
            addline("// ")
            addline("//" & control)
            addline("// ")


           
            If control.EndsWith("id", StringComparison.OrdinalIgnoreCase) Then
                addline(" this." & control & " = new System.Windows.Forms.ComboBox();")
            Else
                addline(" this." & control & " = new System.Windows.Forms.TextBox();")
            End If

            addline(" this." & control & " .Location = new System.Drawing.Point(" & left_s.ToString() & ", " & top_s.ToString() & ");")
            addline(" this." & control & " .Name = " & q & " & control & " & q & ";")
            addline(" this." & control & " .Size = new System.Drawing.Size(160, 20);")
            addline(" this." & control & " .TabIndex = " & i.ToString() & ";")
            addline(" this.Controls.Add(this." & control & ");")

        End If

        If what = "declarecontrol" Then
            If control.EndsWith("id", StringComparison.OrdinalIgnoreCase) Then
                addline(" private System.Windows.Forms.ComboBox " & control & "; ")
            Else
                addline(" private System.Windows.Forms.TextBox " & control & "; ")
            End If

        End If

        If what = "declare" Then
            addline(" private System.Windows.Forms.TextBox " & control & "; ")
        End If

        If what = "label" Then
            dbname = dbname & txtSuffix.Text
            top_s = top_s + 30
            addline("// ")
            addline("// " & "lbl" & dbname)
            addline("// ")
            addline("this." & "lbl" & dbname & " = new System.Windows.Forms.Label();")
            addline("this." & "lbl" & dbname & ".BackColor = System.Drawing.Color.Transparent;")
            addline("this." & "lbl" & dbname & ".Location =new System.Drawing.Point(" & left_s.ToString() & ", " & top_s.ToString() & ");")
            addline("this." & "lbl" & dbname & ".Name =" & q & "lbl" & dbname & q & " ;")
            addline("this." & "lbl" & dbname & ".Size = new System.Drawing.Size(160, 22);")
            addline("this." & "lbl" & dbname & ".TabIndex = 0;")
            addline("this." & "lbl" & dbname & ".Text =" & q & dbname & ":" & q & ";")
            addline("this." & "lbl" & dbname & ".TextAlign = System.Drawing.ContentAlignment.MiddleRight;")
            addline(" this.Controls.Add(this." & "lbl" & dbname & ");")
        End If

        If what = "declarelabel" Then
            addline(" private System.Windows.Forms.Label  " & "lbl" & dbname & "; ")
        End If

        If what = "grid" Then
            addline("this." & "col" & dbname & " = new System.Windows.Forms.DataGridViewTextBoxColumn();")
            addline("// ")
            addline("// " & "col" & dbname)
            addline("// ")
            addline("this." & "col" & dbname & ".AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;")
            addline("this." & "col" & dbname & ".HeaderText = " & q & dbname & q & ";")
            addline("this." & "col" & dbname & ".Name = " & q & "col" & dbname & q & ";")
        End If

        If what = "grid2" Then
            addline("this." & "col" & dbname & " = new System.Windows.Forms.DataGridViewTextBoxColumn();")
            addline("// ")
            addline("// " & "col" & dbname)
            addline("// ")
            addline("this." & "col" & dbname & ".AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;")
            addline("this." & "col" & dbname & ".HeaderText = " & q & gridColname & q & ";")
            addline("this." & "col" & dbname & ".Name = " & q & "col" & dbname & q & ";")
        End If

        If what = "griddeclare" Then
            'private System.Windows.Forms.DataGridViewTextBoxColumn colDhiraj;
            addline(" private System.Windows.Forms.DataGridViewTextBoxColumn  " & "col" & dbname & "; ")
        End If

        If what = "gridaddrange" Then
            addline("this." & "col" & dbname & ",")
        End If


        If what = "dataloader" Then

            addline("  lbl" & dbname & ".Text= dt.Rows[0][" & q & dbname & q & "].ToString();")

        End If
        addline(a.ToString())
    End Sub

    Private Sub GenCreate_loop_Lastline(ByVal what As String, ByVal dbname As String, ByVal type As String, ByVal constraint As String, ByVal control As String, ByVal col As String, ByVal gridColName As String)
        Dim a As New StringBuilder
        If what = "db" Then
            a.AppendFormat("SQLCreate.Append( " & q & " \n, {0} {1} )" & q & ");", dbname, type, constraint, control, col)

        ElseIf what = "gridaddrange" Then
            addline("this." & "col" & dbname & "});")
        Else
            GenCreate_loop(what, dbname, type, constraint, control, col, gridColName)
        End If
        addline(a.ToString())
    End Sub

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


    Private Sub GenCreate(ByVal what As String)

        txtOut.Text = ""

        Dim selection_area As Integer
        Dim selection_column As Integer
        Dim selection_row As Integer
        Dim range As Object
        Dim DATA() As String

        Dim dbname, type, constraint, control, col, gridColname As String
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
                    If selection_column >= 3 Then
                        ReDim DATA(selection_row - 1)
                        range = Excel.Selection.cells.value

                        tablename = range(1, 1)
                        GenCreate_Beforelines(what)

                        For i = 1 To selection_row - 1
                            'DATA(i) = range(i + 1, 1) & "," & range(i + 1, 2) & "," & range(i + 1, 3) & "," & range(i + 1, 4) & "," & range(i + 1, 5)
                            dbname = range(i + 1, 1)
                            type = range(i + 1, 2)
                            constraint = range(i + 1, 3)
                            control = "txt" & dbname
                            col = "col" & dbname
                            If (selection_column = 4) Then
                                gridColname = range(i + 1, 4)
                            End If
                            If constraint Is Nothing Then
                                constraint = "Noooothingssss"
                            End If
                            If i = 1 Then
                                GenCreate_loop_Fistline(what, dbname, type, constraint, control, col, gridColname)
                            ElseIf i = selection_row - 1 Then
                                GenCreate_loop_Lastline(what, dbname, type, constraint, control, col, gridColname)
                            Else
                                GenCreate_loop(what, dbname, type, constraint, control, col, gridColname)
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
    End Sub


#Region "Buttons"
    Private Sub cs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cs.Click
        'dbstructure()
        GenCreate("db")
        Clipboard.SetText(txtOut.Text)
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        GenCreate("listing")
        'listing()
        Clipboard.SetText(txtOut.Text)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        GenCreate("entry")
        'entry()
        Clipboard.SetText(txtOut.Text)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        top_s = 0
        left_s = 300
        GenCreate("textbox")
        Clipboard.SetText(txtOut.Text)
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

        GenCreate("declare")
        Clipboard.SetText(txtOut.Text)
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        top_s = 0
        left_s = 120
        GenCreate("label")
        Clipboard.SetText(txtOut.Text)
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        GenCreate("declarelabel")
        Clipboard.SetText(txtOut.Text)

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        GenCreate("grid")
        Clipboard.SetText(txtOut.Text)
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        GenCreate("griddeclare")
        Clipboard.SetText(txtOut.Text)
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        GenCreate("gridaddrange")
        Clipboard.SetText(txtOut.Text)
    End Sub
#End Region



    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        GenCreate("dataloader")
        Clipboard.SetText(txtOut.Text)
    End Sub

    Private Sub cmdGridWithHeader_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGridWithHeader.Click

        GenCreate("grid2")
        Clipboard.SetText(txtOut.Text)
        
    End Sub

    
    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        'generate with intelligence, ending with id or starting with fk will be cboBox
        top_s = 0
        left_s = 300
        GenCreate("control")
        Clipboard.SetText(txtOut.Text)
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'generate with intelligence, ending with id or starting with fk will be cboBox
        top_s = 0
        left_s = 300
        GenCreate("control")
        Clipboard.SetText(txtOut.Text)
    End Sub
End Class





''' <summary>
''' Excel helper class
''' By Dhiraj 
''' July 2009
''' </summary>
''' <remarks></remarks>
Public Class excelhelper
    Dim xlsApp As Application
    Dim xlsWB As Workbook
    Dim xlsSheet As Worksheet
    Dim xlsCell As Range
      
    Public Function read(ByVal range As String) As String
        Dim readvalue As String
        readvalue = xlsSheet.Range(range).Value
        Return readvalue
    End Function

    Public Function activeread()
        xlsApp = New Application()
        xlsCell = xlsApp.Selection
        '        xlsCell = xlsApp.Selection
        'Sub DoOnSelection() 
        '        Dim oCell As Range
        '        For Each oCell In Selection
        '            oCell.Font.Bold = True
        '        Next

    End Function

    Public Function readExcelRange(ByVal Values As System.Collections.Generic.SortedList(Of String, String)) As System.Collections.Generic.SortedList(Of String, String)
        Dim retrunvalue As New System.Collections.Generic.SortedList(Of String, String)
        For Each item As KeyValuePair(Of String, String) In Values
            retrunvalue.Add(item.Key, xlsSheet.Range(item.Key).Value)

        Next
        Return retrunvalue

    End Function
    Public Function readExcelArray(ByVal Values As String(,))

        Dim i As Integer
        For i = 0 To Values.GetUpperBound(1)
            Values(1, i) = xlsSheet.Range(Values(0, i)).Value
        Next
        Return Values

    End Function

    Public Function findstringRow(ByVal column As String, ByVal find As String) As Integer
        For i As Integer = 1 To 65536
            If xlsSheet.Range(column + i.ToString).Value = find Then
                Return i
                '"TOTAAL:"
            End If
        Next
        Return 0
    End Function

    Public Sub insertRow(ByVal row As Integer)
        xlsSheet.Rows(row).Insert()
    End Sub

    Public Function findnewline(ByVal column As String, ByVal startrow As Integer) As Integer
        For i As Integer = startrow To 65536
            If xlsSheet.Range(column + i.ToString).Value = "" Then
                Return i
            End If
        Next
        Return 0
    End Function

    Public Sub WriteexcelRange(ByVal Values As System.Collections.Generic.SortedList(Of String, String))
        For Each item As KeyValuePair(Of String, String) In Values
            xlsCell = xlsSheet.Range(item.Key)
            xlsCell.Value = item.Value
        Next
        xlsWB.Save()

    End Sub

    Public Sub WriteexcelArray(ByVal Values(,) As String)
        'Cellname , value
        Dim i As Integer
        For i = 0 To Values.GetUpperBound(1)
            xlsCell = xlsSheet.Range(Values(0, i))
            xlsCell.Value = Values(1, i)
        Next
        xlsWB.Save()
    End Sub

    Private Sub Writeexcel(ByVal path As String, ByVal sheet As String, ByVal range As String, ByVal value As String)
        Dim xlsApp As Application
        Dim xlsWB As Workbook
        Dim xlsSheet As Worksheet
        Dim xlsCell As Range
        xlsApp = New Application
        xlsApp.Visible = False
        xlsWB = xlsApp.Workbooks.Open(path)
        xlsSheet = xlsWB.Worksheets(1)
        xlsCell = xlsSheet.Range(range)
        xlsCell.Value = value
        xlsWB.Save()
        xlsWB.Close()
        releaseObject(xlsApp)
        releaseObject(xlsWB)
        releaseObject(xlsSheet)
    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Public Sub close()
        Try
            xlsWB.Close()
            releaseObject(xlsApp)
            releaseObject(xlsWB)
            releaseObject(xlsSheet)
        Catch ex As Exception
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        Try
            xlsWB.Close()
            releaseObject(xlsApp)
            releaseObject(xlsWB)
            releaseObject(xlsSheet)
        Catch
        End Try
    End Sub

    Public Sub New(ByVal path As String, ByVal sheet As Integer)
        Try
            xlsApp = New Application
            xlsApp.Visible = False
            xlsWB = xlsApp.Workbooks.Open(path)
            xlsSheet = xlsWB.Worksheets(sheet)

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

    Public Sub New()
        xlsApp = New Application


    End Sub



    'private void Form1_Load(object sender, EventArgs e)
    '      {

    '          findmenu(menuStrip1);
    '      }
    '      private void findmenu(MenuStrip o)
    '      {
    '          TreeNode node;
    '          foreach (object crl in o.Items )
    '          {
    '              node=treeView1.Nodes.Add(crl.ToString());
    '              findSubmenu((ToolStripMenuItem)crl,node);              
    '          }
    '      }

    '      private void findSubmenu(ToolStripMenuItem o,TreeNode node)
    '      {
    '          foreach (object crl in o .DropDownItems )
    '          {
    '              node.Nodes .Add(crl.ToString());
    '          } 
    '      }







End Class