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
    public partial class lstCompany_CompanyGroup: Friuts.ListingFormBase
    {
        public lstCompany_CompanyGroup()
        {
            InitializeComponent();
            FormType = FormTypes.ListingForm;
            ListingDataGridView = grid;
           // toolstrip = toolStrip1;
  
        }

        private void List_Load(object sender, EventArgs e)
        {
            //<Listing>
TableName = "Company_CompanyGroup";
DataTableColumns.Add( "id",ColumnTypes.Number,"colid",true ,true);
DataTableColumns.Add( "CompanyGroupId",ColumnTypes.Number,"colCompanyGroupId");
DataTableColumns.Add( "CompanyId",ColumnTypes.Number,"colCompanyId");


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void newToolbar_Click(object sender, EventArgs e)
        {
            OpenNewForm();
        }

        private void EditToolbar_Click(object sender, EventArgs e)
        {
            OpenEditForm();
        }
        public override bool OpenEditForm()
        {
            return base.OpenEditForm(new entCompany_CompanyGroup (TableName));
        }

        public override bool OpenNewForm()
        {
            return base.OpenNewForm(new entCompany_CompanyGroup(TableName));
        }

        private void DeleteToolbar_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void ExitToolbar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ExportToolbar_Click(object sender, EventArgs e)
        {
            Reports.ExcelReport excel = new Reports.ExcelReport(this.Text, 4, grid);
        }
    }
}