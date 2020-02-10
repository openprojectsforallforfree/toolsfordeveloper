using System;
using System.Configuration;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Dynamic
{
	/// <summary>
	/// Summary description for Main.
	/// </summary>
	public class Main1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Main1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2});
			this.menuItem1.Text = "Message1";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "Message2";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// Main
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Menu = this.mainMenu1;
			this.Name = "Main";
			this.Text = "Dynamic";
			this.Load += new System.EventHandler(this.Main_Load);

		}
		#endregion

//		/// <summary>
//		/// The main entry point for the application.
//		/// </summary>
//		[STAThread]
//		static void Main() 
//		{
//			Application.Run(new Main1());
//		}

		private void Main_Load(object sender, System.EventArgs e)
		{
			MessageBox.Show(ConfigurationSettings.AppSettings["App"]);
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show(ConfigurationSettings.AppSettings["App"]+" Message1");
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show(ConfigurationSettings.AppSettings["App"]+" Message2");
		}
	}
}
