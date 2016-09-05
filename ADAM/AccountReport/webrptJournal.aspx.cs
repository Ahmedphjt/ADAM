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
    public partial class webrptJournal : System.Web.UI.Page
    {
        public int pageid = 124;

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
            ADAM.DataBase.ADAMConnectionString db = new DataBase.ADAMConnectionString();
            var Rows = from a in db.JournalHeaders where a.JournalCode == long.Parse(txtJournalCode.Text) && a.JournalType == int.Parse(ddlJournalType.SelectedValue) select a;
            if (string.IsNullOrEmpty(txtJournalCode.Text))
            {
                txtJournalCode.Text = "0";
                ShowReport(0);
            }
            if (Rows.Count() > 0)
            {
                ADAM.DataBase.JournalHeader dr = db.JournalHeaders.Single(a => a.JournalCode == long.Parse(txtJournalCode.Text) && a.JournalType == int.Parse(ddlJournalType.SelectedValue));
                ShowReport(dr.Id);
            }
            else
            {
                Response.Write("<script>alert('من فضلك تأكد من رقم القيد')</script>");
                return;
            }
        }

        private void ShowReport(long JournalId)
        {
            ReportDocument myReportDocument = new ReportDocument();

            myReportDocument.Load(Server.MapPath("~/AccountReport/Report/rptJournal.rpt"));
            myReportDocument.Refresh();
            CrystalReportViewer1.ReportSource = myReportDocument;

            myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);
            myReportDocument.SetParameterValue("@JournalId", JournalId);
            myReportDocument.SetParameterValue("@JournalType", int.Parse(ddlJournalType.SelectedValue));
            myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
        }
    }
}