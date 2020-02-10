using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Friuts;
namespace UEMS
{
    [System.ComponentModel.DesignerCategory("form")]
    public partial class entCompanyGroup : Friuts.EntryFormBase
    {
        public entCompanyGroup()
        {
            InitializeComponent();
        }

        public entCompanyGroup(string TableName_)
        {
            TableName = TableName_;
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void entCompanyGroup_Load(object sender, EventArgs e)
        {
            //<Entry>
TableName = "CompanyGroup";
ColumnList.AddNewEditColumn("id",ColumnTypes.Number, txtid ,true ,false );
ColumnList.AddNewEditColumn("Name",ColumnTypes.String, txtName );

ColumnList.AddNewEditColumn("Details",ColumnTypes.String, txtDetails );


         
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}