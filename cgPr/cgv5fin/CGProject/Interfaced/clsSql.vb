Public Class clsInsertSql
    Inherits BasicFrame
    Implements Frame
    Public Overrides Sub GenCreate_Beforelines()
        outText.AppendFormat("insert into {0}  (", tablename)

    End Sub
    Public Overrides Sub GenCreate_loop_Fistline()
        outText.AppendFormat("{0}", dbname)
    End Sub

    Public Overrides Sub GenCreate_loop()
        outText.AppendFormat(",{0}", dbname)
    End Sub

    Public Overrides Sub GenCreate_loop_Lastline()
        outText.AppendFormat(",{0})", dbname)
    End Sub

    Public Overrides Sub GenCreate_Afterlines()
        outText.AppendFormat(" values (", dbname)
    End Sub

End Class

Public Class clsInsertSqlValues
    Inherits BasicFrame
    Implements Frame
    Public Overrides Sub GenCreate_Beforelines()


    End Sub
    Public Overrides Sub GenCreate_loop_Fistline()
        If isNumberType() Then
            outText.AppendFormat("{0}", txt)
        Else
            outText.AppendFormat("'{0}'", txt)
        End If

    End Sub

    Public Overrides Sub GenCreate_loop()
        If isNumberType() Then
            outText.AppendFormat(",{0}", txt)
        Else
            outText.AppendFormat(",'{0}'", txt)
        End If

    End Sub

    Public Overrides Sub GenCreate_loop_Lastline()
        If isNumberType() Then
            outText.AppendFormat(",{0})", txt)
        Else
            outText.AppendFormat(",'{0}')", txt)
        End If
    End Sub

    Public Overrides Sub GenCreate_Afterlines()

    End Sub

End Class