using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Collections;
using System.Data;
using System.Diagnostics;
using ICg;
namespace Cg1
{
    public class CreateControls
    {
        public  string generate(  DataTable dt)
        {
            clsDeclare_Control dec = new clsDeclare_Control();
            clsControl ctrl = new clsControl();
            string ss = null;
            ss = dec.gencreate(262, 22, dt);
            string c = null;
            c = ctrl.gencreate(262, 22, dt);
            return c +   "\n}//<Control>\n" + ss;
        }

    }
    public enum ControlEnum
    {
        TextBox,
        ComboBox,
        Label,
        Grid,
        GroupBox,
        RadioButton,
        CheckBox
    }
    public class clsDeclare_Control : BasicFrame, Frame
    {


        public override void GenCreate_Afterlines()
        {
        }
        public override void GenCreate_Beforelines()
        {
        }
        public override void GenCreate_loop()
        {
            getcontrol();
        }
        public override void GenCreate_loop_Fistline()
        {
            GenCreate_loop();
        }
        public override void GenCreate_loop_Lastline()
        {
            GenCreate_loop();
        }
        public void getcontrol()
        {
            switch (cntrol)
            {
                case "lblComboBox":

                    outText.AppendLine(" private Bsoft.Controls.lblComboBox " + controlName + "; ");

                    break;
                case "lblTextBox":

                    outText.AppendLine(" private Bsoft.Controls.lblTextBox " + controlName + "; ");

                    break;
                case "TextBox":

                    outText.AppendLine(" private System.Windows.Forms.TextBox " + controlName + "; ");

                    break;
                case "ComboBox":

                    outText.AppendLine(" private System.Windows.Forms.ComboBox " + controlName + "; ");

                    break;
                case "Label":

                    outText.AppendLine(" private System.Windows.Forms.Label  " + "lbl" + dbName + "; ");

                    break;
                case "Grid":

                    outText.AppendLine(" private System.Windows.Forms.DataGridViewTextBoxColumn  " + colName + "; ");

                    break;
                case "GroupBox":

                    outText.AppendLine(" private System.Windows.Forms.GroupBox " + controlName + "; ");

                    break;
                case "RadioButton":

                    outText.AppendLine(" private System.Windows.Forms.RadioButton " + controlName + "; ");

                    break;
                case "CheckBox":

                    outText.AppendLine(" private System.Windows.Forms.CheckBox " + controlName + "; ");

                    break;
                case "VtextBox":

                    outText.AppendLine(" private Bsoft.Controls.VtextBox " + controlName + "; ");

                    break;
                case "DateTimePicker":

                    outText.AppendLine(" System.Windows.Forms.DateTimePicker " + controlName + "; ");

                    break;
            }
        }
    }

    public class clsControl : BasicFrame, Frame
    {
        int left_s = 0;
        int top_s = 0;
        int i = 0;
        public override void GenCreate_Afterlines()
        {
        }
        public override void GenCreate_Beforelines()
        {
        }
        public override void GenCreate_loop()
        {
            getcontrol("this");
        }
        public override void GenCreate_loop_Fistline()
        {
            GenCreate_loop();
        }
        public override void GenCreate_loop_Lastline()
        {
            GenCreate_loop();
        }

