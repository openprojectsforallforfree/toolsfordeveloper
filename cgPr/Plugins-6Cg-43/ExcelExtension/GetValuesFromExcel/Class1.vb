Public Class GetValues
    Public Function getValuesFromExcel() As DataSet
        Dim selection_area As Integer
        Dim selection_column As Integer
        Dim selection_row As Integer
        Dim dt As DataTable = New DataTable
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
End Class
