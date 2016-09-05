using ADAM.BasicData;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADAM.AccountReport
{
    public partial class webrptMezanElMorag3a : System.Web.UI.Page
    {
        public int pageid = 143;

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
            if (string.IsNullOrEmpty(txtFromDate.Text) || string.IsNullOrEmpty(txtToDate.Text))
            {
                Response.Write("<script>alert('من فضلك تأكد من أدخال تاريخ البداية وتاريخ النهاية بشكل صحيح')</script>");
                return;
            }

            DateTime DateFrom = DateTime.Parse(txtFromDate.Text);
            DateTime DateTo = DateTime.Parse(txtToDate.Text);

            DateTime NewDateFrom = new DateTime(DateFrom.Year, DateFrom.Month, DateFrom.Day, 00, 00, 00);
            DateTime NewDateTo = new DateTime(DateTo.Year, DateTo.Month, DateTo.Day, 23, 59, 59);

            ShowReport(NewDateFrom, NewDateTo);
        }

        private void ShowReport(DateTime DateFrom,DateTime DateTo)
        {
            ReportDocument myReportDocument = new ReportDocument();

            myReportDocument.Load(Server.MapPath("~/AccountReport/Report/rptMezanElMorag3a.rpt"));
            myReportDocument.Refresh();
            CrystalReportViewer1.ReportSource = myReportDocument;

            myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);
            myReportDocument.SetParameterValue("@FirstDate", DateFrom);
            myReportDocument.SetParameterValue("@LastDate", DateTo);
            myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
        }
    }
}