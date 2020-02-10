using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Collections;
using System.Data;
using System.Diagnostics;
using ICg;
using System.Windows.Forms;

namespace myCg
{
    public class Controlsw
    {

        public static void Execute(bool forceLable, bool withPanel, string parent, string lableEnding)
        {

            string textout = "";
            DataSet ds = ICg.ExcelFunction.getData();
            if (ds == null)
                return;

            foreach (DataTable dt in ds.Tables)
            {
                textout = textout + createControls(forceLable, dt, withPanel, parent);
            }
            try
            {
                Clipboard.SetText(textout);
            }
            catch
            {
            }

        }

        public static string createControls(bool withLables, DataTable dt, bool withPanel,string parent)
        {

            string FinalOut = "";
            string lblDec = "";
            string lblCtrl = "";
            string pnlDec = "";
            string pnlCtrl = "";
            string defaultDeclare = "";
            string defaultCtrl = "";

            clsDeclare_Control dec = new clsDeclare_Control();
            clsControl ctrl = new clsControl();
           
          //  ctrl.TextSuffix = lableEnding;
            if (withPanel)
            {
                ctrl.withPanel = withPanel;
                defaultDeclare = dec.gencreate(172, 1, dt);
                defaultCtrl = ctrl.gencreate(172, 1, dt);
            }
            else
            {
                ctrl.withPanel = withPanel;
                defaultDeclare = dec.gencreate(262, 22, dt);
                defaultCtrl = ctrl.gencreate(262, 22, dt);
            }


            if (withLables)
            {
                
                clsDeclare_Control decL = new clsDeclare_Control();
                clsControl ctrlL = new clsControl();
                ctrlL.withPanel = withPanel;
                decL.forceControl = "Label";
                ctrlL.forceControl = "Label";
                if (withPanel)
                {
                    lblDec = decL.gencreate(10, 1, dt);
                    lblCtrl = ctrlL.gencreate(10, 1, dt);
                }
                else
                {
                    lblDec = decL.gencreate(10, 22, dt);
                    lblCtrl = ctrlL.gencreate(10, 22, dt);
                }


            }
            if (withPanel)
            {
                clsDeclare_Control decL = new clsDeclare_Control();
                clsControl ctrlL = new clsControl();
                decL.forceControl = "Panel";
                ctrlL.forceControl = "Panel";
                ctrlL.parent = parent ;
                pnlDec = decL.gencreate(100, 22, dt);
                pnlCtrl = ctrlL.gencreate(100, 22, dt);
            }

            FinalOut = pnlCtrl + "\n" + defaultCtrl + "\n" + lblCtrl + "\n}//<Control>" + "\n" + pnlDec + "\n" + defaultDeclare + "\n" + lblDec;
            return FinalOut;
        }
    }


    class Controls : PlugIn.MyClassBase
    {
        public Controls()
            : base()
        {
            Menu.Text = "Control";
        }
        public override void Execute()
        {
            Controlsw.Execute(false, false, "this","");

        }
    }

