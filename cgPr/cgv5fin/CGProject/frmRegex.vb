Imports System.Text.RegularExpressions
Imports System.IO
Public Class frmRegex

    Private Sub txtOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOk.Click

        Dim FI As New FileInfo(FileBrowser1.text)
        Try
            Dim SR As New StreamReader(FI.OpenRead())
            Dim s As [String]
            Dim temp As [String] = ""
            Dim pos As Integer = 0
            s = SR.ReadLine()
            While s IsNot Nothing
                temp = temp + s & vbCr & vbLf
                s = SR.ReadLine()
            End While
            temp = temp.Substring(0, temp.Length - 2)
            SR.Close()

            temp = getregx(temp, "values \(.*\)")
           
            Dim SW As New StreamWriter(FileBrowser2.text)
            SW.Write(temp)
            SW.Close()
        Catch ex As Exception
            MessageBox.Show("Error opening file! Error:" & ex.Message, "IO Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try





     
    End Sub

    Private Function getregx(ByVal instring As String, ByVal regx As String) As String
        Dim m As Match


        m = Regex.Match(instring, regx)
        Dim temp As String = String.Empty

        While m.Success
            Dim s As String
            s = m.Value.ToString()
            temp = temp + s.ToString() + vbCrLf
            'Console.WriteLine(s.ToString())
            m = m.NextMatch()
        End While
        Return temp
    End Function
End Class