        public void getcontrol(string parent)
        {
            switch (cntrol)
            {
                case "lblComboBox":

                    // left_s = left_s + 10
                    top_s = top_s + 30;
                    outText.AppendLine("// ");
                    outText.AppendLine("//" + controlName);
                    outText.AppendLine("// ");
                    outText.AppendLine(" this." + controlName + " = new Bsoft.Controls.lblComboBox();");
                    outText.AppendLine(" this." + controlName + " .BackColor = System.Drawing.Color.Transparent;");
                    outText.AppendLine(" this." + controlName + " .Name = \"" + controlName + "\";");
                    parent = "flwLayout";
                    outText.AppendLine(" this." + parent + ".Controls.Add(this." + controlName + ");");

                    if (string.IsNullOrEmpty(txt.Trim()))
                    {
                        txt = dbName;
                    }
                    outText.AppendLine(" this." + controlName + " .Label = \"" + txt + "\";");

                    break;
                case "lblTextBox":

                    // left_s = left_s + 10
                    top_s = top_s + 30;
                    outText.AppendLine("// ");
                    outText.AppendLine("//" + controlName);
                    outText.AppendLine("// ");
                    outText.AppendLine(" this." + controlName + " = new Bsoft.Controls.lblTextBox();");
                    outText.AppendLine(" this." + controlName + " .BackColor = System.Drawing.Color.Transparent;");
                    outText.AppendLine(" this." + controlName + " .Name = \"" + controlName + "\";");
                    if (type .ToLower () == "integer" | type.ToLower ().Contains("float"))
                    {
                        outText.AppendLine(" this." + controlName + ".vtxtBox.ValidationType = Bsoft.Controls.VtextBox.ValidationTypeEnum.Integer;");
                    }
                    //make id invisble,disabled by default
                    if (dbName.ToLower() == "id")
                    {
                        outText.AppendLine(" this." + controlName + " .Visible = false ;");
                        outText.AppendLine(" this." + controlName + " .Enabled = false ;");
                    }
                    //For limiting the input characters eg for varchar
                    int getnum = 0;
                    getnum = getNumber(type);
                    if (getnum > 0)
                    {
                        outText.AppendLine(" this." + controlName + ".vtxtBox.MaxLength = " + getnum.ToString() + ";");
                    }
                    parent = "flwLayout";
                    outText.AppendLine(" this." + parent + ".Controls.Add(this." + controlName + ");");

                    if (string.IsNullOrEmpty(txt.Trim()))
                    {
                        txt = dbName;
                    }
                    outText.AppendLine(" this." + controlName + " .Label = \"" + txt + "\";");

                    break;
                case "TextBox":

                    // left_s = left_s + 10
                    top_s = top_s + 30;
                    outText.AppendLine("// ");
                    outText.AppendLine("//" + controlName);
                    outText.AppendLine("// ");
                    outText.AppendLine(" this." + controlName + " = new System.Windows.Forms.TextBox();");
                    outText.AppendLine(" this." + controlName + " .Location = new System.Drawing.Point(" + left_s.ToString() + ", " + top_s.ToString() + ");");
                    outText.AppendLine(" this." + controlName + " .Name = \"" + controlName + "\";");

                    getnum = 0;
                    getnum = getNumber(type);
                    if (getnum > 0)
                    {
                        outText.AppendLine(" this." + controlName + " .MaxLength = " + getnum.ToString() + ";");
                    }
                    outText.AppendLine(" this." + controlName + " .Size = new System.Drawing.Size(160, 20);");
                    //outText.AppendLine(" this." & controlName & " .Text = " & q & txt & q & ";")
                    outText.AppendLine(" this." + controlName + " .TabIndex = " + i.ToString() + ";");
                    if (parent == "this")
                    {
                        outText.AppendLine(" this.Controls.Add(this." + controlName + ");");
                    }
                    else
                    {
                        outText.AppendLine(" this." + parent + ".Controls.Add(this." + controlName + ");");
                    }

                    break;
                case "ComboBox":

                    // left_s = left_s + 10
                    top_s = top_s + 30;
                    outText.AppendLine("// ");
                    outText.AppendLine("//" + controlName);
                    outText.AppendLine("// ");
                    outText.AppendLine(" this." + controlName + " = new System.Windows.Forms.ComboBox();");
                    outText.AppendLine(" this." + controlName + " .FormattingEnabled = true;");
                    outText.AppendLine(" this." + controlName + " .Location = new System.Drawing.Point(" + left_s.ToString() + ", " + top_s.ToString() + ");");
                    outText.AppendLine(" this." + controlName + " .Name = \"" + controlName + "\";");
                    outText.AppendLine(" this." + controlName + " .Size = new System.Drawing.Size(160, 20);");
                    outText.AppendLine(" this." + controlName + " .TabIndex = " + i.ToString() + ";");
                    if (parent == "this")
                    {
                        outText.AppendLine(" this.Controls.Add(this." + controlName + ");");
                    }
                    else
                    {
                        outText.AppendLine(" this." + parent + ".Controls.Add(this." + controlName + ");");
                    }

                    break;
                case "Label":

                    top_s = top_s + 30;
                    outText.AppendLine("// ");
                    outText.AppendLine("// " + "lbl" + dbName);
                    outText.AppendLine("// ");
                    outText.AppendLine("this." + "lbl" + dbName + " = new System.Windows.Forms.Label();");
                    outText.AppendLine("this." + "lbl" + dbName + ".BackColor = System.Drawing.Color.Transparent;");
                    outText.AppendLine("this." + "lbl" + dbName + ".Location =new System.Drawing.Point(" + left_s.ToString() + ", " + top_s.ToString() + ");");
                    outText.AppendLine("this." + "lbl" + dbName + ".Name =\"" + controlName + "\";");
                    outText.AppendLine("this." + "lbl" + dbName + ".Size = new System.Drawing.Size(160, 22);");
                    outText.AppendLine("this." + "lbl" + dbName + ".TabIndex = 0;");
                    if (string.IsNullOrEmpty(txt.Trim()))
                    {
                        txt = dbName;
                    }
                    outText.AppendLine(" this." + controlName + " .Text = \"" + txt + "\";");
                    outText.AppendLine("this." + "lbl" + dbName + ".TextAlign = System.Drawing.ContentAlignment.MiddleRight;");
                    if (parent == "this")
                    {
                        outText.AppendLine(" this.Controls.Add(this." + controlName + ");");
                    }
                    else
                    {
                        outText.AppendLine(" this." + parent + ".Controls.Add(this." + controlName + ");");

                    }
                    break;
                case "Grid":

                    outText.AppendLine("// ");
                    outText.AppendLine("// " + colName);
                    outText.AppendLine("// ");
                    outText.AppendLine("this." + colName + " = new System.Windows.Forms.DataGridViewTextBoxColumn();");

                    outText.AppendLine("this." + colName + ".AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;");
                    if (string.IsNullOrEmpty(txt.Trim()))
                    {
                        txt = dbName ;
                    }
                    outText.AppendLine(" this." + colName + " .HeaderText = \"" + txt + "\";");
                    outText.AppendLine(" this." + colName + " .Tag = \"" + dbName + "\";");
                    outText.AppendLine("this." + colName + ".Name = \"" + colName + "\";");

                    break;

                case "GroupBox":

                    // left_s = left_s + 10
                    top_s = top_s + 30;
                    outText.AppendLine("// ");
                    outText.AppendLine("//" + controlName);
                    outText.AppendLine("// ");
                    outText.AppendLine(" this." + controlName + " = new System.Windows.Forms.GroupBox();");
                    outText.AppendLine(" this." + controlName + " .Location = new System.Drawing.Point(" + left_s.ToString() + ", " + top_s.ToString() + ");");
                    outText.AppendLine(" this." + controlName + " .Name = \"" + controlName + "\";");
                    outText.AppendLine(" this." + controlName + " .Size = new System.Drawing.Size(160, 20);");
                    outText.AppendLine(" this." + controlName + " .TabIndex = " + i.ToString() + ";");

                    outText.AppendLine(" this." + controlName + " .BackColor = System.Drawing.Color.Transparent; ");
                    outText.AppendLine(" this." + controlName + " .TabStop = false;");
                    outText.AppendLine(" this." + controlName + " .Text = \"" + txt + "\";");

                    if (parent == "this")
                    {
                        outText.AppendLine(" this.Controls.Add(this." + controlName + ");");
                    }
                    else
                    {
                        outText.AppendLine(" this." + parent + ".Controls.Add(this." + controlName + ");");
                    }

                    break;
                case "RadioButton":

                    // left_s = left_s + 10
                    top_s = top_s + 30;
                    outText.AppendLine("// ");
                    outText.AppendLine("//" + controlName);
                    outText.AppendLine("// ");
                    outText.AppendLine(" this." + controlName + " = new System.Windows.Forms.RadioButton();");
                    outText.AppendLine(" this." + controlName + ".UseVisualStyleBackColor = true;");
                    outText.AppendLine(" this." + controlName + ".AutoSize = true;");
                    outText.AppendLine(" this." + controlName + " .Location = new System.Drawing.Point(" + left_s.ToString() + ", " + top_s.ToString() + ");");
                    outText.AppendLine(" this." + controlName + " .Name = \"" + controlName + "\";");
                    outText.AppendLine(" this." + controlName + " .Size = new System.Drawing.Size(160, 20);");
                    outText.AppendLine(" this." + controlName + " .TabIndex = " + i.ToString() + ";");
                    if (string.IsNullOrEmpty(txt.Trim()))
                    {
                        txt = dbName;
                    }
                    outText.AppendLine(" this." + controlName + " .Text = \"" + txt + "\";");
                    if (parent == "this")
                    {
                        outText.AppendLine(" this.Controls.Add(this." + controlName + ");");
                    }
                    else
                    {
                        outText.AppendLine(" this." + parent + ".Controls.Add(this." + controlName + ");");
                    }

                    break;
                case "CheckBox":

                    // left_s = left_s + 10
                    top_s = top_s + 30;
                    outText.AppendLine("// ");
                    outText.AppendLine("//" + controlName);
                    outText.AppendLine("// ");
                    outText.AppendLine(" this." + controlName + " = new System.Windows.Forms.CheckBox();");
                    outText.AppendLine(" this." + controlName + ".UseVisualStyleBackColor = true;");
                    outText.AppendLine(" this." + controlName + ".AutoSize = true;");
                    outText.AppendLine(" this." + controlName + " .Location = new System.Drawing.Point(" + left_s.ToString() + ", " + top_s.ToString() + ");");
                    outText.AppendLine(" this." + controlName + " .Name = \"" + controlName + "\";");
                    outText.AppendLine(" this." + controlName + " .Size = new System.Drawing.Size(160, 20);");
                    if (string.IsNullOrEmpty(txt.Trim()))
                    {
                        txt = dbName;
                    }
                    outText.AppendLine(" this." + controlName + " .Text = \"" + txt + "\";");
                    outText.AppendLine(" this." + controlName + " .TabIndex = " + i.ToString() + ";");
                    if (parent == "this")
                    {
                        outText.AppendLine(" this.Controls.Add(this." + controlName + ");");
                    }
                    else
                    {
                        outText.AppendLine(" this." + parent + ".Controls.Add(this." + controlName + ");");
                    }

                    break;
                case "VtextBox":

                    // left_s = left_s + 10
                    top_s = top_s + 30;
                    outText.AppendLine("// ");
                    outText.AppendLine("//" + controlName);
                    outText.AppendLine("// ");
                    outText.AppendLine(" this." + controlName + " = new Bsoft.Controls.VtextBox();");
                    outText.AppendLine(" this." + controlName + " .Location = new System.Drawing.Point(" + left_s.ToString() + ", " + top_s.ToString() + ");");
                    outText.AppendLine(" this." + controlName + " .Name = \"" + controlName + "\";");
                    outText.AppendLine(" this." + controlName + " .Size = new System.Drawing.Size(160, 20);");
                    if (type.ToLower() == "Integer".ToLower())
                    {
                        outText.AppendLine(" this." + controlName + " .ValidationType = Bsoft.Controls.VtextBox.ValidationTypeEnum.Integer;");
                    }
                    else
                    {
                        outText.AppendLine(" this." + controlName + " .ValidationType = Bsoft.Controls.VtextBox.ValidationTypeEnum.No_Validation;");
                    }


                    outText.AppendLine(" this." + controlName + " .Value = \"" + "\";");
                    outText.AppendLine(" this." + controlName + " .TabIndex = " + i.ToString() + ";");
                    if (parent == "this")
                    {
                        outText.AppendLine(" this.Controls.Add(this." + controlName + ");");
                    }
                    else
                    {
                        outText.AppendLine(" this." + parent + ".Controls.Add(this." + controlName + ");");
                    }

                    break;
                case "DateTimePicker":

                    // left_s = left_s + 10
                    top_s = top_s + 30;
                    outText.AppendLine("// ");
                    outText.AppendLine("//" + controlName);
                    outText.AppendLine("// ");
                    outText.AppendLine(" this." + controlName + " = new System.Windows.Forms.DateTimePicker();");
                    outText.AppendLine(" this." + controlName + " .Location = new System.Drawing.Point(" + left_s.ToString() + ", " + top_s.ToString() + ");");
                    outText.AppendLine(" this." + controlName + " .Name = \"" + controlName + "\";");
                    outText.AppendLine(" this." + controlName + " .Size = new System.Drawing.Size(110, 20);");
                    outText.AppendLine(" this." + controlName + ".CustomFormat = \"" + "dd/MMM/yyyy\"" + ";");
                    outText.AppendLine(" this." + controlName + ".Format = System.Windows.Forms.DateTimePickerFormat.Custom;");
                    outText.AppendLine(" this." + controlName + " .TabIndex = " + i.ToString() + ";");
                    if (parent == "this")
                    {
                        outText.AppendLine(" this.Controls.Add(this." + controlName + ");");
                    }
                    else
                    {
                        outText.AppendLine(" this." + parent + ".Controls.Add(this." + controlName + ");");
                    }

                    break;
            }
        }

    }

}
