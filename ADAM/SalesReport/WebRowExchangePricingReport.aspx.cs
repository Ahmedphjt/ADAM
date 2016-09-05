using ADAM.BasicData;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.SalesReport
{
    public partial class WebRowExchangePricingReport : System.Web.UI.Page
    {
        public int pageid = 104;

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
            if (ddlENo.SelectedValue == "0")
                myReportDocument.Load(Server.MapPath("~/SalesReport/Reports/rptRowExchangePricing.rpt"));
            else if (ddlENo.SelectedValue == "1")
                myReportDocument.Load(Server.MapPath("~/SalesReport/Reports/rptRowExchangePricingWithoutNo.rpt"));
            else if (ddlENo.SelectedValue == "2")
                myReportDocument.Load(Server.MapPath("~/SalesReport/Reports/rptRowExchangePricingShow.rpt"));

            myReportDocument.Refresh();
            CrystalReportViewer1.ReportSource = myReportDocument;
            myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);
            myReportDocument.SetParameterValue("@ExchangeRequestNo", long.Parse(txtExchangeOrder.Text));
            myReportDocument.SetParameterValue("@OrderType", 8);
            myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
        }

        protected void gvExchangePrice_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReportDocument myReportDocument = new ReportDocument();
            if (ddlENo.SelectedValue == "0")
                myReportDocument.Load(Server.MapPath("~/SalesReport/Reports/rptRowExchangePricing.rpt"));
            else
                myReportDocument.Load(Server.MapPath("~/SalesReport/Reports/rptRowExchangePricingWithoutNo.rpt"));
            myReportDocument.Refresh();
            CrystalReportViewer1.ReportSource = myReportDocument;
            myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);
            myReportDocument.SetParameterValue("@ExchangeRequestNo", long.Parse(gvExchangePrice.SelectedRow.Cells[0].Text));
            myReportDocument.SetParameterValue("@OrderType", 8);
            myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
        }
    }
}