    class ControlsWithLable : PlugIn.MyClassBase
    {
        public ControlsWithLable()
            : base()
        {
            Menu.Text = "Controls With Lables";
        }
        public override void Execute()
        {
            Controlsw.Execute(true, false,"this","");

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
            if (cntrol.ToLower () =="none")
            {
                return;
            }
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
                case "Panel":
                    outText.AppendLine(" private System.Windows.Forms.Panel " + controlName + "; ");
                    break;
                case "lblComboBox":
                    outText.AppendLine(" private Bsoft.Controls.lblComboBox " + controlName + "; ");
                    break;
                case "lblTextBox":
                    outText.AppendLine(" private Bsoft.Controls.lblTextBox " + controlName + "; ");
                    break;
                default :
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
                case "NepaliDateControl":  
                    outText.AppendLine(" CrudeFx.NepaliDate.NepaliDatePicker " + controlName + "; ");
                    break;
            }
        }
    }

    public class clsControl : BasicFrame, Frame
    { 
        public bool withPanel;
        public string textSuffix=string .Empty ;
        public clsControl()
        {
            textSuffix = Properties.Settings.Default.TextSuffix;
        }
        int i = 0;
        public override void GenCreate_Afterlines()
        {
        }
        public override void GenCreate_Beforelines()
        {
        }
        public override void GenCreate_loop()
        {
            if (cntrol.ToLower() == "none")
            {
                return;
            }
            getcontrol(withPanel,textSuffix );
        }
        public override void GenCreate_loop_Fistline()
        {
            GenCreate_loop();
        }
        public override void GenCreate_loop_Lastline()
        {
            GenCreate_loop();
        }

        public void getcontrol(bool withPanel,string textSuffix )
        {   
            if (withPanel)
            {
                parent = "pnl" + dbName;
            }

            switch (cntrol)
            {

                case "Panel":
                    top_s = top_s + 50;
                    outText.AppendLine("// ");
                    outText.AppendLine("// " + "lbl" + dbName);
                    outText.AppendLine("// ");
                    outText.AppendLine("this." + "pnl" + dbName + " = new System.Windows.Forms.Panel();");
                    outText.AppendLine("this." + "pnl" + dbName + ".BackColor = System.Drawing.Color.Transparent;");
                    outText.AppendLine("this." + "pnl" + dbName + ".Location =new System.Drawing.Point(" + left_s.ToString() + ", " + top_s.ToString() + ");");
                    outText.AppendLine("this." + "pnl" + dbName + ".Name =\"" + controlName + "\";");
                    outText.AppendLine("this." + "pnl" + dbName + ".Size = new System.Drawing.Size(400, 45);");
                    outText.AppendLine("this." + "pnl" + dbName + ".TabIndex = 0;");
                      outText.AppendLine("this." + "pnl" + dbName + ".Margin = new System.Windows.Forms.Padding(1);");
                    outText.AppendLine("this." + "pnl" + dbName + " .AutoSize = true;");
                    outText.AppendLine(" this." + "pnl" + dbName + ".AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;");
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
                case "lblComboBox":

                    // left_s = left_s + 10
                    if (!withPanel)
                    {
                        top_s = top_s + 30;
                    }
                    outText.AppendLine("// ");
                    outText.AppendLine("//" + controlName);
                    outText.AppendLine("// ");
                    outText.AppendLine(" this." + controlName + " = new Bsoft.Controls.lblComboBox();");
                    outText.AppendLine(" this." + controlName + " .BackColor = System.Drawing.Color.Transparent;");
                    outText.AppendLine(" this." + controlName + " .Name = \"" + controlName + "\n;");
                    parent = "flwLayout";
                    outText.AppendLine(" this." + parent + ".Controls.Add(this." + controlName + ");");

                    if (string.IsNullOrEmpty(txt.Trim()))
                    {
                        txt = dbName;
                    }
                    outText.AppendLine(" this." + controlName + " .Label = \n" + txt + "\";");

                    break;
                case "lblTextBox":

                    // left_s = left_s + 10
                    if (!withPanel)
                    {
                        top_s = top_s + 30;
                    }
                    outText.AppendLine("// ");
                    outText.AppendLine("//" + controlName);
                    outText.AppendLine("// ");
                    outText.AppendLine(" this." + controlName + " = new Bsoft.Controls.lblTextBox();");
                    outText.AppendLine(" this." + controlName + " .BackColor = System.Drawing.Color.Transparent;");
                    outText.AppendLine(" this." + controlName + " .Name = \"" + controlName + "\";");
                    if (type.ToLower() == "integer" | type.ToLower().Contains("float"))
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
                default :
                case "TextBox":

                    // left_s = left_s + 10
                    if (!withPanel )
                    {
                        top_s = top_s + 30;
                    }
                   
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

                    if (getnum > 50 || (getnum ==0 && type.Contains ("varchar")))
                    {
                        outText.AppendLine(" this." + controlName + " .Size = new System.Drawing.Size(400, 50);");
                        outText.AppendLine(" this." + controlName + " .Multiline = true;");
                    }
                    else
                    {
                        outText.AppendLine(" this." + controlName + " .Size = new System.Drawing.Size(160, 20);");
                    }
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
                    if (!withPanel)
                    {
                        top_s = top_s + 30;
                    }
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

                    if (!withPanel)
                    {
                        top_s = top_s + 30;
                    }
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
                        txt = dbName + textSuffix;
                    }
                    else
                    {
                        txt = txt + textSuffix;
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
                        txt = dbName;
                    }
                    outText.AppendLine(" this." + colName + " .HeaderText = \"" + txt + "\";");
                    outText.AppendLine(" this." + colName + " .Tag = \"" + dbName + "\";");
                    outText.AppendLine("this." + colName + ".Name = \"" + colName + "\";");

                    break;

                case "GroupBox":

                    // left_s = left_s + 10
                    if (!withPanel)
                    {
                        top_s = top_s + 30;
                    }
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
                    if (!withPanel)
                    {
                        top_s = top_s + 30;
                    }
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
                    if (!withPanel)
                    {
                        top_s = top_s + 30;
                    }
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
                    if (!withPanel)
                    {
                        top_s = top_s + 30;
                    }
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
                    if (!withPanel)
                    {
                        top_s = top_s + 30;
                    }
                    outText.AppendLine("// ");
                    outText.AppendLine("//" + controlName);
                    outText.AppendLine("// ");
                    outText.AppendLine(" this." + controlName + " = new System.Windows.Forms.DateTimePicker();");
                    outText.AppendLine(" this." + controlName + " .Location = new System.Drawing.Point(" + left_s.ToString() + ", " + top_s.ToString() + ");");
                    outText.AppendLine(" this." + controlName + " .Name = \"" + controlName + "\";");
                    outText.AppendLine(" this." + controlName + " .Size = new System.Drawing.Size(110, 20);");
                    outText.AppendLine(" this." + controlName + ".CustomFormat = \"" + "dd/MMM/yyyy" + "\";");
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
                case "NepaliDateControl":

                
                    
                    //nepaliDate1.Day = 12;
                    //nepaliDate1.Month = 11;
                    //nepaliDate1.Year = 2065;
                    //this.dtDateIssued.Value = nepaliDate1;
                 
                    // left_s = left_s + 10
                    if (!withPanel)
                    {
                        top_s = top_s + 30;
                    }
                    outText.AppendLine("// ");
                    outText.AppendLine("//" + controlName);
                    outText.AppendLine("// ");
                    outText.AppendLine(" this." + controlName + " = new CrudeFx.NepaliDate.NepaliDatePicker();");
                    outText.AppendLine(" this." + controlName + " .Location = new System.Drawing.Point(" + left_s.ToString() + ", " + top_s.ToString() + ");");
                    outText.AppendLine(" this." + controlName + " .Name = \"" + controlName + "\";");
                    outText.AppendLine(" this." + controlName + " .Size = new System.Drawing.Size(110, 20);");
                   // outText.AppendLine(" this." + controlName + ".CustomFormat = \"" + "dd/MMM/yyyy" + "\";");
                   // outText.AppendLine(" this." + controlName + ".Format = System.Windows.Forms.DateTimePickerFormat.Custom;");
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

    class ControlsWithPanel : PlugIn.MyClassBase
    {
        public ControlsWithPanel()
            : base()
        {
            Menu.Text = "Controls With Panels";
        }
        public override void Execute()
        {
            Controlsw.Execute(true, true,"this","");

        }
    }

}
