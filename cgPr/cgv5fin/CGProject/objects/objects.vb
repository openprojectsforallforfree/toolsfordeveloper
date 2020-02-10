


Public Class clsPrivates
    Inherits BasicFrame
    Implements Frame
    Public Overrides Sub GenCreate_Beforelines()
        outText.AppendLine("public class " + tablename)
        outText.AppendLine("{")

        outText.AppendLine(" public " + tablename + "()")
        outText.AppendLine(" { } ")
    End Sub
    Public Overrides Sub GenCreate_loop_Fistline()
        GenCreate_loop()
    End Sub
    Public Overrides Sub GenCreate_loop()
        outText.AppendLine(" string _" + dbname + ";")
    End Sub
    Public Overrides Sub GenCreate_loop_Lastline()
        GenCreate_loop()
    End Sub
    Public Overrides Sub GenCreate_Afterlines()

    End Sub
End Class

'public string a
'        {
'            get { return _a; }
'            set { _a = value; }
'        }
Public Class clsProperties
    Inherits BasicFrame
    Implements Frame
    Public Overrides Sub GenCreate_Beforelines()


    End Sub
    Public Overrides Sub GenCreate_loop_Fistline()
        GenCreate_loop()
    End Sub
    Public Overrides Sub GenCreate_loop()
        outText.AppendLine(" public string " + dbname)
        outText.AppendLine("{")
        outText.AppendLine("get { return _" + dbname + ";}")
        outText.AppendLine("set { _" + dbname + "= value;}")
        outText.AppendLine("}")
    End Sub
    Public Overrides Sub GenCreate_loop_Lastline()
        GenCreate_loop()
    End Sub
    Public Overrides Sub GenCreate_Afterlines()

    End Sub
End Class
 
Public Class clsConstructor_DataRow
    Inherits BasicFrame
    Implements Frame
    Public Overrides Sub GenCreate_Beforelines()
        outText.AppendLine(" public " + tablename + "( DataRow dr )")
        outText.AppendLine(" {")
    End Sub
    Public Overrides Sub GenCreate_loop_Fistline()
        GenCreate_loop()
    End Sub
    Public Overrides Sub GenCreate_loop()
        
        outText.AppendFormat(" _{0} = dr[" & q & "{0}" & q & "].ToString();", dbname)
        outText.AppendLine()
    End Sub
    Public Overrides Sub GenCreate_loop_Lastline()
       GenCreate_loop()
    End Sub
    Public Overrides Sub GenCreate_Afterlines()
        outText.AppendLine(" }")
    End Sub
End Class

Public Class clsConstructora
    Inherits BasicFrame
    Implements Frame
    Public Overrides Sub GenCreate_Beforelines()
        outText.AppendLine(" public " + tablename + "(")

    End Sub
    Public Overrides Sub GenCreate_loop_Fistline()
        GenCreate_loop()
    End Sub
    Public Overrides Sub GenCreate_loop()
        Dim camelcase As String
        camelcase = dbname.Substring(0, 1).ToLower + dbname.Substring(1, dbname.Length - 1)
        outText.Append(" string " + camelcase + ",")

    End Sub
    Public Overrides Sub GenCreate_loop_Lastline()
        Dim camelcase As String
        camelcase = dbname.Substring(0, 1).ToLower + dbname.Substring(1, dbname.Length - 1)
        outText.Append(" string " + camelcase + ")")
    End Sub
    Public Overrides Sub GenCreate_Afterlines()

    End Sub
End Class
Public Class clsConstructorb
    Inherits BasicFrame
    Implements Frame
    Public Overrides Sub GenCreate_Beforelines()
        outText.AppendLine(" {")

    End Sub
    Public Overrides Sub GenCreate_loop_Fistline()
        GenCreate_loop()
    End Sub
    Public Overrides Sub GenCreate_loop()
        Dim camelcase As String
        camelcase = dbname.Substring(0, 1).ToLower + dbname.Substring(1, dbname.Length - 1)
        outText.AppendLine("_" + dbname + "=" + camelcase + ";")

    End Sub
    Public Overrides Sub GenCreate_loop_Lastline()
        GenCreate_loop()
    End Sub
    Public Overrides Sub GenCreate_Afterlines()
        outText.AppendLine(" }")
    End Sub
End Class
Public Class clsEqualOperator
    Inherits BasicFrame
    Implements Frame
    Public Overrides Sub GenCreate_Beforelines()
        outText.AppendFormat(" public static bool operator !=({0} a, {0} b)", tablename)
        outText.AppendLine("  { return !(a == b); }")
        outText.AppendFormat(" public static bool operator ==({0} a, {0} ", tablename)
        outText.AppendLine("b){ if (System.Object.ReferenceEquals(a, b)) { return true; }//same instance")
        outText.AppendLine(" if (((object)a == null) || ((object)b == null)) { return false; }//one is null other not null")
        outText.AppendLine(" if (")
    End Sub
    Public Overrides Sub GenCreate_loop_Fistline()
        GenCreate_loop()
    End Sub
    Public Overrides Sub GenCreate_loop()
        outText.AppendLine("a." + dbname + " == b." + dbname + " &&")
    End Sub
    Public Overrides Sub GenCreate_loop_Lastline()
        outText.AppendLine("a." + dbname + " == b." + dbname + ")")
    End Sub
    Public Overrides Sub GenCreate_Afterlines()
        outText.AppendLine(" { return true; }")
        outText.AppendLine("return false;        }")
    End Sub
End Class
