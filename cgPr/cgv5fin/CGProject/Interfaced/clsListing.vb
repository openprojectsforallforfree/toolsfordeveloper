Imports System.Text
Public Class clsListing
    Inherits BasicFrame
    Implements Frame
    Public Overrides Sub GenCreate_Beforelines()
        outText.AppendLine("TableName = " & q & tablename & q & ";")
    End Sub
    Public Overrides Sub GenCreate_loop_Fistline()
        outText.AppendFormat("DataTableColumns.Add( " & q & "{0}" & q & ",{1}," & q & "{2}" & q & ",true ,true);", dbname, CrudeType, colName)
        outText.AppendLine()
    End Sub
    Public Overrides Sub GenCreate_loop()
        outText.AppendFormat("DataTableColumns.Add( " & q & "{0}" & q & ",{1}," & q & "{2}" & q & ");", dbname, CrudeType, colName)
        outText.AppendLine()
    End Sub
    Public Overrides Sub GenCreate_loop_Lastline()
        GenCreate_loop()
    End Sub
    Public Overrides Sub GenCreate_Afterlines()
       
    End Sub
End Class
