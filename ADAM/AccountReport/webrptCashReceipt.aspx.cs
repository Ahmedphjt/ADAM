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
    public partial class webrptCashReceipt : System.Web.UI.Page
    {
        public int pageid = 126;

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
            var Rows = from a in db.DocumentHeaders where a.DocNo == long.Parse(txtDocumentCode.Text) && a.DocType == 2 select a;
            if (string.IsNullOrEmpty(txtDocumentCode.Text))
            {
                txtDocumentCode.Text = "0";
                ShowReport(0);
            }
            if (Rows.Count() > 0)
            {
                ADAM.DataBase.DocumentHeader dr = db.DocumentHeaders.Single(a => a.DocNo == long.Parse(txtDocumentCode.Text) && a.DocType == 2);
                ShowReport(dr.Id);
            }
            else
            {
                Response.Write("<script>alert('من فضلك تأكد من رقم سند الصرف')</script>");
                return;
            }
        }

        private void ShowReport(long DocId)
        {
            ReportDocument myReportDocument = new ReportDocument();

            myReportDocument.Load(Server.MapPath("~/AccountReport/Report/rptCashReceipts.rpt"));
            myReportDocument.Refresh();
            CrystalReportViewer1.ReportSource = myReportDocument;

            myReportDocument.DataSourceConnections[0].SetConnection(csGetPermission.DBServerName, csGetPermission.DBName, csGetPermission.DBUser, csGetPermission.DBPassword);
            myReportDocument.SetParameterValue("@DocumentHeaderId", DocId);
            myReportDocument.SetParameterValue("@DocType", 2);
            myReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
        }
    }
}