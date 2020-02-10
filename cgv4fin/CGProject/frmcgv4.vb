Imports System.Globalization
Imports System.Threading
Imports System.ComponentModel
Imports System.IO

Public Class frmcgv4
    Dim outputDirectroy As String
    Dim inputDirectroy As String
    Const templateListclassname As String = "frmListSample"
    Const templateEntryclassname As String = "frmEntrySample"


    Private Sub frmSQlGen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        inputDirectroy = Application.StartupPath + "\templates\"
        outputDirectroy = Application.StartupPath + "\outputs\"
    End Sub


#Region "Functions"
    Private Function createControls(ByVal withLables As Boolean, ByVal dt As DataTable) As String
        Dim dec As New clsDeclare_Control()
        Dim ctrl As New clsControl()
        Dim ss As String
        ss = dec.gencreate(262, 22, dt)
        Dim c As String
        c = ctrl.gencreate(262, 22, dt)
        Dim lblDec, lblCtrl As String
        If (withLables) Then
            Dim decL As New clsDeclare_Control()
            Dim ctrlL As New clsControl()
            decL.forceControl = "Label"
            ctrlL.forceControl = "Label"
            lblDec = decL.gencreate(100, 22, dt)
            lblCtrl = ctrlL.gencreate(100, 22, dt)
            Return c + vbCrLf + lblCtrl + vbCrLf + "}//<Control>" + vbCrLf + ss + vbCrLf + lblDec
        End If
        Return c + vbCrLf + "}//<Control>" + vbCrLf + ss
    End Function

    Private Sub entryFormGenerate(ByVal tableName As String, ByVal blank As Boolean, ByVal dt As DataTable)
        Dim fr As New clsFindReplace()
        Dim infile As String = inputDirectroy + "\" + templateEntryclassname
        Dim outfile As String = outputDirectroy + "\" + "ent" + tableName

        Dim genstring As String = ""
        fr.findReplace(infile + ".cs", outfile + ".cs", templateEntryclassname, "ent" + tableName)
        fr.findReplace(infile + ".Designer.cs", outfile + ".Designer.cs", templateEntryclassname, "ent" + tableName)
        fr.findReplace(infile + ".resx", outfile + ".resx", templateEntryclassname, "ent" + tableName)


        If Not blank Then
            genstring = (New clsEntry()).gencreate(dt)
            fr.findReplace(outfile + ".cs", outfile + ".cs", "//<Entry>", "//<Entry>" + vbCrLf + genstring)
            'for form text
            fr.findReplace(outfile + ".Designer.cs", outfile + ".Designer.cs", "TextExtraValue", dt.Namespace)
            fr.findReplace(outfile + ".Designer.cs", outfile + ".Designer.cs", "LableExtraValue", dt.Namespace)
            fr.findReplace(outfile + ".Designer.cs", outfile + ".Designer.cs", "}//<Control>", createControls(False, dt))
        End If
    End Sub

    Private Sub listingFormGenerate(ByVal tableName As String, ByVal blank As Boolean, ByVal dt As DataTable)
        Dim fr As New clsFindReplace()

        Dim inclassfile As String = inputDirectroy + "\" + templateListclassname + ".cs"
        Dim outclassfile As String = outputDirectroy + "\" + "lst" + tableName + ".cs"
        Dim indesignerfile As String = inputDirectroy + "\" + templateListclassname + ".Designer.cs"
        Dim outdesignerfile As String = outputDirectroy + "\" + "lst" + tableName + ".Designer.cs"
        Dim inresourcefile As String = inputDirectroy + "\" + templateListclassname + ".resx"
        Dim outresourcefile As String = outputDirectroy + "\" + "lst" + tableName + ".resx"
        Dim genstring As String = ""
        fr.findReplace(inclassfile, outclassfile, templateListclassname, "lst" + tableName)
        fr.findReplace(indesignerfile, outdesignerfile, templateListclassname, "lst" + tableName)
        fr.findReplace(inresourcefile, outresourcefile, templateListclassname, "lst" + tableName)
        fr.findReplace(outclassfile, outclassfile, templateEntryclassname, "ent" + tableName)
        If Not blank Then
            Dim grideclare As New clsgridDeclare()
            Dim grid As New clsGrid()
            Dim gridrange As New clsGridAddRange()
            Dim gridCode As String

            gridCode = grid.gencreate(dt) + gridrange.gencreate(dt) + "}" + grideclare.gencreate(dt)
            genstring = (New clsListing()).gencreate(dt)
            fr.findReplace(outclassfile, outclassfile, "//<Listing>", "//<Listing>" + vbCrLf + genstring)
            fr.findReplace(outdesignerfile, outdesignerfile, "}//<Control>", gridCode)
            'for extra text
            fr.findReplace(outdesignerfile, outdesignerfile, "TextExtraValue", dt.Namespace + " List")
        End If
    End Sub
    Public Function getValuesFromExcel() As DataSet
        Dim selection_area As Integer
        Dim selection_column As Integer
        Dim selection_row As Integer
        Dim dt As DataTable
        Dim ds As New DataSet
        Dim i As Integer = 0
        Dim dr As DataRow
        Dim range As Object
        Dim newTable As Boolean = True
        Dim Excel As Microsoft.Office.Interop.Excel.Application
        Try
            Excel = GetObject(, "Excel.Application")
        Catch ex As Exception
            MsgBox("Please Open Excel Application" & vbCrLf & ex.Message)
            Return Nothing
        End Try

        Dim workbook As Microsoft.Office.Interop.Excel.Workbook = Excel.ActiveWorkbook
        Dim Worksheet As Microsoft.Office.Interop.Excel.Worksheet = Excel.ActiveSheet
        If workbook IsNot Nothing Then
            If Worksheet IsNot Nothing Then
                selection_area = Excel.Selection.Areas.Count
                selection_column = Excel.Selection.Columns.Count
                selection_row = Excel.Selection.Rows.Count

                If selection_area >= 1 Then
                    If selection_column = 5 Then
                        'get all from excel
                        ' range = Excel.Selection.cells.value
                        'assing values 

                        Dim rowcount As Integer = 0
                        Dim singleArea As Object
                        For Each singleArea In Excel.Selection.Areas
                            range = singleArea.cells.value
                            selection_row = singleArea.Rows.Count
                            ' MessageBox.Show(range(1, 1).ToString().Trim())
                            newTable = True
                            For i = 1 To selection_row
                                If range(i, 1) Is Nothing OrElse range(i, 1).ToString().Trim() = "" Then
                                    newTable = True
                                Else
                                    If newTable = True Then
                                        dt = New DataTable(range(i, 1), range(i, 4))
                                        dt.Columns.Add("Table")
                                        dt.Columns.Add("Type")
                                        dt.Columns.Add("Constraint")
                                        dt.Columns.Add("Text")
                                        dt.Columns.Add("Control")
                                        ds.Tables.Add(dt)
                                        newTable = False
                                    Else
                                        dr = dt.NewRow()
                                        dr.Item("Table") = range(i, 1)
                                        dr.Item("Type") = range(i, 2)
                                        dr.Item("Constraint") = range(i, 3)
                                        dr.Item("Text") = range(i, 4)
                                        dr.Item("Control") = range(i, 5)
                                        dt.Rows.Add(dr)
                                    End If
                                End If
                            Next
                        Next
                    Else
                        MsgBox("Please Select At least 5 columns", MsgBoxStyle.Information)
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

        Return ds  '/ lblStatus.Text = "Finished " + what
    End Function

    Public Function getValuesFromExcelOldwithsingleselection() As DataSet
        Dim selection_area As Integer
        Dim selection_column As Integer
        Dim selection_row As Integer
        Dim dt As DataTable
        Dim ds As New DataSet
        Dim i As Integer = 0
        Dim dr As DataRow
        Dim range As Object
        Dim newTable As Boolean = True
        Dim Excel As Microsoft.Office.Interop.Excel.Application
        Try
            Excel = GetObject(, "Excel.Application")
        Catch ex As Exception
            MsgBox("Please Open Excel Application" & vbCrLf & ex.Message)
            Return Nothing
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
                        'get all from excel
                        range = Excel.Selection.cells.value
                        'assing values 
                        dt = New DataTable()
                        Dim rowcount As Integer = 0

                        For i = 1 To selection_row
                            If range(i, 1) Is Nothing OrElse range(i, 1).ToString().Trim() = "" Then
                                newTable = True
                            Else
                                If newTable = True Then
                                    dt = New DataTable(range(i, 1), range(i, 4))
                                    dt.Columns.Add("Table")
                                    dt.Columns.Add("Type")
                                    dt.Columns.Add("Constraint")
                                    dt.Columns.Add("Text")
                                    dt.Columns.Add("Control")
                                    ds.Tables.Add(dt)
                                    newTable = False
                                Else
                                    dr = dt.NewRow()
                                    dr.Item("Table") = range(i, 1)
                                    dr.Item("Type") = range(i, 2)
                                    dr.Item("Constraint") = range(i, 3)
                                    dr.Item("Text") = range(i, 4)
                                    dr.Item("Control") = range(i, 5)
                                    dt.Rows.Add(dr)
                                End If
                            End If
                        Next
                    Else
                        MsgBox("Please Select At least 5 columns", MsgBoxStyle.Information)
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

        Return ds  '/ lblStatus.Text = "Finished " + what
    End Function

