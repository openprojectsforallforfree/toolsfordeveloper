namespace UEMS
{
    partial class entCompanyGroup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnOk = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.flwLayout = new Bsoft.Controls.LableFlowLayout();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(358, 426);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 29);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOk.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnOk.Location = new System.Drawing.Point(264, 426);
            this.btnOk.Margin = new System.Windows.Forms.Padding(0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(78, 29);
            this.btnOk.TabIndex = 8;
            this.btnOk.Values.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // flwLayout
            // 
            this.flwLayout.AutoScroll = true;
            this.flwLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flwLayout.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.flwLayout.lable = "";
            this.flwLayout.Location = new System.Drawing.Point(3, 3);
            this.flwLayout.Name = "flwLayout";
            this.flwLayout.Size = new System.Drawing.Size(615, 312);
            this.flwLayout.TabIndex = 11;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.flwLayout);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(644, 411);
            this.flowLayoutPanel1.TabIndex = 12;
            // 
            // entCompanyGroup
            // 
            this.ClientSize = new System.Drawing.Size(701, 464);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "entCompanyGroup";
            this.ShowIcon = false;
            this.StateCommon.Header.Content.LongText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.StateCommon.Header.Content.LongText.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.TextExtra = "";
            this.Load += new System.EventHandler(this.entCompanyGroup_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        // 
//txtid
// 
 this.txtid = new System.Windows.Forms.TextBox();
 this.txtid .Location = new System.Drawing.Point(0, 30);
 this.txtid .Name = "txtid";
 this.txtid .MaxLength = 5;
 this.txtid .Size = new System.Drawing.Size(160, 20);
 this.txtid .TabIndex = 0;
 this.Controls.Add(this.txtid);
// 
//txtName
// 
 this.txtName = new System.Windows.Forms.TextBox();
 this.txtName .Location = new System.Drawing.Point(0, 60);
 this.txtName .Name = "txtName";
 this.txtName .MaxLength = 50;
 this.txtName .Size = new System.Drawing.Size(160, 20);
 this.txtName .TabIndex = 0;
 this.Controls.Add(this.txtName);
// 
//txtDetails
// 
 this.txtDetails = new System.Windows.Forms.TextBox();
 this.txtDetails .Location = new System.Drawing.Point(0, 90);
 this.txtDetails .Name = "txtDetails";
 this.txtDetails .MaxLength = 150;
 this.txtDetails .Size = new System.Drawing.Size(160, 20);
 this.txtDetails .TabIndex = 0;
 this.Controls.Add(this.txtDetails);

}//<Control>
 private System.Windows.Forms.TextBox txtid; 
 private System.Windows.Forms.TextBox txtName; 
 private System.Windows.Forms.TextBox txtDetails; 
      



        #endregion
        //<declaration>
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnOk;
        private Bsoft.Controls.LableFlowLayout flwLayout;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}