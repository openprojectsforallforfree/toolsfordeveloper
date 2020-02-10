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
    public partial class entCompany_CompanyGroup : Friuts.EntryFormBase
    {
        public entCompany_CompanyGroup()
        {
            InitializeComponent();
        }

        public entCompany_CompanyGroup(string TableName_)
        {
            TableName = TableName_;
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void entCompany_CompanyGroup_Load(object sender, EventArgs e)
        {
            //<Entry>
TableName = "Company_CompanyGroup";
ColumnList.AddNewEditColumn("id",ColumnTypes.Number, txtid ,true ,false );
ColumnList.AddNewEditColumn("CompanyGroupId",ColumnTypes.Number, txtCompanyGroupId );

ColumnList.AddNewEditColumn("CompanyId",ColumnTypes.Number, txtCompanyId );


         
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}