#End Region
#Region "Button Clicks"
    Private Sub btnDb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDbCreate.Click
        'Data base
        Dim textout As String = ""
        Dim ds As DataSet = getValuesFromExcel()
        If ds Is Nothing Then Return

        Dim fr As New clsFindReplace()
        For Each dt As DataTable In ds.Tables
            textout = textout + vbCrLf + New clsDb().gencreate(dt)

        Next
        Clipboard.SetText(textout)


    End Sub

    Private Sub btnInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        Dim textout As String = ""
        Dim ds As DataSet = getValuesFromExcel()
        If ds Is Nothing Then Return

        Dim fr As New clsFindReplace()
        For Each dt As DataTable In ds.Tables
            textout = textout + vbCrLf + New clsInsertSql().gencreate(dt) + New clsInsertSqlValues().gencreate(dt)

        Next
        Try
            Clipboard.SetText(textout)
        Catch ex As Exception
        End Try


    End Sub

    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListing.Click
        'Lsiting

        Dim textout As String = ""
        Dim ds As DataSet = getValuesFromExcel()
        If ds Is Nothing Then Return

        Dim fr As New clsFindReplace()
        For Each dt As DataTable In ds.Tables
            textout = textout + vbCrLf + New clsListing().gencreate(dt)
        Next
        Try
            Clipboard.SetText(textout)
        Catch ex As Exception
        End Try

    End Sub

    Private Sub btnEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEntry.Click
        'Entry


        Dim textout As String = ""
        Dim ds As DataSet = getValuesFromExcel()
        If ds Is Nothing Then Return

        Dim fr As New clsFindReplace()
        For Each dt As DataTable In ds.Tables
            textout = textout + vbCrLf + New clsEntry().gencreate(dt)

        Next
        Try
            Clipboard.SetText(textout)
        Catch ex As Exception
        End Try

    End Sub

    Private Sub btnControl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnControl.Click
        'Control


        Dim textout As String = ""
        Dim ds As DataSet = getValuesFromExcel()
        If ds Is Nothing Then Return

        Dim fr As New clsFindReplace()
        For Each dt As DataTable In ds.Tables
            textout = textout + createControls(False, dt)

        Next
        Try
            Clipboard.SetText(textout)
        Catch ex As Exception
        End Try

    End Sub

    Private Sub btnGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrid.Click
        'Grid
        Dim textout As String = ""
        Dim ds As DataSet = getValuesFromExcel()
        If ds Is Nothing Then Return

        Dim fr As New clsFindReplace()
        For Each dt As DataTable In ds.Tables
            textout = textout + vbCrLf + New clsGrid().gencreate(dt) + New clsGridAddRange().gencreate(dt) + "}" + New clsgridDeclare().gencreate(dt)

        Next
        Try
            Clipboard.SetText(textout)
        Catch ex As Exception
        End Try

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCriteria.Click
        'Search criteria

        Dim textout As String = ""
        Dim ds As DataSet = getValuesFromExcel()
        If ds Is Nothing Then Return

        Dim fr As New clsFindReplace()
        For Each dt As DataTable In ds.Tables
            textout = textout + vbCrLf + New clsSearchCondition().gencreate(dt)

        Next
        Try
            Clipboard.SetText(textout)
        Catch ex As Exception
        End Try

    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'Generate
        Dim ds As DataSet = getValuesFromExcel()
        If ds Is Nothing Then Return
        Dim tablename As String
        Dim fr As New clsFindReplace()
        For Each dt As DataTable In ds.Tables
            tablename = dt.TableName

            If rdoEntry.Checked Or rdoBoth.Checked Then
                'entry
                entryFormGenerate(tablename, chkBlank.Checked, dt)
            End If

            If rdoListing.Checked Or rdoBoth.Checked Then
                'listing
                listingFormGenerate(tablename, chkBlank.Checked, dt)
            End If
        Next
        Clipboard.SetText(outputDirectroy)
        Process.Start(outputDirectroy)
    End Sub

    Private Sub btnDuplicaion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDuplication.Click
        'Duplication
        Dim A As New frmDuplicationForm()
        A.Show()
    End Sub

    Private Sub btnInserttion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Insert 
        Dim a As New frmInsertGenerate
        a.Show()
    End Sub

    Private Sub btnRegex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegex.Click
        'regEx
        frmRegex.Show()
    End Sub
