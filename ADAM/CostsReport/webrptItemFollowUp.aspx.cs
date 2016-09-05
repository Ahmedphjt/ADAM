using ADAM.BasicData;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.CostsReport
{
    public partial class webrptItemFollowUp : System.Web.UI.Page
    {
        public int pageid = 112;

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
            myReportDocument.Load(Server.MapPath("~/CostsReport/Reports/rptItemFollowUp.rpt"));
            myReportDocument.Refresh();
            CrystalReportViewer1.ReportSource = myReportDocument;
            myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);

            myReportDocument.SetParameterValue("@ItemId", long.Parse(ddlItemName.SelectedValue));
            myReportDocument.SetParameterValue("@ItemTypeId", long.Parse(ddlItemType.SelectedValue));
            myReportDocument.SetParameterValue("@ProductionLineId", long.Parse(ddlProductionLine.SelectedValue));
            if (string.IsNullOrEmpty(txtFMovementDate.Text))
                txtFMovementDate.Text = "2000-01-01";
            if(string.IsNullOrEmpty(txtEMovementDate.Text))
                txtEMovementDate.Text = "3000-01-01";
            myReportDocument.SetParameterValue("@FMovementDate",DateTime.Parse(txtFMovementDate.Text));
            myReportDocument.SetParameterValue("@EMovementDate", DateTime.Parse(txtEMovementDate.Text));

            myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
        }
    }
}