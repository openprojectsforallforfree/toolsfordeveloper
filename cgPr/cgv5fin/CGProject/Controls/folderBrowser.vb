Public Class folderBrowser
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim fbd As New FolderBrowserDialog()
        If (fbd.ShowDialog() = DialogResult.OK) Then
            Dim a As New IO.StreamWriter(Name + ".setting")
            a.Write(fbd.SelectedPath)
            a.Close()
            txtPath.Text = fbd.SelectedPath
        End If
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub folderBrowser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim a As New IO.StreamReader(Name + ".setting")
            txtPath.Text = a.ReadLine()
            a.Close()
        Catch ex As Exception

        End Try
    End Sub
End Class