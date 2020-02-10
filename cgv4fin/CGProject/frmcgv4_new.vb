Imports System.Globalization
Imports System.Threading
Imports System.ComponentModel
Imports System.IO

Public Class frmcgv4
    Dim outputDirectroy As String
    Dim inputDirectroy As String
    Const templateListclassname As String = "frmListSample"
    Const templateEntryclassname As String = "frmEntrySample"
    Dim dt As DataTable

    Private Sub frmSQlGen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        inputDirectroy = Application.StartupPath + "/templates/"
        outputDirectroy = Application.StartupPath + "/outputs/"
    End Sub


#Region "Functions"
    Private Function createControls(ByVal withLables As Boolean) As String
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

    Private Sub entryFormGenerate(ByVal tableName As String, ByVal blank As Boolean)
        Dim fr As New clsFindReplace()
        Dim inclassfile As String = inputDirectroy + "\" + templateEntryclassname + ".cs"
        Dim outclassfile As String = outputDirectroy + "\" + "ent" + tableName + ".cs"
        Dim indesignerfile As String = inputDirectroy + "\" + templateEntryclassname + ".Designer.cs"
        Dim outdesignerfile As String = outputDirectroy + "\" + "ent" + tableName + ".Designer.cs"
        Dim inresourcefile As String = inputDirectroy + "\" + templateEntryclassname + ".resx"
        Dim outresourcefile As String = outputDirectroy + "\" + "ent" + tableName + ".resx"
        Dim genstring As String = ""
        fr.findReplace(inclassfile, outclassfile, templateEntryclassname, "ent" + tableName)
        fr.findReplace(indesignerfile, outdesignerfile, templateEntryclassname, "ent" + tableName)
        fr.findReplace(inresourcefile, outresourcefile, templateEntryclassname, "ent" + tableName)
        If Not blank Then
            genstring = (New clsEntry()).gencreate(dt)
            fr.findReplace(outclassfile, outclassfile, "//<Entry>", "//<Entry>" + vbCrLf + genstring)
            fr.findReplace(outdesignerfile, outdesignerfile, "}//<Control>", createControls(True))
        End If
    End Sub

    Private Sub listingFormGenerate(ByVal tableName As String, ByVal blank As Boolean)
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
        End If
    End Sub

   
    Public Function getValuesFromExcel() As DataSet
        Dim selection_area As Integer
        Dim selection_column As Integer
        Dim selection_row As Integer
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
                                    dt = New DataTable(range(i, 1))
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

        Return ds  '/ lblStatus.Text = "Finished " + what
    End Function

#End Region
#Region "Button Clicks"
    Private Sub btnDb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDbCreate.Click
        'Data base
        Dim a As New clsDb()
        Clipboard.SetText(a.gencreate(dt))
    End Sub

    Private Sub btnInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        'insert
        Dim s As New clsInsertSql()
        Dim v As New clsInsertSqlValues()
        Dim ss As String, vv As String
        ss = s.gencreate(dt)
        vv = v.gencreate(dt)
        Try
            Clipboard.SetText(ss + vv)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListing.Click
        'Lsiting
        Dim s As New clsListing()
        Dim ss As String
        ss = s.gencreate(dt)
        Try
            Clipboard.SetText(ss)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEntry.Click
        'Entry
        Dim s As New clsEntry()
        Dim ss As String
        ss = s.gencreate(dt)
        Try
            Clipboard.SetText(ss)
        Catch ex As Exception
        End Try
    End Sub


    Private Sub btnControl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnControl.Click
        'Control
        Try
            Clipboard.SetText(createControls(False))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrid.Click
        'Grid
        Dim grideclare As New clsgridDeclare()
        Dim grid As New clsGrid()
        Dim gridrange As New clsGridAddRange()

        Try
            Clipboard.SetText(grid.gencreate(dt) + gridrange.gencreate(dt) + "}" + grideclare.gencreate(dt))
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
                entryFormGenerate(tablename, chkBlank.Checked)
            End If

            If rdoListing.Checked Or rdoBoth.Checked Then
                'listing
                listingFormGenerate(tablename, chkBlank.Checked)
            End If
        Next
    End Sub

    Private Sub btnDuplicaion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDuplication.Click
        'Duplication
        Dim A As New frmDuplicationForm()
        A.Show()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCriteria.Click
        'Search criteria
        Dim a As New clsSearchCondition()
        Dim b As String = a.gencreate(dt)
        Try
            Clipboard.SetText(b)
        Catch ex As Exception
        End Try
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
End Class