Imports System.IO
Public Class clsFindReplace
    Public Sub findReplace()
        Dim txtFound As String = ""
        Dim txtFolder As String = ""
        Dim lstFound As String = ""
        Dim txtFind As String = ""
        Dim txtReplace As String = ""
        Dim txtreplacedirectory As String = "C:\Users\dhirajb\Desktop\test\output"
        txtFolder = "C:\Users\dhirajb\Desktop\test"
        Dim DI As New DirectoryInfo(txtFolder)
        If DI.Exists Then
            Dim FileCount As Integer = 0
            lstFound = ""
            txtFind = "frmEntrySample"
            txtReplace = "frmEntrySampleCopy"
            For Each FI As FileInfo In DI.GetFiles()
                If FI.Extension.ToLower() = ".resx" Or FI.Extension.ToLower() = ".cs" Then
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
                        If temp.IndexOf(txtFind) >= 0 Then
                            pos = temp.IndexOf(txtFind)
                            'txtFound =txtFound +FI.Name + " Char:"+pos+System.Environment.NewLine;
                            '   lstFound.Items.Add((FI.Name & " Char:") + pos + System.Environment.NewLine)
                            'MessageBox.Show("temp:"+temp+ " "+"txtFind:"+txtFind.Text+"  txtReplace:"+txtReplace.Text);
                            temp = temp.Replace(txtFind, txtReplace)

                            ' FI.Delete()
                            Dim replacefilename As String = txtreplacedirectory + "\" + txtReplace + FI.Extension


                            Dim SW As New StreamWriter(replacefilename)
                            SW.Write(temp)
                            SW.Close()
                            FileCount += 1
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Error opening file! Error:" & ex.Message, "IO Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
                    End Try
                End If
            Next
            ' Foreach Loop Ends Here
            txtFound = ((txtFound & "Replace Completed Successfully...") + System.Environment.NewLine & "No of Occurences :") + FileCount.ToString()
        Else
            ' IF Loop Ends Here
            MessageBox.Show("Please enter a valid directory path!", "InValid Direcory", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End If
    End Sub

    Public Sub findReplace(ByVal findfile As String, ByVal replacefile As String, ByVal findstring As String, ByVal replacestring As String)
        Dim FI As New FileInfo(findfile)
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
            If temp.IndexOf(findstring) >= 0 Then
                pos = temp.IndexOf(findstring)
                temp = temp.Replace(findstring, replacestring)
            End If
            Dim SW As New StreamWriter(replacefile)
            SW.Write(temp)
            SW.Close()
        Catch ex As Exception
            MessageBox.Show("Error opening file! Error:" & ex.Message, "IO Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

    Public Sub find(ByVal folder As String, ByVal findtext As String)
        Dim founditems As String = String.Empty
        Dim DI As New DirectoryInfo(folder)
        If DI.Exists Then
            Dim FileCount As Integer = 0

            For Each FI As FileInfo In DI.GetFiles()
                If FI.Extension = "*.*" Then
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
                        'MessageBox.Show("temp:"+temp+"   txtFind:"+txtFind.Text);
                        SR.Close()

                        If temp.IndexOf(findtext) >= 0 Then
                            pos = temp.IndexOf(findtext)
                            'txtFound =txtFound +FI.Name + " Char:"+pos+System.Environment.NewLine;

                            founditems += ((FI.Name & ", Char:") + pos)

                            's=s.Replace(txtFind.Text,txtReplace.Text);
                            'StreamWriter SW= new StreamWriter(FI);
                            'SW.Write(s);
                            'SW.Close();

                            FileCount += 1


                        End If
                    Catch ex As Exception
                        MessageBox.Show("Error opening file! Error:" & ex.Message, "IO Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
                    End Try

                End If
            Next
            ' Foreach Loop Ends Here
            founditems = ((founditems & "Search Completed Successfully...") + System.Environment.NewLine & "No of Occurences :") + FileCount
        Else
            ' IF Loop Ends Here
            MessageBox.Show("Please enter a valid directory path!", "InValid Direcory", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End If


    End Sub
End Class
