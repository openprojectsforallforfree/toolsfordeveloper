Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms



Partial Public Class frmInsertGenerate
    Inherits Form

    Private dbcon As Bsoft.Data.DBConnect
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs)

        dbcon = New Bsoft.Data.DBConnect()
        dbcon.ConnectionString = My.MySettings.Default.connection

    End Sub

    Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim dt As DataTable
        dt = dbcon.ExecuteDataTable("select Table_Name from Information_Schema.Tables")

        gridTables.DataSource = dt
    End Sub
    Private tblName As String
    Private dtFields As DataTable
    Private Sub displayfield()
        'tblName 
        Dim dgvr As DataGridViewRow = gridTables.CurrentRow
        tblName = dgvr.Cells("Table_Name").Value.ToString()
        dtFields = dbcon.ExecuteDataTable(("select Name from sys.columns where " & "Object_ID = Object_ID('") + tblName & "')")
        gridFields.DataSource = dtFields
    End Sub







    Private Sub frmInsertGenerate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dbcon = New Bsoft.Data.DBConnect()
        dbcon.ConnectionString = My.MySettings.Default.connection

        Dim dt As DataTable
        dt = dbcon.ExecuteDataTable("select Table_Name from Information_Schema.Tables")

        gridTables.DataSource = dt
        Try
            gridTables.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gridTables_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gridTables.SelectionChanged
        displayfield()
        If chkShowData.Checked Then
            Dim sbSdt As New StringBuilder()
            sbSdt.AppendFormat("SELECT * FROM {0} ", tblName)
            Dim dtSBSdta As DataTable = dbcon.ExecuteDataTable(sbSdt)
            gridDaata.DataSource = dtSBSdta
        End If
        Try
            gridDaata.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            gridFields.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim sb As New StringBuilder()
        Dim sbS As New StringBuilder()
        sbS.AppendFormat("SELECT * FROM {0} ", tblName)
        Dim dtSBS As DataTable = dbcon.ExecuteDataTable(sbS)
        For Each drSbs As DataRow In dtSBS.Rows
            sb.AppendFormat("INSERT INTO {0} VALUES ( ", tblName)
            For Each drFilelds As DataRow In dtFields.Rows
                If drFilelds("Name").ToString().ToLower() <> "id" Then
                    sb.Append("'" & drSbs(drFilelds("Name").ToString()).ToString() & "'")
                    sb.Append(",")
                End If
            Next
            sb.Remove(sb.Length - 1, 1)
            sb.AppendLine(")")
        Next
        Try
            txtOut.Text = sb.ToString()
            Clipboard.SetText(sb.ToString())
        Catch
        End Try
    End Sub

   

    Private Sub gridDaata_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gridDaata.SelectionChanged
        Dim AVALUE As String
        Dim i As Integer = 1
        Dim sb As New StringBuilder()
        Try

       
            ' id = gridDaata.SelectedRows.Item(0).Cells(0).Value.ToString()

            sb.AppendFormat("INSERT INTO {0} VALUES ( ", tblName)
            For i = 1 To gridDaata.ColumnCount - 1
                AVALUE = gridDaata.SelectedRows.Item(0).Cells(i).Value.ToString()
                AVALUE = AVALUE.Replace("'", "''")
                sb.Append("'" & AVALUE & "'")
                sb.Append(",")
                'i = i + 1
                'If drFilelds("Name").ToString().ToLower() <> "id" Then
                '    sb.Append("'" & drSbs(drFilelds("Name").ToString()).ToString() & "'")
                '    sb.Append(",")
                'End If
            Next
            sb.Remove(sb.Length - 1, 1)
            sb.AppendLine(")")
        Catch ex As Exception

        End Try
        Try
            txtOut.Text = sb.ToString()
            Clipboard.SetText(sb.ToString())
        Catch
        End Try
    End Sub
End Class


