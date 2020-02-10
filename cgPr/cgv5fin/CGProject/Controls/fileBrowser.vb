Public Class fileBrowser
    Dim a As New OpenFileDialog
    Dim b As New SaveFileDialog
    Public Event filetextchanged()
    Dim filebrowsertype_ As filebrowsertype



    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        
        Select Case filebrowsertype_
            Case filebrowsertype.openfile

                If (a.ShowDialog() = DialogResult.OK) Then
                    TextBox1.Text = a.FileName
                    RaiseEvent filetextchanged()
                End If

            Case filebrowsertype.savefile
                If (b.ShowDialog() = DialogResult.OK) Then
                    TextBox1.Text = b.FileName
                    RaiseEvent filetextchanged()
                End If
        End Select

    End Sub

    Public Shadows Function text() As String
        Return TextBox1.Text
    End Function


    Public Property path() As String

        Set(ByVal value As String)
            TextBox1.Text = value

        End Set
        Get
            Return TextBox1.Text
        End Get
    End Property

    Public Property filter() As String

        Set(ByVal value As String)
            a.Filter = value

        End Set
        Get
            Return a.Filter
        End Get
    End Property

    Public Property browsertype() As filebrowsertype

        Set(ByVal value As filebrowsertype)
            filebrowsertype_ = value

        End Set
        Get
            Return filebrowsertype_
        End Get
    End Property

  
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        b.CheckFileExists = False
        b.CheckPathExists = True
        b.OverwritePrompt = False
        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
Public Enum filebrowsertype
    openfile
    savefile
    folder
End Enum