using ADAM.BasicData;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.MainReport
{
    public partial class webEmployeeReport : System.Web.UI.Page
    {
        public int pageid = 28;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("~/BasicData/webLogIn.aspx");
            int userid = int.Parse(Session["UserID"].ToString());
            int operationid = 5;

            csGetPermission Per = new csGetPermission();
            if (!Per.getPermission(userid, pageid, operationid))
                Response.Redirect("~/BasicData/webHomePage.aspx");
        }

        protected void btnShowReport_Click(object sender, ImageClickEventArgs e)
        {
            ShowReport();
        }

        private void ShowReport()
        {

            ReportDocument myReportDocument = new ReportDocument();
            if (rdoNormalEmployee.Checked)
                myReportDocument.Load(Server.MapPath("~/MainReport/Reports/rptNormalEmployee.rpt"));
            else if (rdoSpecEmployee.Checked)
                myReportDocument.Load(Server.MapPath("~/MainReport/Reports/rptSpecEmployee.rpt"));

            myReportDocument.Refresh();
            CrystalReportViewer1.ReportSource = myReportDocument;
            long Code = 0;
            int sex = 0;
            long JobId = 0;
            long Idno = 0;
            long CountryId = 0;
            long CityId = 0;
            string FirstName = "0";
            string LastName = "0";
            long DepartmentId = 0;
            long DivisionId = 0;
            long QualificationId = 0;

            if (!string.IsNullOrEmpty(txtCode.Text))
                Code = long.Parse(txtCode.Text);
            if (ddlSex.SelectedValue != "")
                sex = int.Parse(ddlSex.SelectedValue);
            if (ddlJob.SelectedValue != "")
                JobId = long.Parse(ddlJob.SelectedValue);
            if (!string.IsNullOrEmpty(txtIdNo.Text))
                Idno = long.Parse(txtIdNo.Text);
            if (ddlCountry.SelectedValue != "")
                CountryId = long.Parse(ddlCountry.SelectedValue);
            if (ddlCity.SelectedValue != "")
                CityId = long.Parse(ddlCity.SelectedValue);
            if (!string.IsNullOrEmpty(txtFirstName.Text))
                FirstName = txtFirstName.Text;
            if (!string.IsNullOrEmpty(txtLastName.Text))
                LastName = txtLastName.Text;
            if (ddlDepartment.SelectedValue != "")
                DepartmentId = long.Parse(ddlDepartment.SelectedValue);
            if (ddlDivision.SelectedValue != "")
                DivisionId = long.Parse(ddlDivision.SelectedValue);
            if (ddlQualification.SelectedValue != "")
                QualificationId = long.Parse(ddlQualification.SelectedValue);

            //myReportDocument.SetDatabaseLogon(csGetPermission.DBUser, csGetPermission.DBPassword, csGetPermission.DBServerName, csGetPermission.DBName);
            myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);
            myReportDocument.SetParameterValue("@Code", Code);
            myReportDocument.SetParameterValue("@Sex", sex);
            myReportDocument.SetParameterValue("@JobId", JobId);
            myReportDocument.SetParameterValue("@IdNo", Idno);
            myReportDocument.SetParameterValue("@CountryId", CountryId);
            myReportDocument.SetParameterValue("@CityId", CityId);
            myReportDocument.SetParameterValue("@FirstName", FirstName);
            myReportDocument.SetParameterValue("@LastName", LastName);
            myReportDocument.SetParameterValue("@DepartmentId", DepartmentId);
            myReportDocument.SetParameterValue("@DivisionId", DivisionId);
            myReportDocument.SetParameterValue("@QualificationId", QualificationId);

            myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
        }
    }
}