#End Region
    Private Sub btnSelectWithLeftJoin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectWithLeftJoin.Click
        Dim textOut As String = ""
        Dim jointablestring As String = ""
        Dim maintable As String = ""
        Dim maintablestring As String = ""
        Dim ds As DataSet = getValuesFromExcel()
        If ds Is Nothing Then Return
        If ds.Tables.Count > 1 Then
            Dim smt As New frmSelectMainTable
            smt.Text = "Multiple Tables Detected .Please select the Main table!"
            For Each dt As DataTable In ds.Tables
                smt.cmbTables.Items.Add(dt.TableName)
            Next
            smt.cmbTables.SelectedIndex = 0
            If smt.ShowDialog() = Windows.Forms.DialogResult.OK Then
                maintable = smt.cmbTables.SelectedItem.ToString()
            End If
        End If
        Dim fr As New clsFindReplace()
        For Each dt As DataTable In ds.Tables
            If dt.TableName = maintable Then
                maintablestring = New clsSelectWithLeftJoin().gencreate(dt)
            Else
                jointablestring = jointablestring + "," + vbCrLf + New clsSelectWithLeftJoin().gencreate(dt)
            End If
        Next
        maintablestring = maintablestring.Replace(maintable + "_", "")
        textOut = "SELECT " + vbCrLf + maintablestring + jointablestring + vbCrLf + "FROM " + maintable
        For Each dt As DataTable In ds.Tables
            If dt.TableName = maintable Then

            Else
                textOut += vbCrLf + " Left join " + dt.TableName + " on " + dt.TableName + ".Id = " + maintable + "." + dt.TableName + "Id "
            End If
        Next


        Try
            Clipboard.SetText(textOut)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        'Search criteria

        Dim textout As String = ""
        Dim maintable As String = ""
        Dim ds As DataSet = getValuesFromExcel()
        If ds Is Nothing Then Return
        Dim fr As New clsFindReplace()
        For Each dt As DataTable In ds.Tables
            textout = textout + vbCrLf + New clsSelect().gencreate(dt)
        Next
        Try
            Clipboard.SetText(textout)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnListingForView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListingForView.Click
        'Generate
        Dim ds As DataSet = getValuesFromExcel()
        Dim tablename As String = ""
        If ds Is Nothing Then Return
        If ds.Tables.Count > 1 Then
            Dim smt As New frmSelectMainTable
            smt.Text = "Multiple Tables Detected .Please select the Main table!"
            For Each dt As DataTable In ds.Tables
                smt.cmbTables.Items.Add(dt.TableName)
            Next
            smt.cmbTables.SelectedIndex = 0
            If smt.ShowDialog() = Windows.Forms.DialogResult.OK Then
                tablename = smt.cmbTables.SelectedItem.ToString()
            End If
        End If

        Dim fr As New clsFindReplace()
        Dim mergedDt As New DataTable(tablename)
        mergedDt.Columns.Add("Table")
        mergedDt.Columns.Add("Type")
        mergedDt.Columns.Add("Constraint")
        mergedDt.Columns.Add("Text")
        mergedDt.Columns.Add("Control")
        For Each dt As DataTable In ds.Tables
            For Each dr As DataRow In dt.Rows
                mergedDt.Rows.Add(dt.TableName + "_" + dr("Table"), dr("Type"), "", dr("Text"), dr("Control"))
            Next
        Next

        listingFormGenerate(tablename + "View", chkBlank.Checked, mergedDt)

        Clipboard.SetText(outputDirectroy)
        Process.Start(outputDirectroy)
    End Sub
End Class