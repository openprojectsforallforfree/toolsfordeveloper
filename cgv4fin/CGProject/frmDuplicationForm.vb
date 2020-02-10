Public Class frmDuplicationForm

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Dim lst As String = txtList.Text
        Dim find As String = txtFind.Text
        Dim test As String = txtText.Text
        Dim lstitem As String
        Dim out As String = ""
        Dim a() As String
        a = lst.Split(vbCrLf)
        For i As Integer = 0 To a.Length - 1
            lstitem = a(i).Trim()
            out += vbCrLf + test.Replace(find, lstitem)
        Next

        Clipboard.SetText(out)
        txtOut.Text = out
    End Sub

    Private Sub btnWithDelimiter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWithDelimiter.Click
        Dim lst As String = txtList.Text
        Dim find As String = txtFind.Text
        Dim textS As String = txtText.Text
        Dim out As String = String.Empty
        Dim listS() As String
        Dim findS() As String
        Dim lstitemS() As String
        Dim delimiter As String = txtDelimiter.Text
        Dim temp As String = ""
        findS = find.Split(delimiter)
        listS = lst.Trim().Split(vbCrLf)
        For i As Integer = 0 To listS.Length - 1
            lstitemS = listS(i).Trim().Split(delimiter)
            temp = textS
            For j As Integer = 0 To lstitemS.Length - 1
                temp = temp.Replace(findS(j), lstitemS(j))
            Next
            out += temp + vbCrLf
        Next

        Clipboard.SetText(out)
        txtOut.Text = out
    End Sub
End Class