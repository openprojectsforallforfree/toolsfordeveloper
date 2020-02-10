Imports System.Text
Public Class clsDataloader
    Inherits BasicFrame
    Implements Frame
    Public Overrides Sub GenCreate_Afterlines()
        outText.AppendLine("Rslt = _dbStruct.Con.ExecuteNonQuery(SQLCreate.ToString());")
        outText.AppendLine("LogTrace.WriteInfoLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, " & q & tablename & " table created Successfully. " & q & ");")
        outText.AppendLine("Status = true;}")
    End Sub
    Public Overrides Sub GenCreate_Beforelines()
        outText.AppendLine("DataTable dt = new DataTable();")
        outText.AppendLine("StringBuilder SQL = new StringBuilder();")
        outText.AppendLine("SQL.Append(" & q & "select * from " & tablename & q & ");")
        outText.AppendLine("dt = BLL.GlobalResources.SelectDBConnection.ExecuteDataTable(SQL.ToString());")
        outText.AppendLine("   if (dt.Rows.Count > 0)  {")

    End Sub
    Public Overrides Sub GenCreate_loop()



        outText.AppendLine("  lbl" & dbname & ".Text= dt.Rows[0][" & q & dbname & q & "].ToString();")


    End Sub
    Public Overrides Sub GenCreate_loop_Fistline()
        GenCreate_loop()
    End Sub
    Public Overrides Sub GenCreate_loop_Lastline()
        outText.AppendFormat("SQLCreate.Append( " & q & " \n, {0} {1} )" & q & ");", dbname, type, constraint, Cntrol)
    End Sub
End Class
