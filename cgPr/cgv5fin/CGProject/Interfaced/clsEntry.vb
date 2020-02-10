Imports System.Text
Public Class clsEntry
    Inherits BasicFrame
    Implements Frame

    Public Overrides Sub GenCreate_Beforelines()
        outText.AppendLine("TableName = " & q & tablename & q & ";")
    End Sub
    Public Overrides Sub GenCreate_loop_Fistline()
        outText.AppendFormat("ColumnList.AddNewEditColumn(" & q & "{0}" & q & ",{1}, {2} ,true ,false );", dbname, CrudeType, controlName)
        outText.AppendLine()
    End Sub
    Public Overrides Sub GenCreate_loop()
        outText.AppendFormat("ColumnList.AddNewEditColumn(" & q & "{0}" & q & ",{1}, {2} );", dbname, CrudeType, controlName)
        outText.AppendLine()
        If Cntrol = "lblComboBox" Then
            outText.AppendFormat("ComboBoxDataLoader.LoadData(" & q & "Id" & q & ", " & q & "Title" & q & ", " & q & "{1}" & q & ", {0}.cmbBox , true);", controlName, constraint)
        End If
        If Cntrol = "ComboBox" Then
            outText.AppendFormat("ComboBoxDataLoader.LoadData(" & q & "Id" & q & ", " & q & "Title" & q & ", " & q & "{1}" & q & ", {0}, true);", controlName, constraint)
        End If
        outText.AppendLine()
      
    End Sub

    Public Overrides Sub GenCreate_loop_Lastline()
        GenCreate_loop()
    End Sub
    Public Overrides Sub GenCreate_Afterlines()
       
    End Sub
End Class
