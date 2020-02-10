Imports System.Text

Public Class clsgridDeclare
    Inherits BasicFrame
    Implements Frame

    Public Sub New()
        forceControl = "Grid"
    End Sub
    Public Overrides Sub GenCreate_Afterlines()
    End Sub
    Public Overrides Sub GenCreate_Beforelines()

    End Sub
    Public Overrides Sub GenCreate_loop()
        Cntrol = "Grid"
        forceControl = "Grid"
        getcontrol("this")
    End Sub
    Public Overrides Sub GenCreate_loop_Fistline()
        GenCreate_loop()
    End Sub
    Public Overrides Sub GenCreate_loop_Lastline()
        GenCreate_loop()
    End Sub


    Public Sub getcontrol(ByVal parent As String)
        Select Case Cntrol

            Case "Grid"

                outText.AppendLine(" private System.Windows.Forms.DataGridViewTextBoxColumn  " & colName & "; ")


        End Select
    End Sub
    
End Class

Public Class clsGrid
    Inherits BasicFrame
    Implements Frame

    Public Sub New()
        forceControl = "Grid"
    End Sub
    Public Overrides Sub GenCreate_Afterlines()
    End Sub
    Public Overrides Sub GenCreate_Beforelines()

    End Sub
    Public Overrides Sub GenCreate_loop()

        Cntrol = "Grid"
        forceControl = "Grid"

        getcontrol("this")
    End Sub
    Public Overrides Sub GenCreate_loop_Fistline()
        GenCreate_loop()
    End Sub
    Public Overrides Sub GenCreate_loop_Lastline()
        GenCreate_loop()
    End Sub

    Public Sub getcontrol(ByVal parent As String)
        Select Case Cntrol

            Case "Grid"

                outText.AppendLine("// ")
                outText.AppendLine("// " & colName)
                outText.AppendLine("// ")
                outText.AppendLine("this." & colName & " = new System.Windows.Forms.DataGridViewTextBoxColumn();")

                outText.AppendLine("this." & colName & ".AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;")
                If txt.Trim() = "" Then
                    txt = dbname
                End If
                outText.AppendLine(" this." & colName & " .HeaderText = " & q & txt & q & ";")
                outText.AppendLine(" this." & colName & " .Tag = " & q & dbname & q & ";")
                outText.AppendLine("this." & colName & ".Name = " & q & colName & q & ";")

                If dbname.ToLower() = "id" Then
                    outText.AppendLine(" this." & colName & " .Visible = false ;")
                End If

        End Select
    End Sub
End Class

Public Class clsGridAddRange
    Inherits BasicFrame
    Implements Frame

    Public Sub New()
        forceControl = "Grid"
    End Sub
    Public Overrides Sub GenCreate_Afterlines()
    End Sub
    Public Overrides Sub GenCreate_Beforelines()
        outText.AppendLine("this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {")
    End Sub

    Public Overrides Sub GenCreate_loop_Fistline()
        GenCreate_loop()
    End Sub
    Public Overrides Sub GenCreate_loop()
        outText.AppendLine("this." & "col" & dbname & ",")
    End Sub
    Public Overrides Sub GenCreate_loop_Lastline()
        outText.AppendLine("this." & "col" & dbname & "});")
    End Sub
End Class
