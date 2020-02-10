Imports System.Text
Public Class clsSearchCondition
    Inherits BasicFrame
    Implements Frame
    Public Overrides Sub GenCreate_Beforelines()
    End Sub
    Public Overrides Sub GenCreate_loop_Fistline()
        GenCreate_loop()
    End Sub
    Public Overrides Sub GenCreate_loop()
        outText.AppendFormat("SearchConditionList.AddCriteria(ColumnTypes.Number, " & q & "{0}" & q & ", ComparisionTypes.EQUAL, {1});", dbname, controlName)
        outText.AppendLine()
    End Sub
    Public Overrides Sub GenCreate_loop_Lastline()
        GenCreate_loop()
    End Sub
    Public Overrides Sub GenCreate_Afterlines()
    End Sub
End Class

Public Class clsSelect
    Inherits BasicFrame
    Implements Frame
    Public Overrides Sub GenCreate_Beforelines()
        outText.Append("SELECT ")
    End Sub
    Public Overrides Sub GenCreate_loop_Fistline()
        GenCreate_loop()
    End Sub
    Public Overrides Sub GenCreate_loop()
        outText.Append(dbname + ",")
    End Sub
    Public Overrides Sub GenCreate_loop_Lastline()
        outText.Append(dbname)
    End Sub
    Public Overrides Sub GenCreate_Afterlines()
        outText.AppendLine(" FROM " + tablename + " WHERE id = 1")
    End Sub
End Class
Public Class clsSelectWithLeftJoin
    Inherits BasicFrame
    Implements Frame
    Public Overrides Sub GenCreate_Beforelines()

    End Sub
    Public Overrides Sub GenCreate_loop_Fistline()
        GenCreate_loop()
    End Sub
    Public Overrides Sub GenCreate_loop()
        outText.Append(tablename + "." + dbname + " AS " + tablename + "_" + dbname + ",")
    End Sub
    Public Overrides Sub GenCreate_loop_Lastline()
        outText.Append(tablename + "." + dbname + " AS " + tablename + "_" + dbname)
    End Sub
    Public Overrides Sub GenCreate_Afterlines()

    End Sub
End Class
