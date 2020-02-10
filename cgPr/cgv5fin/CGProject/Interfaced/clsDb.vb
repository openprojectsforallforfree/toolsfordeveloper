Imports System.Text
Public Class clsDb
    Inherits BasicFrame
    Implements Frame
    Dim constraintString As New StringBuilder()
    Public Overrides Sub GenCreate_Afterlines()

        outText.AppendLine("Rslt = _dbStruct.Con.ExecuteNonQuery(changeSQL(SQLCreate.ToString()));")
        outText.AppendLine("LogTrace.WriteInfoLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, " & q & tablename & " table created Successfully. " & q & ");")
        outText.AppendLine("Status = true;}")
    End Sub
    Public Overrides Sub GenCreate_Beforelines()
        outText.AppendLine("if (!_dbStruct.DoesTableExists( " & q & tablename & q & ")){")
        outText.AppendLine("SQLCreate.Remove(0, SQLCreate.Length);")
        outText.AppendLine("SQLCreate.AppendLine( " & q & " CREATE TABLE " & tablename & "( " & q & ");")
    End Sub
    Public Overrides Sub GenCreate_loop()
        If constraint.ToLower = "pk" Or dbname.ToLower() = "id" Then
            outText.AppendFormat("SQLCreate.AppendLine( " & q & "{0} {1} primary key identity(1,1) ," & q & ");", dbname, type)
        Else
            If InStr(dbname, "fk") > 0 Or dbname.ToLower().EndsWith("id") Then

                constraintString.AppendFormat("SQLCreate.AppendLine( " & q & "  CONSTRAINT FK_{1}_{0} FOREIGN KEY ({0}) REFERENCES {1} (Id)," & q & ");", dbname, constraint)
                constraintString.AppendLine()
            End If
            outText.AppendFormat("SQLCreate.AppendLine( " & q & " {0} {1}," & q & ");", dbname, type)

        End If
        outText.AppendLine()
    End Sub
    Public Overrides Sub GenCreate_loop_Fistline()
        GenCreate_loop()

    End Sub
    Public Overrides Sub GenCreate_loop_Lastline()
        GenCreate_loop()

        Dim a As String
        a = outText.ToString().Trim()
        Dim b As String
        b = constraintString.ToString().Trim()

        outText.Remove(0, outText.Length)
        outText.Append(a)
        outText.Append(b)
        outText = outText.Remove(outText.Length - 4, 4)
        outText.Append(")" & q & ");")
        outText.AppendLine()
    End Sub
End Class


Public Class clsDb2
    Inherits BasicFrame
    Implements Frame
    Dim constraintString As New StringBuilder()
    Public Overrides Sub GenCreate_Afterlines()
         
    End Sub
    Public Overrides Sub GenCreate_Beforelines()
        outText.AppendLine(" CREATE TABLE " & tablename & "( ")
    End Sub
    Public Overrides Sub GenCreate_loop()
        'If constraint.ToLower = "pk" Or dbname.ToLower() = "id" Then
        '    outText.AppendFormat("{0} {1} primary key identity(1,1) ,", dbname, type)
        'Else
        '    If InStr(dbname, "fk") > 0 Or dbname.ToLower().EndsWith("id") Then

        '        constraintString.AppendFormat("  CONSTRAINT FK_{1}_{0} FOREIGN KEY ({0}) REFERENCES {1} (Id),", dbname, constraint)
        '        constraintString.AppendLine()
        '    End If
        '    outText.AppendFormat("{0} {1},", dbname, type)
        'End If
        outText.AppendFormat("{0} {1} {2},", dbname, type, constraint)
        outText.AppendLine()
    End Sub
    Public Overrides Sub GenCreate_loop_Fistline()
        GenCreate_loop()

    End Sub
    Public Overrides Sub GenCreate_loop_Lastline()
        outText.AppendFormat("{0} {1} {2}", dbname, type, constraint)
        outText.AppendLine(")")
    End Sub
End Class
