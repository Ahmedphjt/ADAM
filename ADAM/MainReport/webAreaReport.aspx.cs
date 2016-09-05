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
    public partial class webAreaReport : System.Web.UI.Page
    {
        public int pageid = 62;

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

            myReportDocument.Load(Server.MapPath("~/MainReport/Reports/rptAreaData.rpt"));
            myReportDocument.Refresh();
            CrystalReportViewer1.ReportSource = myReportDocument;


            long CountryId = 0;
            long CityId = 0;
            long GovernorateId = 0;

            if (ddlCountry.SelectedValue != "0")
                CountryId = long.Parse(ddlCountry.SelectedValue);
            if (ddlCity.SelectedValue != "0")
                CityId = long.Parse(ddlCity.SelectedValue);
            if (ddlGovernorate.SelectedValue != "0")
                GovernorateId = long.Parse(ddlGovernorate.SelectedValue);

            myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);
            myReportDocument.SetParameterValue("@CountryId", CountryId);
            myReportDocument.SetParameterValue("@CityId", CityId);
            myReportDocument.SetParameterValue("@GovernorateId", GovernorateId);
            myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
        }
    }
}